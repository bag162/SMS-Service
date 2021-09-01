using DBInfrastructure;
using DBInfrastructure.DTOModels;
using Implemantation.IServices;
using System;
using System.Threading.Tasks;
using TarantoolDB.Repositories;

namespace Implemantation.Services
{
    public class HistoryService : IHistoryService
    {
        public HistoryRepository historyRepository;

        public HistoryService(HistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }

        public async Task InputNewHistoryAsync(string userID, int typeRequest, string message)
        {
            await historyRepository.Create(new HistoryModel() { Id = Guid.NewGuid().ToString(), TypeRequest = typeRequest, RequestTime = DateTime.Now.ToString(), UserId = userID, Message = message });
            return;
        }
    }
}
