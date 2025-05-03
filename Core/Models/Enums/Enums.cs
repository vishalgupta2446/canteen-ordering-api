namespace TastyByThreeMusketeers.Core.Models.Enums
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Preparing,
        Prepared,
        Completed
    }
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed
    }
    public enum OrderSource
    {
        Online,
        Counter
    }
}
