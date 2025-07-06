using Microsoft.EntityFrameworkCore;

namespace Fatec.Store.User.Infrastructure.Data.Context
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
