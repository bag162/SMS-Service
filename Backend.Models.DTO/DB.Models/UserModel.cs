using ProGaudi.MsgPack.Light;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.DTO.DTOModels
{
    public class UserModel
    {
        [MsgPackArrayElement(0)]
        public string Id { get; set; }
        [MsgPackArrayElement(1)]
        public string Login { get; set; }
        [MsgPackArrayElement(2)]
        public double Balance { get; set; }
        [MsgPackArrayElement(3)]
        public string ApiKey { get; set; }
        [MsgPackArrayElement(4)]
        public string Bucket { get; set; }
    }
}