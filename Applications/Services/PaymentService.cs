using System;
using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Core.Models.Enums;
using TastyByThreeMusketeers.WebAPI.DTOs;

namespace TastyByThreeMusketeers.Applications.Services
{
    public class PaymentService : IPaymentService

    {

        private readonly IOrderRepository _orderRepository;

        private readonly IPaymentRepository _paymentRepository;

        private readonly INotificationService _notificationService;

        public PaymentService(

            IOrderRepository orderRepository,

            IPaymentRepository paymentRepository,

            INotificationService notificationService)

        {

            _orderRepository = orderRepository;

            _paymentRepository = paymentRepository;

            _notificationService = notificationService;

        }

        public async Task<Payment> MakePaymentAsync(Payment payment)

        {

            var existingOrder = await _orderRepository.GetByIdAsync(payment.OrderId);

            if (existingOrder == null)

                throw new Exception("Order not found.");

            payment.PaymentDate = DateTime.UtcNow;

            await _paymentRepository.AddAsync(payment);

            if (payment.PaymentStatus == PaymentStatus.Completed)

            {

                existingOrder.OrderStatus = OrderStatus.Confirmed;

                await _orderRepository.UpdateAsync(existingOrder);

                await _notificationService.NotifyOrderStatusChangeAsync(

                    existingOrder.Id,

                    "Payment received. Order confirmed."

                );

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
        Task<Payment> MakePaymentAsync(Payment payment);
        Task<Payment> GetPaymentByOrderIdAsync(int orderId);
    }

}
