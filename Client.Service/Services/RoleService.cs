using SMS_Service_Angular.DataBase.Data.Contexts;
using SMS_Service_Angular.DataBase.Data.IServices;
using System;
using System.Linq;

namespace SMS_Service_Angular.DataBase.Data.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleContext roleContext;

        public RoleService (RoleContext roleContext)
        {
            this.roleContext = roleContext;
        }

        public bool isAdmin(long idUser)
        {
            return roleContext.Roles
                .AsQueryable()
                .Contains(new Entities.RoleEntity { id = idUser, role = "admin" });
        }
    }
}
