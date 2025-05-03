using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;

namespace TastyByThreeMusketeers.Applications.Services
{
    public class PaymentService

    {

        private readonly IPaymentRepository _paymentRepository;

        private readonly IOrderRepository _orderRepository;

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository)

        {

            _paymentRepository = paymentRepository;

            _orderRepository = orderRepository;

        }

        public async Task<Payment> ProcessPaymentAsync(Payment paymentDto)

        {

            // Step 1: Retrieve the order details

            var order = await _orderRepository.GetByIdAsync(paymentDto.OrderId);

            if (order == null)

            {

                return null; // Order not found

            }

            // Step 2: Create payment entry

            var payment = new Payment

            {

                PaymentId = Guid.NewGuid(),

                OrderId = paymentDto.OrderId,

                Amount = order.TotalAmount,

                PaymentStatus = PaymentStatus.Pending,

                PaymentMethod = paymentDto.PaymentMethod,

                TransactionId = paymentDto.TransactionId,

                PaidAt = DateTime.UtcNow

            };

            // Step 3: Save payment information

            await _paymentRepository.AddAsync(payment);

            // Step 4: Update order status to "Paid"

            order.OrderStatus = OrderStatus.Completed;

            await _orderRepository.UpdateAsync(order);

            return payment;

        }

    }

}
