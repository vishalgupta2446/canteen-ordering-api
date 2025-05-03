namespace TastyByThreeMusketeers.Applications.Services
{
    public class NotificationService
    {
        public async Task SendNotification(Guid userId, string message)
        {
            // Send notification logic (in-app, push, email, etc.)
        }
        public async Task NotifyOrderReady(Guid orderId)
        {
            // Notify the user when the order is ready
        }
    }
}
