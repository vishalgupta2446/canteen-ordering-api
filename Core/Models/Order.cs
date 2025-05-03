namespace TastyByThreeMusketeers.Core.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public bool IsCounterOrder { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public enum OrderStatus
    {
        Pending,
        Preparing,
        Completed,
        Cancelled
    }
    public enum PaymentStatus
    {
        Pending,
        Paid
    }
}
