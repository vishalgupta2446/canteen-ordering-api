using Microsoft.EntityFrameworkCore;
using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.Core.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly CanteenDbContext _context;
        public NotificationRepository(CanteenDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Notification>> GetNotificationsByUserAsync(int userId)
        {
            return await _context.Notifications
                .Include(n => n.Order)
                .Where(n => n.Order.UserId == userId)
                .ToListAsync();
        }
    }
}
