using Microsoft.EntityFrameworkCore;
using Universal.Core.Entities;

namespace Universal.Infrastructure.Persistence
{
    public class UniversalDbContext : DbContext
    {
        public UniversalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversalDbContext).Assembly);
        }
    }
}