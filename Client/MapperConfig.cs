using AutoMapper;
using Client.DataBase.Contexts;
using Client.Models.DTO;
using gRPCAccount;
using gRPCUser;

namespace Client
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Backend.Models.DB.AccountModel, AccountModel>();
            CreateMap<AccountModel, Backend.Models.DB.AccountModel>();

            CreateMap<Backend.Models.DB.UserModel, UserModel>();
            CreateMap<UserModel, Backend.Models.DB.UserModel>();

            CreateMap<UserInfoDTO, UserEntity>();
            CreateMap<UserEntity, UserInfoDTO>();
        }
    }
}