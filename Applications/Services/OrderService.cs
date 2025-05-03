using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Core.Models.Enums;
using TastyByThreeMusketeers.Infrastructure.Data;
using TastyByThreeMusketeers.WebAPI.DTOs;

namespace TastyByThreeMusketeers.Applications.Services
{
    public class OrderService 
    {
        private readonly IOrderRepository _orderRepository;
        private readonly INotificationService _notificationService;
        private readonly CanteenConfig _canteenConfig;
        public OrderService(
            IOrderRepository orderRepository,
            INotificationService notificationService, CanteenConfig canteenConfig)
        {
            _orderRepository = orderRepository;
            _notificationService = notificationService;
            _canteenConfig = canteenConfig;
        }
        public async Task<object> PlaceOrderAsync(OrderDTO orderDTO)
        {
            
            if (!_canteenConfig.IsDeskModeAvailable)
            {
                return "Desk Mode is currently unavailable. Please try later.";
            }
            var order = new Order
            {
                UserId = orderDTO.UserId,
                IsDeskOrder = orderDTO.IsDeskMode,
                SeatNumber = orderDTO.SeatNumber,
                OrderSource = orderDTO.OrderSource,
                OrderStatus = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                Items = orderDTO.Items.Select(item => new OrderItem
                {   Name = item.Name,
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
            await _orderRepository.AddAsync(order);
            await _notificationService.NotifyOrderStatusChangeAsync(order.Id, "Order placed successfully.");
            return order.Id;
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId)
        {
            return await _orderRepository.GetOrdersByUserAsync(userId);
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }
        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) throw new Exception("Order not found.");
            order.OrderStatus = status;
            await _orderRepository.UpdateAsync(order);
            await _notificationService.NotifyOrderStatusChangeAsync(orderId, $"Order status updated to {status}.");
        }
        public async Task<IEnumerable<Order>> GetActiveOrdersAsync()
        {
            return await _orderRepository.GetActiveOrdersAsync();
        }
    }
    public interface IOrderService
    {
    }

}
