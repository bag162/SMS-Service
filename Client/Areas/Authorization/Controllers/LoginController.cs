using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Client.Areas.Authorization.Models;
using Client.Database.Data.Repository;

namespace Client.Areas.Authorization.Controllers
{
    [Area("auth")]
    public class LoginController : Controller
    {
        public readonly IUserService userService;
        public LoginController(IUserService userService)
        {
            this.userService = userService;
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
        public JsonResult Register([FromBody] RegistrationUserModel user)
        {
            return userService.CreateNewUser(user, HttpContext);
        }
    }
}