using Models.DTO.DTOModels;
using System.Threading.Tasks;

namespace Implemantation.IServices
{
    public interface IUserService
    {
        public UserModel GetUserByApiKey(string apiKey, int bucket = 2000);

        public bool CheckBalanceUser(UserModel user, long serviceIndex);

        public Task TakePaymentAsync(string userId, double price, int bucket = 2000);
    }
}