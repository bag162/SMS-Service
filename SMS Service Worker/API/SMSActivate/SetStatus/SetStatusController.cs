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
            UserModel user = await userService.GetUserByApiKeyAsync(api_key);
            OrderModel order = await orderService.GetOrderByOrderIdAsync(id);
            if (order == null)
            {
                await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.SetStatus_Fail);
                return new ContentResult { Content = "NO_ACTIVATION", StatusCode = 405 };
            }

            if (order.Status != 1 && order.Status != 6)
            {
                return new ContentResult { Content = "BAD_STATUS", StatusCode = 406 };
            } // если у ордера статус не "ожидание смс" и не "смс получено" то выдать ошибку, т.к. смысла нет проводить запрос

            switch (status)
            {
                case 1:
                    return new ContentResult { Content = "ACCESS_READY " }; // если статус 1, то ответ всегда один и тот же, т.к. в этой системе этот запрос не нужен, и он вставлен как заглушка
                case 3:
                    return new ContentResult { Content = "BAD_STATUS", StatusCode = 406 }; // если статус 3, то выдаём неверный статус, т.к. в этой системе этот запрос ПОКА ЧТО не работает
            }

            if (order.UserId != user.Id)
            {
                await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.SetStatus_Fail);
                return new ContentResult { Content = "NO_ACTIVATION", StatusCode = 405 };
            } // если id юзера из ордера и id юзера с апи ключа не совпадает, то отдать ошибку

            if (order.SMS == null || order.SMS == "")
            {
                await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.Setstatus_8);
                await orderService.SetStatusAsync(order, (int)OrderStatuses.STATUS_CANCEL);
            } // если у ордера нет смс, то ставится статус 8
            else
            {
                await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.SetStatus_6);
                await orderService.SetStatusAsync(order, (int)OrderStatuses.STATUS_OK);
            } // если есть, то 6

            return new ContentResult { Content = "ACCESS_CANCEL" };
        }
    }
}
