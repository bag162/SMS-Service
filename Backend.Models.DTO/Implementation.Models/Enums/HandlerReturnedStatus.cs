using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.Implementation.Enums
{
    public enum HandlerConveerStatus
    {
        Success = 1,
        NoProxy = 2,
        IncorrectCookie = 3,
        NoCookie = 4
    }
}
