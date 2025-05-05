namespace TastyByThreeMusketeers.WebAPI.DTOs
{
    public class PaymentResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
    }
}
