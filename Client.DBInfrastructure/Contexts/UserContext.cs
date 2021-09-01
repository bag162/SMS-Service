using Microsoft.EntityFrameworkCore;
using SMS_Service_Angular.DataBase.Contexts;

namespace SMS_Service_Angular.DataBase.Data.Contexts
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
