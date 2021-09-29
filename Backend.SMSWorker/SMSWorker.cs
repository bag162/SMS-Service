using Backend.Implemantation.IServices;
using Backend.Models.Implementation;
using Backend.Models.Implementation.Enums;
using Backend.Implemantation.Services;
using Microsoft.Extensions.Options;
using Backend.Models.DB;
using Backend.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Backend.Models.Implementation.JsonModels.TNMessages;
using Backend.Models.Implementation.JsonModels;
using System.Text.Json;
using Backend.Models.Implementation.HistoryInputModels;

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
            var handlerResponse = handlerConveer.GetHandlerAsync(account, proxy, true).Result;
            if (handlerResponse.handler == null)
            {
                if (handlerResponse.status == HandlerConveerStatus.IncorrectCookie)
                {
                    DeactivateAccountAndOrder(order, account);
                    var newHistory = new HistoryJsonModel() { Account = account, TimeIncident = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds };
                    historyService.InputNewHistoryAsync("0", HistoryType.CookieIncorrect, newHistory);
                    return;
                }
                if (handlerResponse.status == HandlerConveerStatus.NoCookie)
                {
                    DeactivateAccountAndOrder(order, account);
                    var newHistory = new HistoryJsonModel() { Account = account, TimeIncident = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds };
                    historyService.InputNewHistoryAsync("0", HistoryType.NoCookie, newHistory);
                    return;
                }
                if (handlerResponse.status == HandlerConveerStatus.NoProxy)
                {
                    historyService.InputNewHistoryAsync("0", HistoryType.NoProxy);
                    return;
                }
            }
            var handler = handlerResponse.handler;

            // request first message
            HttpClient client = new(handler);
            string response = await GetMessage(client);
            if (response == "")
            {
                var newHestory = new HistoryJsonModel() { Proxy = proxy, TimeIncident = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds, Message = "Proxy not have access to TextNow. Cloudfare blocked session" };
                historyService.InputNewHistoryAsync("0", HistoryType.ProxyNotHaveAccessToTN, newHestory);
                return;
            }

            // check account on valid
            ErrorCode errorResponse = null;
            try
            {
                errorResponse = JsonSerializer.Deserialize<ErrorCode>(response);
            }
            catch
            {
                errorResponse.error_code = "not";
            }
            if (errorResponse.error_code == Config.Value.Common.ErrorResponse)
            {
                DeactivateAccountAndOrder(order, account);
                var newHistory = new HistoryJsonModel() { Account = account, TimeIncident = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds };
                historyService.InputNewHistoryAsync("0", HistoryType.CookieInactive, newHistory);
                return;
            }

            // parse message string
            string responseMessage = JsonSerializer.Deserialize<JsonMessages>(response).result.FirstOrDefault().preview.message;
            if (firstMessgae == null)
            {
                var updatedOrder = order;
                var jsonData = new TaskModel()
                {
                    Account = account,
                    FirstMessage = responseMessage,
                    Order = order,
                    Proxy = proxy,
                    User = user
                };
                updatedOrder.JsonData = JsonSerializer.Serialize(jsonData);
                orderService.UpdateOrderAsync(updatedOrder);
                historyService.InputNewHistoryAsync(user.Id, HistoryType.InsertFirstMessage);
                return;
            }

            if (firstMessgae == responseMessage)
                return;

            // get sms code
            JsonExpression ExpressionList = pricesService.GetAllExpressionByServiceId(order.Service);
            string smsCode = CheckForRegularExpressions(ExpressionList.Expressions, responseMessage);

            // set result
            if (smsCode != null)
            {
                SetSMS(order, responseMessage, smsCode, user);
                return;
            }

            ResetOrder(order, user, responseMessage);
            return;
        }

        private static string CheckForRegularExpressions(List<Expression> expressions, string message)
        {
            foreach (var expression in expressions)
            {
                Regex regex = new(expression.RegularExpression);
                MatchCollection matches = regex.Matches(message);
                if (matches.Count == 0)
                {
                    continue;
                }
                return matches.FirstOrDefault().Value;
            }
            
            return null;
        }

        public async Task<string> GetMessage(HttpClient client)
        {
            var request = client.GetAsync(Config.Value.TextNowRoutes.GetMessageURI).Result.Content;
            var response = request.ReadAsStringAsync().Result;

            return response;
        }

        public async Task DeactivateAccountAndOrder(OrderModel order, AccountModel account)
        {
            orderService.SetStatusAsync(order, OrderStatus.STATUS_CANCEL);
            accountService.DeactivateAccountAsync(account);
            return;
        }

        public async Task SetSMS(OrderModel order, string message, string smsCode, UserModel user)
        {
            orderService.SetSMSAndSMSCodeAsync(order, message, smsCode);
            orderService.SetStatusAsync(order, OrderStatus.STATUS_OK);
            userService.TakePaymentAsync(user.Id, pricesService.GetServicePriceByServiceId(order.Service));
        }

        public async Task ResetOrder(OrderModel order, UserModel user, string message)
        {
            historyService.InputNewHistoryAsync(user.Id, HistoryType.InvalidExpressions, $"service: {order.Service} - all regexes failed, message: ${message}");
            orderService.SetStatusAsync(order, OrderStatus.STATUS_FREE);
        }

    }
}