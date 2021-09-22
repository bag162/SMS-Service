using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models.Implementation.Models.Enums
{
    public enum QueueType
    {
        Order = 1,
        CheckProxy = 2,
        CheckOrderOnInsert = 3,
        CheckOrderOnDelete = 4,
        CheckAccountsOnValid = 5
    }
}
