using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS_Service_Angular.Database.Data.Repository
{
    public interface IAuthenticateService
    {
        public Task Authenticate(string userLogin, HttpContext HttpContext);
    }
}
