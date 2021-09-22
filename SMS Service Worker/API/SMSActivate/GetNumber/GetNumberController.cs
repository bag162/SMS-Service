using System.Linq;
using System.Threading.Tasks;
using Models.DTO.DTOModels;
using Hangfire;
using Implemantation.IServices;
using Models.ImplementationModels;
using Models.ImplementationModels.Enums;
using Microsoft.AspNetCore.Mvc;
using SMS_Service_Worker.Workers.SMSWorker;
using System;
using Backend.Models.Implementation.Models.JsonModels;
using System.Text.Json;

namespace SMS_Service_Worker.API.PrivateWEB.GetNumber.Controllers
{
    public class GetNumberController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IHistoryService historyService;
        private readonly IServicePricesService servicePricesService;
        private readonly IAccountService accountService;
        private readonly IProxyService proxyService;
        public readonly Random Randomizer = new();

        public GetNumberController(
            IUserService userService,
            IOrderService orderService, 
            IHistoryService historyService,
            IServicePricesService servicePricesService,
            IAccountService accountService,
            IProxyService proxyService)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.historyService = historyService;
            this.servicePricesService = servicePricesService;
            this.accountService = accountService;
            this.proxyService = proxyService;
        }

        [HttpGet]
        public async Task<ContentResult> GetNumber(string api_key, string service)
        {
            UserModel user = userService.GetUserByApiKey(api_key);
            long requestServiceId = servicePricesService.GetAllServices()
                .Where(x => x.ServicePrefix == service)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (requestServiceId == 0)
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.GetNumber_Fail);
                return new ContentResult { Content = "BAD_SERVICE", StatusCode = 404 };
            }
            if (!userService.CheckBalanceUser(user, requestServiceId))
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.GetNumber_Fail);
                return new ContentResult { Content = "NO_BALANCE", StatusCode = 401 };
            }
            var order = await orderService.GetNumberAsync((int)requestServiceId);

            if (order == null)
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.GetNumber_Fail);
                return new ContentResult { Content = "NO_NUMBER", StatusCode = 401 };
            }

            int orderId = Randomizer.Next(10000000, 99999999);

            OrderModel updatedOrder = new()
            {
                Id = order.Id,
                Status = (int)OrderStatus.STATUS_WAIT_CODE,
                Number = order.Number,
                OrderId = orderId,
                Service = order.Service,
                StartDateTime = DateTime.Now.ToString(),
                UserId = user.Id,
                Bucket = 4000
            };
            var data = new TaskModel() 
            {
                Account = accountService.GetAccountByNumberAsync(order.Number).Result,
                Proxy = await proxyService.GetRandomProxyAsync(),
                User = user,
                Order = updatedOrder
            };
            updatedOrder.JsonData = JsonSerializer.Serialize(data);

            orderService.UpdateOrderAsync(updatedOrder);
            historyService.InputNewHistoryAsync(user.Id, HistoryType.GetNumber);
            return new ContentResult { Content = "ACCESS_NUMBER:" + updatedOrder.OrderId + ":" + order.Number, StatusCode = 200 };
        }
    }
}