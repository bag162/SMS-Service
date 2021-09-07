using Models.DTO.DTOModels;
using System.Threading.Tasks;

namespace Implemantation.IServices
{
    public interface IUserService
    {
        public Task<UserModel> GetUserByApiKeyAsync(string apiKey);
        public bool CheckBalanceUser(UserModel user, long serviceIndex);
        public Task TakePaymentAsync(string userId, double price);
    }
}