using TastyByThreeMusketeers.Core.Models.Enums;

namespace TastyByThreeMusketeers.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsDeskOrder { get; set; }
        public string SeatNumberCabinNumber { get; set; }
        public OrderSource OrderSource { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public User User { get; set; }
        public Payment Payment { get; set; }
        public ICollection<Notification> Notifications { get; set; }

    }
    
}
