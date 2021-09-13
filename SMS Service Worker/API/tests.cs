using Models.DTO.DTOModels;
using Implemantation.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using ProGaudi.Tarantool.Client;
using Backend.TarantoolDB.Repositories;
using System.Collections.Generic;
using Backend.Models.Implementation.Models.MsgPackModels;

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
        public tests(IAccountService accountService,
            IUserService userService,
            IProxyService proxyService,
            IOrderService orderService,
            IServicePricesService servicePricesService,
            IHistoryService historyService,
            Box box,
             QueueRepository queueRepository)
        {
            this.accountService = accountService;
            this.userService = userService;
            this.proxyService = proxyService;
            this.orderService = orderService;
            this.servicePricesService = servicePricesService;
            this.historyService = historyService;
            this.box = box;
            this.queueRepository = queueRepository;
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

        class JsonRequest
        {
            public ServiceModel service { get; set; }
            public Dictionary<int,string> numeric { get; set; }
        }

        [HttpGet]
        [Route("testRouter")]
        public async Task testRouter()
        {
            var service = new ServiceModel() { Id = randomizer.Next(100, 2000), Price = 5, RegularExpressions = "asd", ServicePrefix = randomizer.Next(100,1000).ToString(), Bucket = Guid.NewGuid().ToString() };
            var jsonData = GenerateJsonData(service);
            var jsonRequest = new JSONRequest() { JSON = jsonData };

            // create
            var result = await box.Call<JSONRequest, ServiceModel>("Insert", jsonRequest);

            // getAll
            var result4 = await box.Call<JSONRequest, ServiceModel[]>("GetAll", jsonRequest);

            // update
            service.Price = 600;
            jsonRequest = new JSONRequest() { JSON = GenerateJsonData(service) };
            var result1 = await box.Call<JSONRequest, ServiceModel[]>("Update", jsonRequest);

            // getAll
            var result3 = await box.Call<JSONRequest, ServiceModel[]>("GetAll", jsonRequest);

            // delete
            var result2 = await box.Call<JSONRequest, ServiceModel>("Delete", jsonRequest);

            // getAll
            var result5 = await box.Call<JSONRequest, ServiceModel[]>("GetAll", jsonRequest);

            return;
        }

        private string GenerateJsonData(ServiceModel model = null)
        {
            var jsonPropertyes = new Dictionary<int, string>();
            if (model != null)
            {
                var propertyes = model.GetType().GetProperties();

                for (int i = 0; i < propertyes.Length; i++)
                {
                    jsonPropertyes.Add(i + 1, propertyes[i].Name);
                }
            }

            var jsonModel = new JsonRequestModel() { numeric = jsonPropertyes, model = model, space_name = "service" };
            return JsonSerializer.Serialize<JsonRequestModel>(jsonModel);
        }
        private class JsonRequestModel
        {
            public string space_name { get; set; }
            public ServiceModel model { get; set; }
            public Dictionary<int, string> numeric { get; set; }
        }
        private enum CRUDOperations
        {
            GetAll = 1,
            Get = 2,
            Insert = 3,
            Update = 4,
            Delete = 5
        }
    }
}