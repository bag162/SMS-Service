using Backend.Implemantation.IServices;
using Backend.Models.Implementation.Models.JsonModels;
using SMS_Service_Worker.Workers.SMSWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.TaskMonitor
{
    class TaskMonitorWorker
    {
        private readonly IQueueService queueService;
        private SMSWorker smsWorker { get; set; }
        public TaskMonitorWorker(IQueueService queueService,
            SMSWorker smsWorker)
        {
            this.queueService = queueService;
            this.smsWorker = smsWorker;
        }
        public async Task StartTaskMonitor()
        {
            while (true)
            {
                TaskModel task = await queueService.GetTask();
                if (task != null)
                {
                    smsWorker.StartSMSWorker(task.Order, task.User, task.Account, task.Proxy, task.FirstMessage);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
