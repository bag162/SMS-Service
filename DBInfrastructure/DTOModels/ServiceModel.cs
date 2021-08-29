using ProGaudi.MsgPack.Light;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBInfrastructure.DTOModels
{
    public class ServiceModel
    {
        [MsgPackArrayElement(0)]
        public long Id { get; set; }
        [MsgPackArrayElement(1)]
        public double Price { get; set; }
        [MsgPackArrayElement(2)]
        public string RegularExpressions { get; set; }
        [MsgPackArrayElement(3)]
        public string ServicePrefix { get; set; }
    }
}