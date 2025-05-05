using Microsoft.EntityFrameworkCore;
using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CanteenDbContext _context;
        public UserRepository(CanteenDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> AddAsync(User User)
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return User;
        }
        public async Task<User> UpdateAsync(User User)
        {
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
            return User;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Users.FindAsync(id);
            if (item == null)
                return false;
            _context.Users.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        Task IRepository<User>.UpdateAsync(User entity)
        {
            return UpdateAsync(entity);
        }

        Task IRepository<User>.DeleteAsync(int id)
        {
            return DeleteAsync(id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}


