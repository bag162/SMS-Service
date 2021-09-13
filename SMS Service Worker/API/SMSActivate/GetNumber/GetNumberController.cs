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

namespace SMS_Service_Worker.API.PrivateWEB.GetNumber.Controllers
{
    public class GetNumberController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IHistoryService historyService;
        private readonly IServicePricesService servicePricesService;
        private readonly IAccountService accountService;
        private readonly SMSWorker smsWorker;
        public readonly Random Randomizer = new();

        public GetNumberController(
            IUserService userService,
            IOrderService orderService, 
            IHistoryService historyService,
            IServicePricesService servicePricesService,
            IAccountService accountService,
            SMSWorker smsWorker)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.historyService = historyService;
            this.servicePricesService = servicePricesService;
            this.smsWorker = smsWorker;
            this.accountService = accountService;
        }

        [HttpGet]
        public async Task<ContentResult> GetNumber(string api_key, string service)
        {
            UserModel user = await userService.GetUserByApiKeyAsync(api_key);
            long requestServiceId = servicePricesService.GetAllServices()
                .Where(x => x.ServicePrefix == service)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (requestServiceId == 0)
            {
                await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.GetNumber_Fail);
                return new ContentResult { Content = "BAD_SERVICE", StatusCode = 404 };
            }
            if (!userService.CheckBalanceUser(user, requestServiceId))
            {
                await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.GetNumber_Fail);
                return new ContentResult { Content = "NO_BALANCE", StatusCode = 401 };
            }

            OrderModel order = orderService.GetAllOrdersByServiceAsync(requestServiceId).Result.ToList()
                .AsQueryable()
                .Where(x => x.Status == (int)OrderStatuses.STATUS_FREE)
                .FirstOrDefault();

            if (order == null)
            {
                await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.GetNumber_Fail);
                return new ContentResult { Content = "NO_NUMBER", StatusCode = 401 };
            }

            int orderId = Randomizer.Next(10000000, 99999999);
            string dateTime = DateTime.Now.ToString();
            OrderModel updatedOrder = new()
            {
                Id = order.Id,
                Status = (int)OrderStatuses.STATUS_WAIT_CODE,
                Number = order.Number,
                OrderId = orderId,
                Service = order.Service,
                StartDateTime = dateTime,
                UserId = user.Id
            };

            await orderService.UpdateOrderAsync(updatedOrder);
            await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.GetNumber);
            return new ContentResult { Content = "ACCESS_NUMBER:" + order.OrderId + ":" + order.Number, StatusCode = 200 };
        }
    }
}