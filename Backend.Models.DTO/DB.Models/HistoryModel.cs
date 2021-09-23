using ProGaudi.MsgPack.Light;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.DTO.DTOModels
{
    public class HistoryModel
    {
        [MsgPackArrayElement(0)]
        public string Id { get; set; }
        [MsgPackArrayElement(1)]
        public int TypeRequest { get; set; }
        [MsgPackArrayElement(2)]
        public string UserId { get; set; }
        [MsgPackArrayElement(3)]
        public int RequestTime { get; set; }
        [MsgPackArrayElement(4)]
        public string Message { get; set; }
        [MsgPackArrayElement(5)]
        public long Bucket { get; set; }
    }
}