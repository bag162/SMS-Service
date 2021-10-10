using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Areas.Authorization.Models
{
    public class RegUserDTO
    {
        public string login { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string telegram { get; set; }
        public string name { get; set; }
    }
}
