using Microsoft.AspNetCore.Mvc;
using Client.Database.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Client.Configuration;
using System.Text.Json;
using Client.Infrastructure;
using Client.Models.DTO;
using Client.gRPC.Services;

namespace Client.Areas.Home.Controllers
{
    [Area("cp")]
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IOptions<ConfigurationClass> config;
        private readonly gRPCUserService gRPCUserService;
        public HomeController(IUserService userRepository,
            IOptions<ConfigurationClass> config,
            gRPCUserService gRPCUserService) {
            userService = userRepository;
            this.config = config;
            this.gRPCUserService = gRPCUserService;
        }

        [HttpGet]
        [Route("cp/user")]
        public async Task<JsonResult> UserInfo()
        {
            return userService.GetUser(User.Identity.Name);
        }

        [HttpGet]
        [Route("cp/dashboard")]
        public async Task<JsonResult> SharedDashboardData()
        {
            var response = new DashboardInfoDTO() { success = true, BalancerAddress = config.Value.BalancerAddress };
            return new JsonResult(response);
        }

        [HttpPut]
        [Route("cp/apikey")]
        public async Task<JsonResult> UpdateApiKey()
        {
            var response = gRPCUserService.UpdateApikey(User.Identity.Name);
            return new JsonResult(new {apiKey = response.Result.ApiKey});
        }
    }
}