using Client.DataBase.Data.Contexts;
using Client.DataBase.Data.IServices;
using System;
using System.Linq;

namespace Client.DataBase.Data.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserContext userContext;

        public RoleService (UserContext userContext)
        {
            this.userContext = userContext;
        }

        public bool isAdmin(long idUser)
        {
            return userContext.Roles
                .AsQueryable()
                .Contains(new Entities.RoleEntity { id = idUser, role = "admin" });
        }
    }
}
