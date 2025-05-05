namespace TastyByThreeMusketeers.Core.Models
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }   // ✅ true/false for status
        public string Message { get; set; }   // ℹ️ custom message
        public T Data { get; set; }           // 📦 the entity/model (can be null)
    }
}
