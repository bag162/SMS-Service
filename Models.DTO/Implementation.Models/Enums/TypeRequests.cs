using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.ImplementationModels.Enums
{
    public enum TypeRequests
    {
        GetNumber = 1,
        GetStatus = 2,
        SetStatus_6 = 3,
        Setstatus_8 = 4,
        SetSMS = 5,
        GetNumber_Fail = 6,
        GetStatus_Fail = 7,
        SetStatus_Fail = 8,
        NoProxy = 9,
        NoCookie = 10,
        InvalidExpressions = 11,
        NotComSMS = 12,
        CookieInactive = 13,
        AccountNoNumber = 14
    }
}
