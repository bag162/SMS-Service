using Backend.Implemantation.IServices;
using Backend.Models.DB.Models;
using Backend.Models.Implementation.Models.Enums;
using Backend.Models.Implementation.Models.HistoryInputModels;
using Backend.Models.Implementation.Models.JsonModels;
using Implemantation.IServices;
using Microsoft.Extensions.Options;
using Models.ImplementationModels.Enums;
using SMS_Service_Worker.Common.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.TaskMonitor
{
    public class TaskCreatorWorker
    {
        private readonly IQueueService queueService;
        private readonly IOrderService orderService;
        private readonly IHistoryService historyService;
        private readonly IOptions<ConfigurationClass> Config;

        public TaskCreatorWorker(IQueueService queueService,
            IOrderService orderService,
            IHistoryService historyService,
            IOptions<ConfigurationClass> Config)
        {
            this.historyService = historyService;
            this.orderService = orderService;
            this.queueService = queueService;
            this.Config = Config;
        }

        public async Task CheckTaskForCreateQueue()
        {
            while (true)
            {
                var allOrders = await orderService.GetActiveOrdersAsync();
                var allQueue = await queueService.GetAllQueueAsync();
                var dataQueues = new List<TaskModel>();
                double currentTime = DateTime.Now.TimeOfDay.TotalSeconds;
                var addedQueues = new List<QueueModel>();

                foreach (var queue in allQueue)
                {
                    if (queue.Data != "not")
                    {
                        dataQueues.Add(JsonSerializer.Deserialize<TaskModel>(queue.Data));
                    }
                }

                foreach (var order in allOrders)
                {
                    var data = JsonSerializer.Deserialize<TaskModel>(order.JsonData);
                    if (!dataQueues.Select(x => x.Order.Id).Contains(data.Order.Id))
                    {
                        double orderTime = Convert.ToDouble(order.StartDateTime);
                        if ((currentTime - orderTime) / 60 > Config.Value.SMSWorkerSettings.SMSWaitTime && orderTime != 0)
                        {
                            var newHistory = new HistoryJsonModel() { Account = data.Account, TimeIncident = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds };
                            historyService.InputNewHistoryAsync(data.User.Id, HistoryType.NotComSMS, newHistory);
                            orderService.SetStatusAsync(data.Order, OrderStatus.STATUS_CANCEL);
                            continue;
                        }

                        if (order.LastCheckTime == 0)
                        {
                            order.LastCheckTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                        }
                        else
                        {
                            double interval = (DateTime.Now.TimeOfDay.TotalSeconds - Convert.ToDouble(order.LastCheckTime));
                            if (interval < Config.Value.SMSWorkerSettings.TimeBetweenRequests)
                                continue;
                        }

                        var priority = (data.FirstMessage == null) ? 5 : 1;
                        
                        var addedQueue = new QueueModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Type = (int)QueueType.Order,
                            Data = order.JsonData,
                            Priority = priority,
                            Bucket = 1000
                        };
                        var updatedOrder = order;
                        updatedOrder.LastCheckTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                        orderService.UpdateOrderAsync(updatedOrder);
                        addedQueues.Add(addedQueue);
                    }
                }
                if (addedQueues.Count() != 0)
                {
                    queueService.CreateQueuesAsync(addedQueues);
                    historyService.InputNewHistoryAsync("0", HistoryType.AddQueues, $"Added {addedQueues.Count()} queues");
                }

                await Task.Delay(1000);
            }
        }

    }
}