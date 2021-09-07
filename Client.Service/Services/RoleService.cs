using Client.DataBase.Data.Contexts;
using Client.DataBase.Data.IServices;
using System;
using System.Linq;

namespace Client.DataBase.Data.Services
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
