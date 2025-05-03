using TastyByThreeMusketeers.Core.Models;

namespace TastyByThreeMusketeers.Core.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> GetPaymentByOrderIdAsync(int orderId);
    }
}
