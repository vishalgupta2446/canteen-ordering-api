namespace TastyByThreeMusketeers.Core.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
    }
}
