using TastyByThreeMusketeers.Core.Models;

namespace TastyByThreeMusketeers.Core.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetNotificationsByUserAsync(int userId);
    }
}
