using Backend.Models.DB;
using System.Threading.Tasks;

namespace Backend.Implemantation.IServices
{
    public interface IUserService
    {
        public UserModel GetUserByApiKey(string apiKey, int bucket = 2000);

        public bool CheckBalanceUser(UserModel user, long serviceIndex);

        public Task TakePaymentAsync(string userId, double price, int bucket = 2000);
    }
}