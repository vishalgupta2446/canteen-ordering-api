namespace TastyByThreeMusketeers.WebAPI.DTOs
{
    public class OrderDTO
    {
        public Guid UserId { get; set; }  // User placing the order
        public List<OrderItemDTO> Items { get; set; }  // List of items in the order
    }
    public class OrderItemDTO
    {
        public Guid MenuItemId { get; set; }  // ID of the menu item being ordered
        public int Quantity { get; set; }     // Quantity of the menu item
        public decimal Price { get; set; }    // Price of the menu item
    }
}
