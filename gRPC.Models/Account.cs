using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace gRPC.Models
{
    public class Account
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Number { get; set; }
        public string Cookie { get; set; }
        public int Status { get; set; }
    }
}