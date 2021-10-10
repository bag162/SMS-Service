using Backend.Models.DB;
using Backend.Implemantation.IServices;
using System.Linq;
using System.Threading.Tasks;
using Backend.TarantoolDB.Repositories;
using static Backend.TarantoolDB.Repositories.UserRepository;
using System;
using System.Text;

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
        static Random random = new();
        const string accessSymbols = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public async Task<UserModel> CreateUser(UserModel user, int bucket = 2000)
        {
            var createdUser = user;
            createdUser.ApiKey = GenerateRandomString(30);
            createdUser.Id = Guid.NewGuid().ToString();
            createdUser.Bucket = 2000;
            return await userRepository.Create(createdUser);
        }

        public UserModel GetUserByApiKey(string apiKey, int bucket = 2000)
        {
            var user = userRepository.Find(apiKey, (int)UserTFields.apikey, bucket).Result;
            if (user.Count() == 0)
            {
                return null;
            }
            return user.First();
        }
        public UserModel GetUserByLogin(string login, int bucket = 2000)
        {
            var user = userRepository.Find(login, (int)UserTFields.login, bucket).Result;
            if (user.Count() == 0)
            {
                return null;
            }
            return user.First();
        }

        public async Task<UserModel> UpdateApiKey(string login, int bucket = 2000)
        {
            var user = userRepository.Find(login, (int)UserTFields.login, bucket).Result.First();
            user.ApiKey = GenerateRandomString(30);
            return await userRepository.Update(user);
        }

        public bool CheckBalanceUser(UserModel user, long serviceIndex)
        {
            double price = pricesService.GetServicePriceByServiceId(serviceIndex);
            if (user.Balance < price)
            {
                return false;
            }
            return true;
        }
        public async Task TakePaymentAsync(string userId, double price, int bucket = 2000)
        {
            UserModel user = userRepository.Find(userId, 1, bucket).Result.FirstOrDefault();
            user.Balance += price;
            await userRepository.Update(user);
            return;
        }

        internal static string GenerateRandomString(int length)
        {
            StringBuilder sb = new StringBuilder(length - 1);
            for (int i = 0; i < length; i++)
            {
                sb.Append(accessSymbols[random.Next(0, accessSymbols.Length - 1)]);
            }
            return sb.ToString();
        }
    }
}