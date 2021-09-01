using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS_Service_Angular.Areas.Settings.Models
{
    public class UpdateUserInfoModel
    {
        public string EmailAddress { get; set; }
        public string Telegram { get; set; }
        public string Username { get; set; }
    }
}
