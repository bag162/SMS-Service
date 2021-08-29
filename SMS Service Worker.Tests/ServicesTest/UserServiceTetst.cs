using NUnit.Framework;
using SMS_Service_Worker.Data.AbstractModels;
using SMS_Service_Worker.Data.Business.IServices;
using SMS_Service_Worker.Data.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Service_Worker.Tests.ServicesTest
{
    class UserServiceTetst
    {
        private readonly IUserService service;
        private readonly string apiKey = "123456789"; // взято из конфига инициализации БД тарантула

        public UserServiceTetst(IUserService service)
        {
            this.service = service;
        }

        [Test]
        public void GetUserByApikey()
        {
            // Arrange

            // Act
            UserModel user = service.GetUserByApiKeyAsync(apiKey).Result;

            // Assert 
            Assert.NotNull(user);
        }
    }
}
