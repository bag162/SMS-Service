using Backend.Models.DB;
using Backend.Implemantation.IServices;
using System.Linq;
using System.Threading.Tasks;
using Backend.TarantoolDB.Repositories;
using static Backend.TarantoolDB.Repositories.UserRepository;

namespace Backend.Implemantation.Services
{
    public class UserService : IUserService
    {
        public UserService(IServicePricesService pricesService, 
            UserRepository userRepository)
        {
            this.pricesService = pricesService;
            this.userRepository = userRepository;
        }

        private readonly UserRepository userRepository;
        private readonly IServicePricesService pricesService;

        public bool CheckBalanceUser(UserModel user, long serviceIndex)
        {
            double price = pricesService.GetServicePriceByServiceId(serviceIndex);
            if (user.Balance < price)
            {
                return false;
            }
            return true;
        }

        public UserModel GetUserByApiKey(string apiKey, int bucket = 2000)
        {
            return userRepository.Find(apiKey, (int)UserTFields.apikey, bucket).Result.FirstOrDefault();
        }

        public async Task TakePaymentAsync(string userId, double price, int bucket = 2000)
        {
            UserModel user = userRepository.Find(userId, 1, bucket).Result.FirstOrDefault();
            user.Balance += price;
            userRepository.Update(user);
            return;
        }
    }
}