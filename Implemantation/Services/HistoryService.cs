using DBInfrastructure;
using DBInfrastructure.DTOModels;
using Implemantation.IServices;
using System;
using System.Threading.Tasks;

namespace Implemantation.Services
{
    public class HistoryService : IHistoryService
    {
        public TRepository<HistoryModel> historyRepository;

        public HistoryService(TRepository<HistoryModel> historyRepository)
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
