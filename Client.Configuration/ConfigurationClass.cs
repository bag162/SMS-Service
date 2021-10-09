using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Configuration
{
    public class ConfigurationClass
    {
        public string gRPC { get; set; }
        public string BalancerAddress { get; set; }
        public string ConnectionString { get; set; }
    }
}