using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Core.Models.Enums;

namespace TastyByThreeMusketeers.Applications.Services
{
    public class PaymentService 
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly INotificationService _notificationService;
        public PaymentService(
            IPaymentRepository paymentRepository,
            IOrderRepository orderRepository,
            INotificationService notificationService)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _notificationService = notificationService;
        }
        public async Task<Payment> MakePaymentAsync(Payment payment)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(payment.OrderId);
            if (existingOrder == null) throw new Exception("Order not found.");
            payment.PaidAt = DateTime.UtcNow;
            await _paymentRepository.AddAsync(payment);
            if (payment.PaymentStatus == PaymentStatus.Completed)
            {
                existingOrder.OrderStatus = OrderStatus.Confirmed;
                await _orderRepository.UpdateAsync(existingOrder);
                await _notificationService.NotifyOrderStatusChangeAsync(existingOrder.Id, "Payment received. Order confirmed.");
            }
            return payment;
        }
        public async Task<Payment> GetPaymentByOrderIdAsync(int orderId)
        {
            return await _paymentRepository.GetPaymentByOrderIdAsync(orderId);
        }
    }
    public interface IPaymentService
    {
    }

}
