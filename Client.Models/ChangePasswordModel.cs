using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Areas.Settings.Models
{
    public class ChangePasswordModel
    {
        public string oldpassword { get; set; }
        public string newpassword { get; set; }
    }
}
