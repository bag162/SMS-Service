using AutoMapper;
using gRPCAccount;

namespace GrpcService
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Backend.Models.DB.AccountModel, AccountModel>();
            CreateMap<AccountModel, Backend.Models.DB.AccountModel >();
        }
    }
}