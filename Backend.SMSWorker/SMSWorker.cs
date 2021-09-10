using Implemantation.IServices;
using Models.ImplementationModels;
using Models.ImplementationModels.Enums;
using Implemantation.Services;
using Microsoft.Extensions.Options;
using Models.DTO.DTOModels;
using Newtonsoft.Json;
using SMS_Service_Worker.Common.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Models.ImplementationModels.JsonModels.TNMessages;

namespace SMS_Service_Worker.Workers.SMSWorker
{
    public class SMSWorker
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IHistoryService historyService;
        private readonly IAccountService accountService;
        private readonly IServicePricesService pricesService;
        private readonly IHandlerConveyor handlerConveer;
        private readonly IOptions<ConfigurationClass> Config;

        public SMSWorker(IOrderService orderService,
            IHistoryService historyService,
            IAccountService accountService,
            IServicePricesService pricesService,
            IUserService userService,
            IHandlerConveyor handlerConveer,
            IOptions<ConfigurationClass> Сonfig)
        {
            this.accountService = accountService;
            this.orderService = orderService;
            this.historyService = historyService;
            this.pricesService = pricesService;
            this.userService = userService;
            this.handlerConveer = handlerConveer;
            this.Config = Сonfig;
        }

        public async Task StartSMSWorker(OrderModel order, UserModel user, AccountModel account, ProxyModel proxy, string firstMessgae = null)
        {
            // get handler
            HttpClientHandler handler = handlerConveer.GetHandler(account, proxy, true).Result.handler;
            if (handler == null)
            {
                await orderService.SetStatusAsync(order, (int)OrderStatuses.STATUS_CANCEL);
                return;
            }

            // request first message
            HttpClient client = new(handler);
            string response = GetMessage(client);

            // check account on valid
            ErrorCode errorResponse = new ErrorCode() { error_code = "not" };
            try
            {
                errorResponse = JsonConvert.DeserializeObject<ErrorCode>(response);
            }
            catch
            {

            }
            if (errorResponse.error_code == Config.Value.Common.ErrorResponse)
            {
                DeactivateAccount(order, account);
                return;
            }
            if (firstMessgae == null)
            {
                // TODO
            }

            // get sms code
            var message = JsonConvert.DeserializeObject<JsonMessages>(response).result.FirstOrDefault();
            JsonExpression ExpressionList = pricesService.GetAllExpressionByServiceId(order.Service);
            string smsCode = CheckForRegularExpressions(ExpressionList.Expressions, message.preview.message);
            
            // set result
            if (smsCode != null)
            {
                SetSMS(order, message, smsCode, user);
                return;
            }
            if (response != firstMessgae)
            {
                ResetOrder(order, user, message);
                return;
            }
        }

        private static string CheckForRegularExpressions(List<Expression> expressions, string message)
        {
            foreach (var expression in expressions)
            {
                Regex regex = new($@"${expression.RegularExpression}");
                MatchCollection matches = regex.Matches(message);
                if (matches.Count == 0)
                {
                    continue;
                }
                return matches.FirstOrDefault().Value;
            }
            return null;
        }

        public string GetMessage(HttpClient client)
        {
            var request = client.GetAsync(Config.Value.TextNowRoutes.GetMessageURI).Result.Content;
            var response = request.ReadAsStringAsync().Result;

            return response;
        }

        public async Task DeactivateAccount(OrderModel order, AccountModel account)
        {
            await orderService.SetStatusAsync(order, (int)OrderStatuses.STATUS_CANCEL);
            await accountService.DeactivateAccountAsync(account);
            await historyService.InputNewHistoryAsync("0", (int)TypeRequests.CookieInactive, "Inactive cookie");
            return;
        }

        public async Task SetSMS(OrderModel order, Result message, string smsCode, UserModel user)
        {
            await orderService.SetSMSAndSMSCodeAsync(order, message.preview.message, smsCode);
            await orderService.SetStatusAsync(order, (int)OrderStatuses.STATUS_OK);
            await userService.TakePaymentAsync(user.Id, pricesService.GetServicePriceByServiceId(order.Service));
            return;
        }

        public async Task ResetOrder(OrderModel order, UserModel user, Result message)
        {
            historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.InvalidExpressions, $"service: {order.Service} - all regexes failed, message: ${message.preview.message}");
            orderService.SetStatusAsync(order, (int)OrderStatuses.STATUS_FREE);
        }

    }
}
