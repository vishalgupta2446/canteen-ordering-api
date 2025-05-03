namespace TastyByThreeMusketeers.Core.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public MembershipStatus MembershipStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public enum MembershipStatus
    {
        Regular,
        Member
    }
}
