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
        public Task<QueueModel[]> GetAllQueueAsync(int bucket = 1000);
        public Task<QueueModel[]> GetTaskAsync(int count, int bucket = 1000);

        public Task DeleteQueueAsync(QueueModel task);

        public Task CreateQueueAsync(QueueModel task);
        public Task CreateQueuesAsync(List<QueueModel> tasks);
    }
}