using Models.DTO.DTOModels;
using Implemantation.IServices;
using Models.ImplementationModels;
using Models.ImplementationModels.Enums;
using Models.ImplementationModels.JsonModels;
using Implemantation.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SMS_Service_Worker.Common.Services.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Models.Implementation.Models.HistoryInputModels;
using System;

namespace SMS_Service_Worker.Workers.CheckerDBWorker
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
                var handlerReponse = handlerConveer.GetHandlerAsync(account).Result;
                if (handlerReponse.handler == null)
                {
                    if (handlerReponse.status == HandlerConveerStatus.IncorrectCookie)
                    {
                        var newHistory = new HistoryJsonModel(){ Account = account, TimeIncident = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds };
                        accountService.DeactivateAccountAsync(account);
                        historyService.InputNewHistoryAsync("0", HistoryType.CookieIncorrect, newHistory);
                        continue;
                    }
                    if (handlerReponse.status == HandlerConveerStatus.NoCookie)
                    {
                        var newHistory = new HistoryJsonModel() { Account = account, TimeIncident = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds };
                        accountService.DeactivateAccountAsync(account);
                        historyService.InputNewHistoryAsync("0", HistoryType.NoCookie, newHistory);
                        continue;
                    }
                    if (handlerReponse.status == HandlerConveerStatus.NoProxy)
                    {
                        historyService.InputNewHistoryAsync("0", HistoryType.NoProxy, "No active proxies in the database");
                        break;
                    }
                }

                HttpClient client = new(handlerReponse.handler);
                HttpContent accountProfileRequest = client.GetAsync(Config.Value.TextNowRoutes.GetProfileURI + account.Login).Result.Content;
                string accountProfileResponse = accountProfileRequest.ReadAsStringAsync().Result;

                if (accountProfileResponse == "")
                {
                    historyService.InputNewHistoryAsync("0", HistoryType.ProxyNotHaveAccessToTN, "Cloudfare. No access to TextNow");
                    continue;
                }

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
                    accountService.DeactivateAccountAsync(account);
                    var newHistory = new HistoryJsonModel() { Account = account, TimeIncident = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds };
                    historyService.InputNewHistoryAsync("0", HistoryType.CookieInactive, newHistory);
                    continue;
                }
                JsonUser serializeUser;

                try
                {
                    serializeUser = JsonConvert.DeserializeObject<JsonUser>(accountProfileResponse);
                }
                catch 
                {
                    serializeUser = null;
                }
                if (serializeUser.phone_number == null)
                {
                    var newHistory = new HistoryJsonModel() { Account = account, TimeIncident = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds };
                    historyService.InputNewHistoryAsync("0", HistoryType.AccountNoNumber, newHistory);
                    accountService.DeactivateAccountAsync(account, AccountStatus.NoNumber);
                    continue;
                }
                else
                {
                    accountService.DeactivateAccountAsync(account, AccountStatus.Active);
                }
            }
            return;
        }
        

    }
}