using System.Threading.Tasks;

namespace Implemantation.IServices
{
    public interface IHistoryService
    {
        public Task InputNewHistoryAsync(string userID, int typeRequest, string message = null);
    }
}
