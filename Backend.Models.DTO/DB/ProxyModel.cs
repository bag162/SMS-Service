using ProGaudi.MsgPack.Light;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.DB
{
    public class ProxyModel
    {
        [MsgPackArrayElement(0)]
        public string Id { get; set; }
        [MsgPackArrayElement(1)]
        public string Ip { get; set; }
        [MsgPackArrayElement(2)]
        public string Port { get; set; }
        [MsgPackArrayElement(3)]
        public string Login { get; set; }
        [MsgPackArrayElement(4)]
        public string Password { get; set; }
        [MsgPackArrayElement(5)]
        public int Status { get; set; }
        [MsgPackArrayElement(6)]
        public int LasteTimeActive { get; set; }
        [MsgPackArrayElement(7)]
        public string ExternalIp { get; set; }
        [MsgPackArrayElement(8)]
        public long Bucket { get; set; }
    }
}