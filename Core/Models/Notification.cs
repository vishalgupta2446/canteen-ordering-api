namespace TastyByThreeMusketeers.Core.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Notification
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public Order Order { get; set; }
    }
}
