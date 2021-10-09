using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRPC.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public double Balance { get; set; }
        public string ApiKey { get; set; }
    }
}