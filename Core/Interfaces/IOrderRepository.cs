using TastyByThreeMusketeers.Core.Models;

namespace TastyByThreeMusketeers.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetByIdAsync(Guid userId);
        Task<IEnumerable<Order>> GetOrdersByUser(Guid userId);
        Task UpdateOrderStatus(Guid orderId, OrderStatus status);
    }
}
