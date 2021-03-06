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
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UserService(
            IMapper mapper,
            IUserService userService) {
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
            if (updatedUser == null)
            {
                return null;
            }
            return mapper.Map<UserModel>(updatedUser);
        }

        public override async Task<UserModel> GetUserByLogin(ReqUserByLoginModel request, ServerCallContext context)
        {
            var user = userService.GetUserByLogin(request.Login);
            if (user == null)
            {
                return null;
            }
            return mapper.Map<UserModel>(user);
        }
    }
}