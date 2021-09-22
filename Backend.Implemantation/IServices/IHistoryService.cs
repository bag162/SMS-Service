using Backend.Models.Implementation.Models.HistoryInputModels;
using Models.ImplementationModels.Enums;
using System.Threading.Tasks;

namespace Implemantation.IServices
{
    public interface IHistoryService
    {
        public Task InputNewHistoryAsync(string userID, HistoryType typeRequest, string message = null, int bucket = 6000);
        public Task InputNewHistoryAsync(string userID, HistoryType typeRequest, HistoryJsonModel messages, int bucket = 6000);
    }
}