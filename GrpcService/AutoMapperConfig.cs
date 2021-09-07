using AutoMapper;
using GrpcAccount;

namespace GrpcService
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Models.DTO.DTOModels.AccountModel, AccountModel>();
            CreateMap<AccountModel, Models.DTO.DTOModels.AccountModel>();
        }
    }
}
