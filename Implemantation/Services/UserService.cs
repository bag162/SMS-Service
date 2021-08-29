using DBInfrastructure;
using DBInfrastructure.DTOModels;
using Implemantation.IServices;
using System.Linq;
using System.Threading.Tasks;

namespace Implemantation.Services
{
    public class UserService : IUserService
    {
        public UserService(IServicePricesService pricesService, 
            TRepository<UserModel> userRepository)
        {
            this.pricesService = pricesService;
            this.userRepository = userRepository;
        }

        private readonly TRepository<UserModel> userRepository;
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
        public async Task<UserModel> GetUserByApiKeyAsync(string apiKey)
        {
            var iindex = await userRepository.Space.GetIndex("secondary_apikey");
            return userRepository.Find(iindex, apiKey).FirstOrDefault();
        }

        public async Task TakePaymentAsync(string userId, double price)
        {
            UserModel user = userRepository.FindById(userId);
            user.Balance += price;
            await userRepository.Update(user);
            return;
        }
    }
}
