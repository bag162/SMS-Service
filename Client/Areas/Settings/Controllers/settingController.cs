using Microsoft.AspNetCore.Mvc;
using Client.Areas.Settings.Models;
using Client.Database.Data.Repository;
using Client.DataBase.Contexts;
using Client.DataBase.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Areas.Settings.Controllers
{
    [Area("settings")]
    public class SettingController : Controller
    {
        private readonly IUserService userService;
        public SettingController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public JsonResult ChangePassword([FromBody] ChangePasswordModel model)
        {
            return userService.ChangePassword(model.oldpassword, model.newpassword, User.Identity.Name);
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public JsonResult UpdateUserInfo([FromBody] UpdateUserInfoModel user)
        {
            UserEntity userEntiy = new UserEntity() { EmailAddress = user.EmailAddress, Telegram = user.Telegram, Name = user.Name, Login = User.Identity.Name };
            return userService.UpdateUserInfo(userEntiy);
        }
    }
}
