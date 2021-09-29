using Backend.Models.DB;
using Backend.Implemantation.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using ProGaudi.Tarantool.Client;
using Backend.TarantoolDB.Repositories;
using System.Collections.Generic;
using Backend.DBInfrastructure;
using Backend.DBInfrastructure.Models;
using Backend.TarantoolDB.Repositories;
using System.Linq;
using static Backend.TarantoolDB.Repositories.ServiceRepository;
using Backend.Models.DB.Models;
using Backend.Models.Implementation.Enums;
using static Backend.TarantoolDB.Repositories.QueueRepository;

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
        private readonly Box box;
        private readonly QueueRepository queueRepository;
        private readonly ServiceRepository serviceRepository;
        private readonly OrderRepository orderRepository;

        public tests(IAccountService accountService,
            IUserService userService,
            IProxyService proxyService,
            IOrderService orderService,
            IServicePricesService servicePricesService,
            IHistoryService historyService,
            Box box,
            QueueRepository queueRepository,
            ServiceRepository serviceRepository,
            OrderRepository orderRepository)
        {
            this.accountService = accountService;
            this.userService = userService;
            this.proxyService = proxyService;
            this.orderService = orderService;
            this.servicePricesService = servicePricesService;
            this.historyService = historyService;
            this.box = box;
            this.queueRepository = queueRepository;
            this.serviceRepository = serviceRepository;
            this.orderRepository = orderRepository;
        }

        // delete in prod
        [HttpGet]
        [Route("getallorders")]
        public async Task<ContentResult> getallorders()
        {
            return new ContentResult { Content = JsonSerializer.Serialize(orderService.GetAllOrders()) };
        }

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
            await orderService.InsertOrderAsync(new OrderModel() { Id = Guid.NewGuid().ToString(), Number = randomizer.Next(1000).ToString(), OrderId = randomizer.Next(1000), Service = 2, SMS = "asd", SMSCode = "132", StartDateTime = 0, Status = 0, UserId = "userid" });
            return new ContentResult { Content = "ok" };
        }

        // delete in prod
        [HttpGet]
        [Route("getallproxy")]
        public async Task<ContentResult> getallproxy()
        {
            return new ContentResult { Content = JsonSerializer.Serialize(proxyService.GetAllProxyAsync().Result) };
        }

        [HttpGet]
        [Route("testRouter")]
        public async Task testRouter()
        {
            var insertedQueue = new QueueModel()
            {
                Id = randomizer.Next(500, 5000).ToString(),
                Type = (int)QueueType.Order,
                Data = randomizer.Next(500, 5000).ToString(),
                Bucket = 50, Priority = randomizer.Next(1, 10)};



            var insertedOrder = new OrderModel()
            {
                Bucket = 49,
                Id = Guid.NewGuid().ToString(),
                Number = "asdasdasdasd",
                OrderId = randomizer.Next(5,50000),
                Service = randomizer.Next(50,5000),
                StartDateTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                Status = 1,
                UserId = Guid.NewGuid().ToString(),
            };

            var queues = await queueRepository.FindAll();

            await orderRepository.Create(insertedOrder);
            return;
        }
    }
}