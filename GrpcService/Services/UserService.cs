using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Implemantation.IServices;
using Grpc.Core;
using gRPCUser;
using Microsoft.Extensions.Logging;

namespace GrpcService.Services
{
    public class UserService : User.UserBase
    {
        private readonly ILogger<UserService> logger;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UserService(ILogger<UserService> logger,
            IMapper mapper,
            IUserService userService) {
            this.logger = logger;
            this.mapper = mapper;
            this.userService = userService;
        }

        public override async Task<UserModel> CreateUser(UserModel request, ServerCallContext context)
        {
            var createdUser = mapper.Map<Backend.Models.DB.UserModel>(request);
            var newUser = await userService.CreateUser(createdUser);
            return mapper.Map<UserModel>(newUser);
        }

        public override async Task<UserModel> UpdateApiKey(UpdateApiKeyModel request, ServerCallContext context)
        {
            var updatedUser = await userService.UpdateApiKey(request.Login);
            return mapper.Map<UserModel>(updatedUser);
        }

    }
}
