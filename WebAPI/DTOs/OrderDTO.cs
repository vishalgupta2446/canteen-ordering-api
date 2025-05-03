using TastyByThreeMusketeers.Core.Models.Enums;

namespace TastyByThreeMusketeers.WebAPI.DTOs
{
    public class OrderDTO
    {
        public int UserId { get; set; }
        public List<OrderItemDTO> Items { get; set; }
        public string SeatNumber { get; set; }  // For desk delivery (optional)
        public OrderSource OrderSource { get; set; }
        public bool IsDeskMode { get; set; }    // To check if it's a desk mode order
    }
    public class OrderItemDTO
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }

        public decimal Price { get; set; }
    }
}
