using TastyByThreeMusketeers.Core.Models;

namespace TastyByThreeMusketeers.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId);
        Task<IEnumerable<Order>> GetActiveOrdersAsync();
    }
}
