using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.Implementation.Enums
{
    public enum HistoryType
    {
        // Routes
        GetNumber = 1,
        GetStatus = 2,
        SetStatus_6 = 3,
        SetStatus_8 = 4,

        // Fail requests (invalide data etc.)
        GetNumber_Fail = 6,
        GetStatus_Fail = 7,
        SetStatus_Fail = 8,

        // SMS Worker
        SetSMS = 5,
        InvalidExpressions = 11,
        NotComSMS = 12,
        AccountNoNumber = 14,
        InsertFirstMessage = 17,

        // Handler Conveyor
        NoProxy = 9,
        NoCookie = 10,
        CookieInactive = 13,
        CookieIncorrect = 18,
        ProxyNotHaveAccessToTN = 16,

        // Queue
        AddQueues = 15
    }
}
