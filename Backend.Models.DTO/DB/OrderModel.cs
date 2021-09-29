using ProGaudi.MsgPack.Light;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.DB
{
    public class OrderModel
    {
        [MsgPackArrayElement(0)]
        public string Id { get; set; }
        [MsgPackArrayElement(1)]
        public int Status { get; set; }
        [MsgPackArrayElement(2)]
        public string Number { get; set; }
        [MsgPackArrayElement(3)]
        public string UserId { get; set; }
        [MsgPackArrayElement(4)]
        public int OrderId { get; set; }
        [MsgPackArrayElement(5)]
        public int Service { get; set; }
        [MsgPackArrayElement(6)]
        public string SMS { get; set; }
        [MsgPackArrayElement(7)]
        public string SMSCode { get; set; }
        [MsgPackArrayElement(8)]
        public int StartDateTime { get; set; }
        [MsgPackArrayElement(9)]
        public int LastCheckTime { get; set; }
        [MsgPackArrayElement(10)]
        public string JsonData { get; set; }
        [MsgPackArrayElement(11)]
        public long Bucket { get; set; }
    }
}