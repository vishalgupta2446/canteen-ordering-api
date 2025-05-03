namespace TastyByThreeMusketeers.Core.Models.Enums
{
    public enum PaymentMethod
    {
        Cash,
        Card,
        UPI
    }
    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed
    }
    public enum OrderStatus
    {
        Pending,
        Preparing,
        Completed,
        Cancelled
    }
}
