using Microsoft.EntityFrameworkCore;

namespace Universal.Infrastructure.DataContext
{
    public class UniversalDbContext : DbContext
    {
        public UniversalDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}