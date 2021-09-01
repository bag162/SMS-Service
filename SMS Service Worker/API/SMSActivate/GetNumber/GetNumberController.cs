using System.Linq;
using System.Threading.Tasks;
using DBInfrastructure.DTOModels;
using Hangfire;
using Implemantation.IServices;
using Implemantation.Models;
using Implemantation.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using SMS_Service_Worker.Workers.SMSWorker;

namespace SMS_Service_Worker.API.PrivateWEB.GetNumber.Controllers
{
    public class GetNumberController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IHistoryService historyService;
        private readonly IServicePricesService servicePricesService;
        private readonly SMSWorker smsWorker;

        public GetNumberController(
            IUserService userService,
            IOrderService orderService, 
            IHistoryService historyService,
            IServicePricesService servicePricesService,
            SMSWorker smsWorker)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.historyService = historyService;
            this.servicePricesService = servicePricesService;
            this.smsWorker = smsWorker;
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
                return new ContentResult { Content = "BAD_SERVICE", StatusCode = 404 };
            }

            if (!userService.CheckBalanceUser(user, requestServiceId))
            {
                return new ContentResult { Content = "NO_BALANCE", StatusCode = 401 };
            } // проверка есть ли у юзера деньги на покупку номера

            GetNumberModel order = await orderService.GetNumberAsync(new GetNumberModel { Service = requestServiceId, User = user }); // получение номера для юзера

            if (!order.Success)
            {
                await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.GetNumber_Fail);

                return new ContentResult { Content = order.FailMessage, StatusCode = order.StatusCode };
            } // если возникла ошибка во время получения номера, респонсим ответ

            await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.GetNumber);
            // Task.Factory.StartNew(() => smsWorker.StartSMSWorker(order))
            BackgroundJob.Enqueue<SMSWorker>(x => x.StartSMSWorker(order));
            return new ContentResult { Content = "ACCESS_NUMBER:" + order.OrderId + ":" + order.Number, StatusCode = 200 }; // ACCESS_NUMBER:123456:375299154283
        }
    }
}
