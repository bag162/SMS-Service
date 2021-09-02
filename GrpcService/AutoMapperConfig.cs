using AutoMapper;

namespace GrpcService
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<DBInfrastructure.DTOModels.AccountModel, AccountModel>();
            CreateMap<AccountModel, DBInfrastructure.DTOModels.AccountModel>();


        }
    }
}
