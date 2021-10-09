using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Client.Areas.Authorization.Models;
using Client.DataBase.Contexts;
using Client.Infrastructure;
using Client.Models.DTO;

namespace Client.Database.Data.Repository
{
    public interface IUserService
    {
        public JsonResponseDTO CreateNewUser(RegistrationUserModel user, HttpContext httpContext);
        public JsonResult LoginUser(LoginUserModel user, HttpContext httpContext);
        public UserInfoDTO GetUser(string userLogin);
        public JsonResult ChangePassword(string oldpassword, string newpassword, string login);
        public JsonResult UpdateUserInfo(UserEntity user);
    }
}