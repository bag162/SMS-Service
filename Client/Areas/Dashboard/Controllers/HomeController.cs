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
using AutoMapper;

namespace Client.Areas.Home.Controllers
{
    [Area("cp")]
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IOptions<ConfigurationClass> config;
        private readonly gRPCUserService gRPCUserService;
        private readonly IMapper mapper;

        public HomeController(IUserService userRepository,
            IOptions<ConfigurationClass> config,
            gRPCUserService gRPCUserService,
            IMapper mapper) 
        {
            userService = userRepository;
            this.config = config;
            this.gRPCUserService = gRPCUserService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("cp/user")]
        public async Task<JsonResult> UserInfo()
        {
            var userClient = userService.GetUser(User.Identity.Name);
            if (userClient == null)
            {
                return new JsonResult(new JsonResponseDTO() { success = false });
            }

            var userBackend = gRPCUserService.GetUserByLogin(User.Identity.Name);

            await mapper.Map(userClient, userBackend);
            return new JsonResult(userBackend);
        }

        [HttpGet]
        [Route("cp/dashboard")]
        public async Task<JsonResult> SharedDashboardData()
        {
            var response = new DashboardInfoDTO() { success = true, BalancerAddress = config.Value.BalancerAddress };
            return new JsonResult(response);
        }

        [HttpGet]
        [Route("cp/apikey")]
        public async Task<JsonResult> UpdateApiKey()
        {
            var response = gRPCUserService.UpdateApikey(User.Identity.Name);
            return new JsonResult(new {apiKey = response.Result.ApiKey});
        }
    }
}