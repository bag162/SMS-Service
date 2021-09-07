using Microsoft.AspNetCore.Mvc;
using Client.Database.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Areas.Home.Controllers
{
    [Area("controlPanel")]
    public class HomeController : Controller
    {
        private readonly IUserService userService;

        public HomeController(IUserService userRepository)
        {
            this.userService = userRepository;
        }

        [HttpGet]
        public JsonResult GetUserInfo()
        {
            return userService.GetUser(User.Identity.Name);
        }
    }
}