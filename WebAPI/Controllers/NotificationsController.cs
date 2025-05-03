using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly CanteenDbContext _context;
        public NotificationsController(CanteenDbContext context)
        {
            _context = context;
        }
        // GET: api/Notifications/User/{userId}
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetUserNotifications(Guid userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
        // POST: api/Notifications
        [HttpPost]
        public async Task<IActionResult> PostNotification(Notification notification)
        {
            notification.NotificationId = Guid.NewGuid();
            notification.CreatedAt = DateTime.UtcNow;
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUserNotifications", new { userId = notification.UserId }, notification);
        }
    }
}
