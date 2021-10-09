using AutoMapper;
using Backend.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.gRPC.Services
{
    public class gRPCUserService
    {
        private readonly IMapper mapper;
        private readonly gRPCUser.User.UserClient userClient;

        public gRPCUserService(gRPCClient Client, IMapper mapper) 
        {
            this.mapper = mapper;
            userClient = new gRPCUser.User.UserClient(Client.channel);
        }

        public async Task<UserModel> CreateUser(UserModel user)
        {
            var response = await userClient.CreateUserAsync(mapper.Map<gRPCUser.UserModel>(user));
            return mapper.Map<UserModel>(response);
        }

        public async Task<UserModel> UpdateApikey(string idUser)
        {
            var response = await userClient.UpdateApiKeyAsync(new gRPCUser.UpdateApiKeyModel() { UserId = idUser });
            return mapper.Map<UserModel>(response);
        }
    }
}
