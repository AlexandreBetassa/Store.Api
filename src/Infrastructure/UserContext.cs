using Microsoft.EntityFrameworkCore;
using Store.User.Domain.Models.v1.Users;

namespace Store.User.Infrastructure.Data
{
    public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
    {
        public DbSet<Domain.Entities.v1.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.v1.User>().OwnsOne(u => u.Name);
        }
    }
}
