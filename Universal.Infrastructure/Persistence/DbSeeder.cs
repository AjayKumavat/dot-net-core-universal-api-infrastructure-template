using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Universal.Core.Entities;
using Universal.Core.Interfaces;

namespace Universal.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<UniversalDbContext>();
            var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            await context.Database.MigrateAsync();

            if (!context.Roles.Any())
            {
                var adminRole = new Role("Admin");
                var userRole = new Role("User");

                await context.Roles.AddRangeAsync(adminRole, userRole);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var adminRole = await context.Roles.FirstAsync(x => x.Name == "Admin");

                var hashedPassword = hasher.Hash("Admin@123");

                var adminUser = new User(
                    "admin@test.com",
                    hashedPassword,
                    adminRole.Id);

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}