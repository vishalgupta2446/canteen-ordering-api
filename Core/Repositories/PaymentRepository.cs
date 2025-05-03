using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.Core.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(CanteenDbContext context) : base(context)
        {
        }
    }
}
