using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Client.Areas.Authorization.Models;
using Client.Database.Data.Repository;
using Client.Database.Data.Services;
using Client.DataBase.Contexts;
using Client.DataBase.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Areas.Authorization.Controllers
{
    [Area("authorization")]
    public class RegistrationController : Controller
    {
        public readonly IUserService userService;
        public RegistrationController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public JsonResult NewUserRegistration([FromBody] RegistrationUserModel user)
        {
            return userService.CreateNewUser(user, HttpContext);
        }
    }
}
