using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS_Service_Angular.DataBase.Data.IServices
{
    public interface IRoleService
    {
        public bool isAdmin(long idUser);
    }
}
