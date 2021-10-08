using AutoMapper;
using Backend.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.gRPC.Services
{
    public class UserService
    {
        private readonly gRPCClient client;
        private readonly IMapper mapper;
        private readonly gRPCUser.User.UserClient userClient;

        public UserService(gRPCClient client,
            IMapper mapper,
            gRPCUser.User.UserClient userClient) {
            this.client = client;
            this.mapper = mapper;
            this.userClient = userClient;
        }

        public async Task<UserModel> CreateUser(UserModel user)
        {
            var response = await userClient.CreateUserAsync(mapper.Map<gRPCUser.UserModel>(user));
            return mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> UpdateApikey(string idUser)
        {
            var response = await userClient.UpdateApiKeyAsync(new gRPCUser.UpdateApiKeyModel() { UserId = idUser });
            return mapper.Map<UserModel>(response);
        }
    }
}
