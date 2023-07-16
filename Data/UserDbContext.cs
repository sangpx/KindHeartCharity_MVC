using KindHeartCharity.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KindHeartCharity.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<DbContext> options) : base(options) { }


        public DbSet<User> users { get; set; }
    }
}
