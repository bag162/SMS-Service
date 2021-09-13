using Backend.Implemantation.IServices;
using Backend.Models.DB.Models;
using Backend.Models.Implementation.Models.JsonModels;
using Backend.TarantoolDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Implemantation.Services
{
    public class QueueService : IQueueService
    {
        public QueueRepository queueRepository;

        public QueueService(QueueRepository queueRepository)
        {
            this.queueRepository = queueRepository;
        }

        public Task CreateQueue(QueueModel task)
        {
            throw new NotImplementedException();
        }

        public Task DeleteQueue(QueueModel task)
        {
            throw new NotImplementedException();
        }

        public Task<TaskModel> GetTask()
        {
            throw new NotImplementedException();
        }

        public Task UpdateDataQueue(TaskModel data)
        {
            throw new NotImplementedException();
        }
    }
}
