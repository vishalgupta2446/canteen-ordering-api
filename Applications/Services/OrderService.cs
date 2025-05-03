using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.WebAPI.DTOs;

namespace TastyByThreeMusketeers.Applications.Services
{
    public class OrderService

    {

        private readonly IRepository<Order> _orderRepository;

        private readonly IRepository<OrderItem> _orderItemRepository;

        private readonly IRepository<Notification> _notificationRepository;

        public OrderService(

            IRepository<Order> orderRepository,

            IRepository<OrderItem> orderItemRepository,

            IRepository<Notification> notificationRepository)

        {

            _orderRepository = orderRepository;

            _orderItemRepository = orderItemRepository;

            _notificationRepository = notificationRepository;

        }

        public async Task<Order> PlaceOrderAsync(OrderDTO orderDto)

        {

            // Create Order entity

            var order = new Order

            {

                OrderId = Guid.NewGuid(),

                UserId = orderDto.UserId,

                OrderStatus = OrderStatus.Pending,

                TotalAmount = orderDto.Items.Sum(item => item.Price * item.Quantity),

                CreatedAt = DateTime.UtcNow

            };

            await _orderRepository.AddAsync(order); // Persist order

            // Create OrderItems for each item in the OrderDTO

            foreach (var item in orderDto.Items)

            {

                var orderItem = new OrderItem

                {

                    OrderItemId = Guid.NewGuid(),

                    OrderId = order.OrderId,

                    MenuItemId = item.MenuItemId,

                    Quantity = item.Quantity,

                    Price = item.Price,

                    SubTotal = item.Price * item.Quantity

                };

                await _orderItemRepository.AddAsync(orderItem); // Persist order item

            }

            // Create Notification for the user

            var notification = new Notification

            {

                UserId = orderDto.UserId,

                Message = $"Your order {order.OrderId} has been placed successfully.",

                IsRead = false,

                CreatedAt = DateTime.UtcNow

            };

            await _notificationRepository.AddAsync(notification); // Persist notification

            return order; // Return the created Order

        }

    }


}
