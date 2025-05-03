namespace TastyByThreeMusketeers.Core.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public Guid MenuItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal SubTotal { get; set; } // Quantity * MenuItem.Price
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; }

        public decimal Price { get; set; }
    }
}
