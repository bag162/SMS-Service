using Microsoft.EntityFrameworkCore;
using SMS_Service_Angular.DataBase.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS_Service_Angular.DataBase.Data.Contexts
{
    public class RoleContext : DbContext
    {
        public DbSet<RoleEntity> Roles { get; set; }

        public RoleContext(DbContextOptions<RoleContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
