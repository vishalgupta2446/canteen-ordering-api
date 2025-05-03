using Microsoft.EntityFrameworkCore;
using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.Core.Repositories
{
    public class OrderItemRepository : IRepository<OrderItem>

    {

        private readonly CanteenDbContext _context;

        public OrderItemRepository(CanteenDbContext context)

        {

            _context = context;

        }

        public async Task AddAsync(OrderItem entity)

        {

            await _context.OrderItems.AddAsync(entity);

            await SaveAsync();

        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()

        {

            return await _context.OrderItems.ToListAsync();

        }

        public async Task<OrderItem> GetByIdAsync(Guid id)

        {

            return await _context.OrderItems.FirstOrDefaultAsync(oi => oi.OrderItemId == id);

        }

        public async Task UpdateAsync(OrderItem entity)

        {

            _context.OrderItems.Update(entity);

            await SaveAsync();

        }

        public async Task DeleteAsync(Guid id)

        {

            var orderItem = await GetByIdAsync(id);

            if (orderItem != null)

            {

                _context.OrderItems.Remove(orderItem);

                await SaveAsync();

            }

        }

        public async Task SaveAsync()

        {

            await _context.SaveChangesAsync();

        }

    }

}
