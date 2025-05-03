using Microsoft.EntityFrameworkCore;
using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.Core.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly CanteenDbContext _context;
        public OrderRepository(CanteenDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await SaveAsync();
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
        }
        public async Task UpdateAsync(Order entity)
        {
            _context.Orders.Update(entity);
            await SaveAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await SaveAsync();
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Order>> GetOrdersByUser(Guid userId)
        {
            return await _context.Orders.Include(a=>a.OrderItm).Where(o => o.UserId == userId).ToListAsync();
        }
        public async Task UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = status;
                _context.Orders.Update(order);
            }
        }
    }
}
