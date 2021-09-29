using Backend.Backend.Implemantation.IServices;
using Backend.Models.Implementation.JsonModels;
using DalSoft.Hosting.BackgroundQueue;
using SMS_Service_Worker.Workers.CheckerDBWorker;
using SMS_Service_Worker.Workers.CheckProxyValid;
using SMS_Service_Worker.Workers.SMSWorker;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.TaskMonitor
{
    public class TaskMonitorWorker
    {
        private readonly IQueueService queueService;
        private SMSWorker smsWorker { get; set; }
        private readonly CheckProxyValidWorker checkProxyValidWorker;
        private readonly CheckAccountValidWorker checkAccountValidWorker;
        private readonly CheckDBWorker checkDBWorker;
        private readonly BackgroundQueue backgroundQueue;

        public TaskMonitorWorker(IQueueService queueService,
            SMSWorker smsWorker,
            CheckProxyValidWorker checkProxyValidWorker,
            CheckAccountValidWorker checkAccountValidWorker,
            CheckDBWorker checkDBWorker,
            BackgroundQueue backgroundQueue)
        {

            this.queueService = queueService;
            this.smsWorker = smsWorker;
            this.checkProxyValidWorker = checkProxyValidWorker;
            this.checkAccountValidWorker = checkAccountValidWorker;
            this.checkDBWorker = checkDBWorker;
            this.backgroundQueue = backgroundQueue;
        }

        public async Task StartTaskMonitor()
        {
            while (true)
            {
                var tasks = queueService.GetTaskAsync(50).Result;
                var StartingTasks = new List<Task>();
                foreach (var task in tasks)
                {
                    switch (task.Type)
                    {
                        case 1:
                            var queueTask = JsonSerializer.Deserialize<TaskModel>(task.Data);
                            StartingTasks.Add(smsWorker.StartSMSWorker(queueTask.Order, queueTask.User, queueTask.Account, queueTask.Proxy, queueTask.FirstMessage));
                            break;
                        case 2:
                            StartingTasks.Add(checkProxyValidWorker.CheckProxyValid());
                            break;
                        case 3:
                            StartingTasks.Add(checkDBWorker.CheckDBInsert());
                            break;
                        case 4:
                            StartingTasks.Add(checkDBWorker.CheckDBDelete());
                            break;
                        case 5:
                            StartingTasks.Add(checkAccountValidWorker.CheckAccountsOnValid());
                            break;
                    }
                }

                foreach (var startTask in StartingTasks)
                {
                    backgroundQueue.Enqueue(async cancellationToken =>
                    {
                        await startTask;
                    });
                }
                await Task.Delay(1000);
            }
        }
    }
}