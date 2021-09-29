using Client.gRPC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Areas
{
    public class TestController : Controller
    {
        private readonly AccountService accountService;
        public TestController(AccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        [Route("test")]
        public ContentResult test()
        {
            return new ContentResult() { Content = JsonSerializer.Serialize(accountService.GetAccounts(1)), StatusCode = 200 };
        }
    }
}