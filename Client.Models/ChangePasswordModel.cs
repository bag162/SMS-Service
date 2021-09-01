using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS_Service_Angular.Areas.Settings.Models
{
    public class ChangePasswordModel
    {
        public string oldpassword { get; set; }
        public string newpassword { get; set; }
    }
}
