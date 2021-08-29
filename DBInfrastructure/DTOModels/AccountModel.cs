﻿using ProGaudi.MsgPack.Light;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBInfrastructure.DTOModels
{
    public class AccountModel
    {
        [MsgPackArrayElement(0)]
        public string Id { get; set; }
        [MsgPackArrayElement(1)]
        public string Login { get; set; }
        [MsgPackArrayElement(2)]
        public string Password { get; set; }
        [MsgPackArrayElement(3)]
        public string Number { get; set; }
        [MsgPackArrayElement(4)]
        public string Cookie { get; set; }
        [MsgPackArrayElement(5)]
        public int Status { get; set; }
    }
}