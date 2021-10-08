using AutoMapper;
using gRPCAccount;
using gRPCUser;

namespace GrpcService
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Backend.Models.DB.AccountModel, AccountModel>();
            CreateMap<AccountModel, Backend.Models.DB.AccountModel >();

            CreateMap<Backend.Models.DB.UserModel, UserModel>();
            CreateMap<UserModel, Backend.Models.DB.UserModel>();
        }
    }
}