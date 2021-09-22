using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.ImplementationModels.Enums
{
    public enum OrderStatus
    {
        STATUS_FREE = 0,
        STATUS_WAIT_CODE = 1,
        STATUS_OK = 6,
        STATUS_CANCEL = 8,
        
        RESERVE = 9
    }
}