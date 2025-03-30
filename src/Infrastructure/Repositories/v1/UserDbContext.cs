using Microsoft.EntityFrameworkCore;

namespace Store.User.Infrastructure.Data.Repositories.v1
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<Domain.Entities.v1.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.v1.User>().OwnsOne(u => u.Name);
        }
    }
}
