using Models.DTO.DTOModels;
using Implemantation.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Models.ImplementationModels.Enums;
using System.Linq;
using System;
using Backend.Models.Implementation.Models.JsonModels;
using System.Text.Json;

namespace SMS_Service_Worker.API.PrivateWEB
{
    public class SMSActivateController : Controller
    {
        private readonly IUserService userService;
        private readonly IServicePricesService servicePricesService;
        private readonly IHistoryService historyService;
        private readonly IOrderService orderService;
        private readonly IAccountService accountService;
        private readonly IProxyService proxyService;
        private readonly Random Randomizer = new();

        public SMSActivateController(
            IUserService userService,
            IServicePricesService servicePricesService,
            IHistoryService historyService,
            IOrderService orderService,
            IAccountService accountService,
            IProxyService proxyService) 
        {
            this.userService = userService;
            this.servicePricesService = servicePricesService;
            this.historyService = historyService;
            this.orderService = orderService;
            this.accountService = accountService;
            this.proxyService = proxyService;
        }


        [HttpGet]
        [Route("stubs/handler_api.php")]
        public async Task<ContentResult> RoutingController(string api_key, [FromQuery] string action, string service, int id, int status)
        {
            UserModel user = userService.GetUserByApiKey(api_key);
            if (user == null)
            {
                return BadKey();
            }

            switch (action)
            {
                case "getNumber":
                    return await GetNumber(api_key, service);

                case "getStatus":
                    return await GetStatus(api_key, id);

                case "setStatus":
                    return await SetStatus(api_key, id, status);

                case "getBalance":
                    return GetBalance(api_key);

                default:
                    return BadAction(); // если такого экшена нет, то редирект на bad action response
            }
        }

        public ContentResult GetBalance(string api_key)
        {
            UserModel user = userService.GetUserByApiKey(api_key);
            return new ContentResult { Content = "ACCESS_BALANCE:" + user.Balance };
        }
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
                return new ContentResult { Content = "BAD_SERVICE", StatusCode = 400 };
            }
            if (!userService.CheckBalanceUser(user, requestServiceId))
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.GetNumber_Fail);
                return new ContentResult { Content = "NO_BALANCE", StatusCode = 402 };
            }
            var order = await orderService.GetNumberAsync((int)requestServiceId);

            if (order == null)
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.GetNumber_Fail);
                return new ContentResult { Content = "NO_NUMBER", StatusCode = 418 };
            }

            int orderId = Randomizer.Next(10000000, 99999999);

            OrderModel updatedOrder = new()
            {
                Id = order.Id,
                Status = (int)OrderStatus.STATUS_WAIT_CODE,
                Number = order.Number,
                OrderId = orderId,
                Service = order.Service,
                StartDateTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
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
        public async Task<ContentResult> GetStatus(string api_key, int id)
        {
            UserModel user = userService.GetUserByApiKey(api_key);
            OrderModel order = await orderService.GetOrderByOrderIdAsync(id);

            if (order == null || order.UserId != user.Id)
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.GetStatus_Fail);
                return new ContentResult { Content = "NO_ACTIVATION", StatusCode = 403 };
            }

            await historyService.InputNewHistoryAsync(user.Id, HistoryType.GetStatus);
            if (order.Status == 6)
            {
                return new ContentResult { Content = Enum.GetName(typeof(OrderStatus), order.Status) + ":" + order.SMSCode };
            }
            return new ContentResult { Content = Enum.GetName(typeof(OrderStatus), order.Status) };
        }
        public async Task<ContentResult> SetStatus(string api_key, int id, int status)
        {
            UserModel user = userService.GetUserByApiKey(api_key);
            OrderModel order = await orderService.GetOrderByOrderIdAsync(id);
            // если такого ордера нет
            if (order == null)
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.SetStatus_Fail);
                return new ContentResult { Content = "NO_ACTIVATION", StatusCode = 403 };
            }
            // если ордер взят другим пользователем
            if (order.UserId != user.Id)
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.SetStatus_Fail);
                return new ContentResult { Content = "NO_ACTIVATION", StatusCode = 403 };
            }
            // если невозможно установить статус в связи с текущим
            if (order.Status != 1)
                return new ContentResult { Content = "BAD_STATUS", StatusCode = 400 };

            // загрушки на неподдерживаемые запросы
            switch (status)
            {
                case 1:
                    return new ContentResult { Content = "ACCESS_READY" }; // если статус 1, то ответ всегда один и тот же, т.к. в этой системе этот запрос не нужен, и он вставлен как заглушка
                case 3:
                    return new ContentResult { Content = "BAD_STATUS", StatusCode = 400 }; // если статус 3, то выдаём неверный статус, т.к. в этой системе этот запрос ПОКА ЧТО не работает
            }
            if (order.SMS == null || order.SMS == "" || order.SMS == "null")
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.SetStatus_8);
                await orderService.SetStatusAsync(order, OrderStatus.STATUS_CANCEL);
            }
            else
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.SetStatus_6);
                await orderService.SetStatusAsync(order, OrderStatus.STATUS_OK);
            }

            return new ContentResult { Content = "ACCESS_CANCEL" };
        }

        public ContentResult BadKey()
        {
            return new ContentResult { Content = "BAD_KEY", StatusCode = 403 };
        }
        public ContentResult BadAction()
        {
            return new ContentResult { Content = "BAD_ACTION", StatusCode = 400 };
        }
        public ContentResult BasStatus()
        {
            return new ContentResult { Content = "BAD_STATUS", StatusCode = 400 };
        }
    }
}