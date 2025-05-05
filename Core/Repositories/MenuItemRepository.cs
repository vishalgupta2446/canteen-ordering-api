using Microsoft.EntityFrameworkCore;
using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.Core.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly CanteenDbContext _context;
        public MenuItemRepository(CanteenDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems.ToListAsync();
        }
        public async Task<MenuItem> GetByIdAsync(int id)
        {
            return await _context.MenuItems.FindAsync(id);
        }
        public async Task<MenuItem> AddAsync(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }
        public async Task<MenuItem> UpdateAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item == null)
                return false;
            _context.MenuItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        Task IRepository<MenuItem>.UpdateAsync(MenuItem entity)
        {
            return UpdateAsync(entity);
        }

        Task IRepository<MenuItem>.DeleteAsync(int id)
        {
            return DeleteAsync(id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
