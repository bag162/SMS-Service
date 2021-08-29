using DBInfrastructure.DTOModels;
using Implemantation.IServices;
using Implemantation.Models;
using Implemantation.Models.Enums;
using Implemantation.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SMS_Service_Worker.Common.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Implemantation.Models.JsonModels.TNMessages;

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

        public async Task StartSMSWorker(GetNumberModel numberModel)
        {
            OrderModel order = orderService.GetOrderByOrderIdAsync(numberModel.OrderId).Result;
            if (order == null)
            {
                return;
            }
            AccountModel account = await accountService.GetAccountByNumberAsync(order.Number);
            HttpClientHandler handler = handlerConveer.GetHandler(account).Result.handler;

            if (handler == null)
            {
                await orderService.SetStatusAsync(order, (int)OrderStatuses.STATUS_CANCEL);
                return;
            }

            HttpClient client = new(handler);

            var firstRequest = client.GetAsync(Config.Value.TextNowRoutes.GetMessageURI).Result.Content;
            var oldMessageString = firstRequest.ReadAsStringAsync().Result;

            ErrorCode errorResponse;
            try
            {
                errorResponse = JsonConvert.DeserializeObject<ErrorCode>(oldMessageString);
            }
            catch
            {
                errorResponse = new ErrorCode { error_code = "not" };
            }
            
            if (errorResponse.error_code == Config.Value.Common.ErrorResponse)
            {
                await orderService.SetStatusAsync(order, (int)OrderStatuses.STATUS_CANCEL);
                await accountService.DeactivateAccountAsync(account);
                await historyService.InputNewHistoryAsync("0", (int)TypeRequests.CookieInactive, "Inactive cookie");
                return;
            }

            var serializeOldMessage = JsonConvert.DeserializeObject<JsonMessages>(oldMessageString).result.FirstOrDefault();
            JsonExpression ExpressionList = pricesService.GetAllExpressionByServiceId(order.Service);

            DateTime orderDateTime = DateTime.Parse(order.StartDateTime);
            orderDateTime = orderDateTime.AddMinutes(Config.Value.SMSWorkerSettings.SMSWaitTime);

            while(true)
            {
                if (orderService.GetOrderByOrderIdAsync(order.OrderId).Result.Status != (int)OrderStatuses.STATUS_WAIT_CODE)
                {
                    return;
                }

                await Task.Delay(Config.Value.SMSWorkerSettings.TimeBetweenRequests);

                var messageRequest = client.GetAsync(Config.Value.TextNowRoutes.GetMessageURI).Result.Content;
                var serializeNewMessage = JsonConvert.DeserializeObject<JsonMessages>(messageRequest.ReadAsStringAsync().Result).result.FirstOrDefault();

                if (serializeNewMessage.preview.message != serializeOldMessage.preview.message)
                {
                    string smsCode = CheckForRegularExpressions(ExpressionList.Expressions, serializeNewMessage.preview.message);
                    if (smsCode != null)
                    {
                        await orderService.SetSMSAndSMSCodeAsync(order, serializeNewMessage.preview.message, smsCode);
                        await orderService.SetStatusAsync(order, (int)OrderStatuses.STATUS_OK);
                        await userService.TakePaymentAsync(numberModel.User.Id, pricesService.GetServicePriceByServiceId(order.Service));
                        return;
                    }
                    else
                    {
                        await historyService.InputNewHistoryAsync(numberModel.User.Id, (int)TypeRequests.InvalidExpressions, $"service: {order.Service} - all regexes failed, message: ${serializeNewMessage.preview.message}");
                        await orderService.SetStatusAsync(order, (int)OrderStatuses.STATUS_FREE);
                        return;
                    }
                    
                }

                if (DateTime.Now > orderDateTime)
                {
                    await historyService.InputNewHistoryAsync(numberModel.User.Id, (int)TypeRequests.NotComSMS, $"Service: {order.Service}, number: {order.Number}");
                    await orderService.SetStatusAsync(order, (int)OrderStatuses.STATUS_CANCEL);
                    return;
                }
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

    }
}
