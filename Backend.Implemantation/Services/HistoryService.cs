﻿using DBInfrastructure;
using Models.DTO.DTOModels;
using Implemantation.IServices;
using System;
using System.Threading.Tasks;
using TarantoolDB.Repositories;
using Models.ImplementationModels.Enums;
using Backend.Models.Implementation.Models.HistoryInputModels;
using System.Text.Json;

namespace Implemantation.Services
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
                TimeIncident = DateTime.Now.ToString()
            };

            historyRepository.Create(new HistoryModel() {
                Id = Guid.NewGuid().ToString(),
                TypeRequest = (int)typeRequest, 
                RequestTime = DateTime.Now.ToString(), 
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
                RequestTime = DateTime.Now.ToString(),
                UserId = userID,
                Message = JsonSerializer.Serialize(messages),
                Bucket = bucket
            });

            return;
        }
    }
}