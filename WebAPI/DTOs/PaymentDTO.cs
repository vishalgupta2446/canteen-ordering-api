namespace TastyByThreeMusketeers.WebAPI.DTOs
{
    public class PaymentDTO
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }  // e.g., UPI, CreditCard
        public string UpiId { get; set; }          // For UPI, optional for others
        public string TransactionId { get; set; }  // Gateway Txn ID (optional)
        public string Status { get; set; }         // Pending, Success, Failed
        public DateTime PaymentDate { get; set; }
    }
}
