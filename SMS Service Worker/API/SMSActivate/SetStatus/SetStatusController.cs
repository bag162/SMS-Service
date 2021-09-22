using Models.DTO.DTOModels;
using Implemantation.IServices;
using Models.ImplementationModels;
using Models.ImplementationModels.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SMS_Service_Worker.API.PrivateWEB.SetStatus
{
    public class SetStatusController : Controller
    {

        private readonly IUserService userService;
        private readonly IOrderService orderService;
        public IHistoryService historyService;

        public SetStatusController(IUserService userService, IOrderService orderService, IHistoryService historyService)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.historyService = historyService;
        }


        [HttpGet]
        public async Task<ContentResult> SetStatus(string api_key, int id, int status)
        {
            UserModel user = userService.GetUserByApiKey(api_key);
            OrderModel order = await orderService.GetOrderByOrderIdAsync(id);
            // если такого ордера нет
            if (order == null)
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.SetStatus_Fail);
                return new ContentResult { Content = "NO_ACTIVATION", StatusCode = 405 };
            }
            // если ордер взят другим пользователем
            if (order.UserId != user.Id)
            {
                await historyService.InputNewHistoryAsync(user.Id, HistoryType.SetStatus_Fail);
                return new ContentResult { Content = "NO_ACTIVATION", StatusCode = 405 };
            }
            // если невозможно установить статус в связи с текущим
            if (order.Status != 1)
                return new ContentResult { Content = "BAD_STATUS", StatusCode = 406 };

            // загрушки на неподдерживаемые запросы
            switch (status)
            {
                case 1:
                    return new ContentResult { Content = "ACCESS_READY" }; // если статус 1, то ответ всегда один и тот же, т.к. в этой системе этот запрос не нужен, и он вставлен как заглушка
                case 3:
                    return new ContentResult { Content = "BAD_STATUS", StatusCode = 406 }; // если статус 3, то выдаём неверный статус, т.к. в этой системе этот запрос ПОКА ЧТО не работает
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
    }
}