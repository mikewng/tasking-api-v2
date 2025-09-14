using Microsoft.EntityFrameworkCore;
using tasking_api.Infrastructure.Context;
using tasking_api.Main.Data.Contracts;
using tasking_api.Main.Models;

namespace tasking_api.Main.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user, CancellationToken ct = default)
        {
            _context.user_account.Add(user);
            await _context.SaveChangesAsync(ct);
            return user;
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
        {
            return await _context.user_account.AnyAsync(u => u.Email == email, ct);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _context.user_account.FirstOrDefaultAsync(u => u.Email == email, ct);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.user_account.FirstOrDefaultAsync(u => u.Id == id, ct);
        }

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default)
        {
            return await _context.user_account.FirstOrDefaultAsync(u => u.Username == username, ct);
        }

        public async Task<User> UpdateAsync(User user, CancellationToken ct = default)
        {
            _context.user_account.Update(user);
            await _context.SaveChangesAsync(ct);
            return user;
        }

        public async Task<bool> UsernameExistsAsync(string username, CancellationToken ct = default)
        {
            return await _context.user_account.AnyAsync(u => u.Username == username, ct);
        }
    }
}
