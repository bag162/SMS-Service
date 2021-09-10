using Models.DTO.DTOModels;
using Implemantation.IServices;
using Models.ImplementationModels;
using Models.ImplementationModels.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SMS_Service_Worker.API.PrivateWEB.GetStatus
{
    public class GetStatusController : Controller
    {
        private readonly IHistoryService historyService;
        private readonly IOrderService orderService;
        private readonly IUserService userService;

        public GetStatusController(IOrderService orderService, IUserService userService, IHistoryService historyService)
        {
            this.orderService = orderService;
            this.userService = userService;
            this.historyService = historyService;
        }

        [HttpGet]
        public async Task<ContentResult> GetStatus(string api_key, int id)
        {
            UserModel user = await userService.GetUserByApiKeyAsync(api_key);
            OrderModel order = await orderService.GetOrderByOrderIdAsync(id);

            if (order == null || order.UserId != user.Id)
            {
                await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.GetStatus_Fail);
                return new ContentResult { Content = "NO_ACTIVATION", StatusCode = 405 };
            }

            await historyService.InputNewHistoryAsync(user.Id, (int)TypeRequests.GetStatus);
            if (order.Status == 6)
            {
                return new ContentResult { Content = Enum.GetName(typeof(OrderStatuses), order.Status) + ":" + order.SMSCode };
            }
            return new ContentResult { Content = Enum.GetName(typeof(OrderStatuses), order.Status) };
        }
    }
}
