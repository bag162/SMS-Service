using ProGaudi.MsgPack.Light;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DBInfrastructure.Models
{
    public class JsonRequestOneField
    {
        [MsgPackArrayElement(0)]
        public string JsonRequest { get; set; }
    }
}
