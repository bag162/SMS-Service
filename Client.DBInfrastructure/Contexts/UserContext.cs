using Microsoft.EntityFrameworkCore;
using Client.DataBase.Contexts;

namespace Client.DataBase.Data.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
