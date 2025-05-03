using Microsoft.EntityFrameworkCore;
using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.Core.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>
    {
        public OrderItemRepository(CanteenDbContext context) : base(context)
        {
        }
    }

}
