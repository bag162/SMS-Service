using AutoMapper;
using Microsoft.AspNetCore.Http;
using Client.Areas.Authorization.Models;
using Client.Database.Data.Repository;
using Client.DataBase.Contexts;
using Client.DataBase.Data.Contexts;
using Client.Infrastructure;
using System.Linq;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace Client.DataBase.Data.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext userContext;
        private readonly IAuthenticateService authenticateService;
        public UserService(
            UserContext context, 
            IAuthenticateService authenticateRepository)
        {
            this.userContext = context;
            this.authenticateService = authenticateRepository;
        }

        public JsonResult ChangePassword(string oldpassword, string newpassword, string login)
        {
            UserEntity user = userContext.Users.FirstOrDefault(x => x.Login == login);
            if (user.Password != oldpassword)
            {
                return new JsonResult(new JsonResponseDTO
                {
                    success = false,
                    message = "Old password is incorrect"
                });
            }
            user.Password = newpassword;
            userContext.Users.Update(user);
            userContext.SaveChanges();
            return new JsonResult(new JsonResponseDTO
            {
                success = true,
                message = "Password updated successfully"
            });
        }

        public JsonResult CreateNewUser(RegistrationUserModel user, HttpContext httpContext)
        {
            var checLogin = userContext.Users.AsQueryable().Where(u => u.Login == user.login).FirstOrDefault();
            if (checLogin != null) {
                return new JsonResult(new JsonResponseDTO
                {
                    success = false,
                    message = "This login is already registered in the system."
                });
            }
            var checkEmail = userContext.Users.AsQueryable().Where(u => u.EmailAddress == user.email).FirstOrDefault();
            if (checkEmail != null){
                return new JsonResult(new JsonResponseDTO
                {
                    success = false,
                    message = "This email is already registered in the system.",
                });
            }

            // mapper
            var registraionUserMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<RegistrationUserModel, UserEntity>()
            .ForMember("EmailAddress", opt => opt.MapFrom(c => c.email))
            .ForMember("Username", opt => opt.MapFrom(c => c.name))
            );
            var registrationUserMapper = new Mapper(registraionUserMapConfig);
            UserEntity addedUser = registrationUserMapper.Map<UserEntity>(user);
            // end mapper

            addedUser.IdRole = 1;
            userContext.Users.Add(addedUser);
            userContext.SaveChanges();
            if (httpContext != null)
            {
                authenticateService.Authenticate(user.login, httpContext);
            }
            return new JsonResult(new JsonResponseDTO
            {
                success = true,
                message = "You have successfully registered. You will now be redirected to the control panel."
            });
        }

        public JsonResult GetUser(string userLogin)
        {
            UserEntity user = userContext.Users
                .AsQueryable()
                .Where(u => u.Login == userLogin)
                .FirstOrDefault();
            if (user == null)
            {
                return new JsonResult(new JsonResponseDTO
                {
                    success = false
                });
            }
            return new JsonResult(new
            {
                success = true,
                data = user
            });
        }

        public JsonResult LoginUser(LoginUserModel user, HttpContext httpContext)
        {
            var checkUser = userContext.Users
                .AsQueryable()
                .Where(x => x.Login == user.Login && x.Password == user.Password)
                .FirstOrDefault();

            if (checkUser != null)
            {
                if (httpContext != null)
                {
                    authenticateService.Authenticate(user.Login, httpContext);
                }
                return new JsonResult(new JsonResponseDTO
                {
                    success = true,
                    message = "The authorization was successful. You will now be redirected to the control panel."
                });
            }
            else
            {
                return new JsonResult(new JsonResponseDTO
                {
                    success = false,
                    message = "Wrong login or password."
                });
            }
            
        }

        public JsonResult UpdateUserInfo(UserEntity user)
        {
            UserEntity newUser = userContext.Users.FirstOrDefault(x => x.Login == user.Login);
            newUser.EmailAddress = user.EmailAddress;
            newUser.Telegram = user.Telegram;
            newUser.Username = user.Username;
            userContext.Users.Update(newUser);
            userContext.SaveChanges();
            return new JsonResult(new JsonResponseDTO
            {
                success = true,
                message = "Account data updated successfully"
            });
        }
    }
}