using Backend.Implemantation.IServices;
using Backend.Models.DB.Models;
using Backend.Models.Implementation.Models.JsonModels;
using Backend.TarantoolDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        public async Task CreateQueueAsync(QueueModel task)
        {
            queueRepository.Create(task, (int)task.Bucket);
            return;
        }
        public async Task CreateQueuesAsync(List<QueueModel> tasks)
        {
            foreach (var task in tasks)
            {
                queueRepository.Create(task, (int)task.Bucket);
            }

            return;
        }

        public async Task DeleteQueueAsync(QueueModel task)
        {
            queueRepository.Delete(task, (int)task.Bucket);
            return;
        }

        public async Task<QueueModel[]> GetAllQueueAsync(int bucket = 1000)
        {
            return queueRepository.FindAll(bucket).Result.ToArray();
        }
        public async Task<QueueModel[]> GetTaskAsync(int count, int bucket = 1000)
        {
            return queueRepository.GetTasks(count, bucket).Result.ToArray();
        }
    }
}
