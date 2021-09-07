using Microsoft.EntityFrameworkCore;
using Client.DataBase.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DataBase.Data.Contexts
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
