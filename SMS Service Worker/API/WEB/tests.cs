using DBInfrastructure.DTOModels;
using Implemantation.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SMS_Service_Worker.API.PrivateWEB
{
    public class tests : Controller
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        private readonly IProxyService proxyService;
        private readonly IOrderService orderService;
        private readonly IServicePricesService servicePricesService;
        private readonly IHistoryService historyService;
        private readonly Random randomizer = new();
        public tests(IAccountService accountService,
            IUserService userService,
            IProxyService proxyService,
            IOrderService orderService,
            IServicePricesService servicePricesService,
            IHistoryService historyService)
        {
            this.accountService = accountService;
            this.userService = userService;
            this.proxyService = proxyService;
            this.orderService = orderService;
            this.servicePricesService = servicePricesService;
            this.historyService = historyService;
        }

        // delete in prod
        [HttpGet]
        [Route("getallorders")]
        public async Task<ContentResult> getallorders()
        {
            return new ContentResult { Content = JsonSerializer.Serialize(orderService.GetAllOrders()) };
        }

       /* // delete in prod
        [HttpGet]
        [Route("getallhistory")]
        public async Task<ContentResult> getallhostiry()
        {
            return new ContentResult { Content = JsonSerializer.Serialize(historyEntity.FindAllString().Result.Data) };
        }*/

        // delete in prod
        [HttpGet]
        [Route("getallaccounts")]
        public async Task<ContentResult> getAllAccounts()
        {
            var result = accountService.GetAllAccounts();
            return new ContentResult { Content = JsonSerializer.Serialize(result) };
        }

        // delete in prod
        [HttpGet]
        [Route("insertorder")]
        public async Task<ContentResult> insertorder()
        {
            await orderService.InsertOrderAsync(new OrderModel() { Id = Guid.NewGuid().ToString(), Number = randomizer.Next(1000).ToString(), OrderId = randomizer.Next(1000), Service = 2, SMS = "asd", SMSCode = "132", StartDateTime = "", Status = 0, UserId = "userid" });
            return new ContentResult { Content = "ok" };
        }

        // delete in prod
        [HttpGet]
        [Route("getallproxy")]
        public async Task<ContentResult> getallproxy()
        {
            return new ContentResult { Content = JsonSerializer.Serialize(proxyService.GetAllProxy()) };
        }

       /* [HttpGet]
        [Route("getallusers")]
        public async Task<ContentResult> getallusers()
        {
            return new ContentResult { Content = JsonSerializer.Serialize(userService.getallusers) };
        }*/
    }
}
