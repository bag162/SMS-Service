using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Client.Areas.Authorization.Models;
using Client.Database.Data.Repository;

namespace Client.Areas.Authorization.Controllers
{
    [Area("authorization")]
    public class LoginController : Controller
    {
        public readonly IUserService userService;
        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public JsonResult LoginUser([FromBody] LoginUserModel user)
        {
            return userService.LoginUser(user, HttpContext);
        }
    }
}