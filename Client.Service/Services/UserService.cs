using AutoMapper;
using Microsoft.AspNetCore.Http;
using Client.Areas.Authorization.Models;
using Client.Database.Data.Repository;
using Client.DataBase.Contexts;
using Client.DataBase.Data.Contexts;
using Client.Infrastructure;
using System.Linq;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using Client.Models.DTO;

namespace Client.DataBase.Data.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext userContext;
        private readonly IMapper mapper;
        private readonly IAuthenticateService authenticateService;
        public UserService(
            UserContext context, 
            IAuthenticateService authenticateRepository,
            IMapper mapper)
        {
            this.userContext = context;
            this.authenticateService = authenticateRepository;
            this.mapper = mapper;
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

        public JsonResponseDTO CreateNewUser(RegUserDTO user, HttpContext httpContext)
        {
            var checLogin = userContext.Users.AsQueryable().Where(u => u.Login == user.login).FirstOrDefault();
            if (checLogin != null) {
                return new JsonResponseDTO
                {
                    success = false,
                    message = "This login is already registered in the system."
                };
            }
            var checkEmail = userContext.Users.AsQueryable().Where(u => u.EmailAddress == user.email).FirstOrDefault();
            if (checkEmail != null){
                return new JsonResponseDTO
                {
                    success = false,
                    message = "This email is already registered in the system.",
                };
            }
            UserEntity addedUser = mapper.Map<UserEntity>(user);

            addedUser.IdRole = 1;
            userContext.Users.Add(addedUser);
            userContext.SaveChanges();
            if (httpContext != null)
            {
                authenticateService.Authenticate(user.login, httpContext);
            }
            return new JsonResponseDTO
            {
                success = true,
                message = "You have successfully registered. You will now be redirected to the control panel."
            };
        }

        public UserInfoDTO GetUser(string userLogin)
        {
            UserEntity user = userContext.Users
                .AsQueryable()
                .Where(u => u.Login == userLogin)
                .FirstOrDefault();

            return mapper.Map<UserInfoDTO>(user);
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
            newUser.Name = user.Name;
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