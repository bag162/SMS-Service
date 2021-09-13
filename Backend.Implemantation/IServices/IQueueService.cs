using Backend.Models.DB.Models;
using Backend.Models.Implementation.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Implemantation.IServices
{
    public interface IQueueService
    {
        public Task<TaskModel> GetTask();

        public Task DeleteQueue(QueueModel task);

        public Task CreateQueue(QueueModel task);

        public Task UpdateDataQueue(TaskModel data);
    }
}