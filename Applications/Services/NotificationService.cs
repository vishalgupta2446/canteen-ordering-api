using System.Threading.Tasks;
using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;

namespace TastyByThreeMusketeers.Applications.Services
{
    public class NotificationService :INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task NotifyOrderStatusChangeAsync(int orderId, string statusMessage)
        {
            var notification = new Notification
            {
                OrderId = orderId,
                Message = statusMessage,
                CreatedAt = DateTime.UtcNow
            };
            await _notificationRepository.AddAsync(notification);
        }
        public async Task<IEnumerable<Notification>> GetNotificationsByUserAsync(int userId)
        {
            return await _notificationRepository.GetNotificationsByUserAsync(userId);
        }
    }
    public interface INotificationService 
    {
        Task<IEnumerable<Notification>> GetNotificationsByUserAsync(int userId);
        Task NotifyOrderStatusChangeAsync(int orderId, string statusMessage);
    }
}
