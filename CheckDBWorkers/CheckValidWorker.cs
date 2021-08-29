using DBInfrastructure.DTOModels;
using Implemantation.IServices;
using Implemantation.Models;
using Implemantation.Models.Enums;
using Implemantation.Models.JsonModels;
using Implemantation.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SMS_Service_Worker.Common.Services.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace SMS_Service_Worker.Workers.CheckValidWorker
{
    public class CheckAccountValidWorker
    {
        private readonly IAccountService accountService;
        private readonly IHandlerConveyor handlerConveer;
        private readonly IHistoryService historyService;
        private readonly IOptions<ConfigurationClass> Config;

        public CheckAccountValidWorker(IAccountService accountService,
            IHandlerConveyor handlerConveer,
            IHistoryService historyService,
            IOptions<ConfigurationClass> Config)
        {
            this.accountService = accountService;
            this.handlerConveer = handlerConveer;
            this.historyService = historyService;
            this.Config = Config;
        }



        public async Task CheckAccountsOnValid()
        {
            AccountModel[] allAccounts = accountService.GetAllAccounts();

            foreach (var account in allAccounts)
            {
                var handlerReponse = handlerConveer.GetHandler(account).Result;
                if (handlerReponse.handler == null)
                {
                    if (handlerReponse.status == HandlerConveerStatus.InvalidCookie)
                    {
                        await accountService.DeactivateAccountAsync(account);
                        await historyService.InputNewHistoryAsync("0", (int)TypeRequests.NoCookie, "Invalid cookie");
                        continue;
                    }
                    if (handlerReponse.status == HandlerConveerStatus.NoProxy)
                    {
                        break;
                    }
                }

                HttpClient client = new(handlerReponse.handler);
                HttpContent accountProfileRequest = client.GetAsync(Config.Value.TextNowRoutes.GetProfileURI).Result.Content;
                string accountProfileResponse = accountProfileRequest.ReadAsStringAsync().Result;
                ErrorCode errorResponse;
                try
                {
                    errorResponse = JsonConvert.DeserializeObject<ErrorCode>(accountProfileResponse);
                }
                catch
                {
                    errorResponse = new ErrorCode { error_code = "no" };
                }

                if (errorResponse.error_code == Config.Value.Common.ErrorResponse)
                {
                    await accountService.DeactivateAccountAsync(account);
                    await historyService.InputNewHistoryAsync("0", (int)TypeRequests.CookieInactive, "Inactive cookie");
                    return;
                }
                var serializeUser = JsonConvert.DeserializeObject<JsonUser>(accountProfileResponse);
                if (serializeUser.phone_number == null)
                {
                    await historyService.InputNewHistoryAsync("0", (int)TypeRequests.AccountNoNumber, "Account not have a number");
                    await accountService.DeactivateAccountAsync(account, (int)AccountStatus.NoNumber);
                    continue;
                }
            }
            return;
        }
    }
}
