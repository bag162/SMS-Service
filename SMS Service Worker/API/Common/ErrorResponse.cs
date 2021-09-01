using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS_Service_Worker.API.PrivateWEB.Common
{
    public class ErrorResponseController : Controller
    {
        public ContentResult BadKey()
        {
            return new ContentResult { Content = "BAD_KEY", StatusCode = 403 };
        }

        public ContentResult BadAction()
        {
            return new ContentResult { Content = "BAD_ACTION", StatusCode = 403 };
        }

        public ContentResult BasStatus()
        {
            return new ContentResult { Content = "BAD_STATUS", StatusCode = 406 };
        }
    }
}
