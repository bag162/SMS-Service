using Microsoft.EntityFrameworkCore;
using Client.DataBase.Contexts;
using Client.DataBase.Data.Entities;

namespace Client.DataBase.Data.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}