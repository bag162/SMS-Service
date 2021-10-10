using AutoMapper;
using Client.Areas.Authorization.Models;
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

            CreateMap<UserInfoDTO, Backend.Models.DB.UserModel>();
            CreateMap<Backend.Models.DB.UserModel, UserInfoDTO>();

            CreateMap<RegUserDTO, UserEntity>()
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(c => c.email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(c => c.name));
        }
    }
}