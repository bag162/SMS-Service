using Models.DTO.DTOModels;
using Implemantation.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SMS_Service_Worker.API.PrivateWEB.GetBalance
{
    public class GetBalanceController : Controller
    {
        private readonly IUserService userService;

        public GetBalanceController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet]
        public async Task<ContentResult> GetBalance(string api_key)
        {
            UserModel user = await userService.GetUserByApiKeyAsync(api_key);
            return new ContentResult { Content = "ACCESS_BALANCE:" + user.Balance };
        }
    }
}
