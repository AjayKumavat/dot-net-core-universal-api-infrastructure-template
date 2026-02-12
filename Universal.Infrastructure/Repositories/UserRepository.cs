using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Universal.Core.Entities;
using Universal.Core.Interfaces;
using Universal.Infrastructure.Persistence;

namespace Universal.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UniversalDbContext _context;

        public UserRepository(UniversalDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email.Equals(email.ToLower()));
        }
    }
}
