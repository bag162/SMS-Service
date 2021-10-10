using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Client.Areas.Authorization.Models;
using Client.Database.Data.Repository;
using Client.gRPC.Services;
using System.Text.Json;
using Client.Infrastructure;
using System.Threading.Tasks;

namespace Client.Areas.Authorization.Controllers
{
    [Area("auth")]
    public class AuthController : Controller
    {
        private readonly gRPCUserService gRPCUserService;
        public readonly IUserService userService;

        public AuthController(IUserService userService,
            gRPCUserService gRPCUserService) {
            this.userService = userService;
            this.gRPCUserService = gRPCUserService;
        }

        [HttpPost]
        [Route("auth/login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public JsonResult Login([FromBody] LoginUserModel user)
        {
            return userService.LoginUser(user, HttpContext);
        }

        [HttpPost]
        [Route("auth/register")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<JsonResult> Register([FromBody] RegUserDTO user)
        {
            var result = userService.CreateNewUser(user, HttpContext);
            if (result.success == true)
            {
                await gRPCUserService.CreateUser(new Backend.Models.DB.UserModel() { Balance = 0, Login = user.login, ApiKey = "no", Id = "no" });
            }
            // TODO: Register user on tarantool
            return new JsonResult(result);
        }
    }
}