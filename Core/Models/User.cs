namespace TastyByThreeMusketeers.Core.Models
{
    public class User
    {
        public int Id { get; set; }  // Primary Key
        public string Username { get; set; }
        public string PasswordHash { get; set; }  // Store hashed password!
        public string Role { get; set; }  // e.g., Admin, Customer
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public enum MembershipStatus
    {
        Regular,
        Member
    }
}
