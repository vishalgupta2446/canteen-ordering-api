namespace TastyByThreeMusketeers.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SeatNumber { get; set; }
    }
    public enum MembershipStatus
    {
        Regular,
        Member
    }
}
