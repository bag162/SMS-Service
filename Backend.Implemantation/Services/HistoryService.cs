using Backend.Models.DB;
using Backend.Implemantation.IServices;
using System;
using System.Threading.Tasks;
using Backend.TarantoolDB.Repositories;
using Backend.Models.Implementation.Enums;
using Backend.Models.Implementation.HistoryInputModels;
using System.Text.Json;

namespace Backend.Implemantation.Services
{
    public class HistoryService : IHistoryService
    {
        public HistoryRepository historyRepository;

        public HistoryService(HistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }

        public async Task InputNewHistoryAsync(string userID, HistoryType typeRequest, string message, int bucket = 6000)
        {
            var insertedMessage = new HistoryJsonModel() 
            {
                Message = message,
                TimeIncident = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
            };

            historyRepository.Create(new HistoryModel() {
                Id = Guid.NewGuid().ToString(),
                TypeRequest = (int)typeRequest, 
                RequestTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds, 
                UserId = userID, 
                Message = JsonSerializer.Serialize(insertedMessage),
                Bucket = bucket });

            return;
        }
        public async Task InputNewHistoryAsync(string userID, HistoryType typeRequest, HistoryJsonModel messages, int bucket = 6000)
        {
            historyRepository.Create(new HistoryModel()
            {
                Id = Guid.NewGuid().ToString(),
                TypeRequest = (int)typeRequest,
                RequestTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                UserId = userID,
                Message = JsonSerializer.Serialize(messages),
                Bucket = bucket
            });

            return;
        }
    }
}