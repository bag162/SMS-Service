using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Database.Data.Repository
{
    public interface IAuthenticateService
    {
        public Task Authenticate(string userLogin, HttpContext HttpContext);
    }
}
