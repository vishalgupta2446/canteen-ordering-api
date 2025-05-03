namespace TastyByThreeMusketeers.Core.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TastyByThreeMusketeers.Core.Models.Enums;

    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; } // Enum-like: Card, UPI, Cash
        [Required]
        public PaymentStatus PaymentStatus { get; set; } // Enum-like: Pending, Paid
        public string TransactionId { get; set; }
        public DateTime PaidAt { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
