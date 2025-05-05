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
        private readonly CanteenDbContext _context;
        public OrderService(
            IOrderRepository orderRepository,
            INotificationService notificationService, CanteenConfig canteenConfig, CanteenDbContext context)
        {
            _orderRepository = orderRepository;
            _notificationService = notificationService;
            _canteenConfig = canteenConfig;
            _context = context;
        }
        public async Task<OperationResult<Order>> PlaceOrderAsync(OrderDTO orderDTO)
        {
            foreach (var item in orderDTO.Items)
            {
                var unavailableItems = new List<int>();
                
                    MenuItem menuItem =  _context.MenuItems.FirstOrDefault(a=>a.Id==item.MenuItemId);
                    if (!menuItem.IsAvailable)
                    {
                        unavailableItems.Add(menuItem.Id);
                    }
                
                if (unavailableItems.Any())
                {
                    // Notify the user about the unavailable items
                    var message = $"The following items are out of stock: {string.Join(", ", unavailableItems)}.";
                    await _notificationService.NotifyOrderStatusChangeAsync(orderDTO.UserId, message);
                   return new OperationResult<Order>() { IsSuccess = false, Message = message };
                   
                }
            }

            if (!_canteenConfig.IsDeskModeAvailable)
            {
                return new OperationResult<Order>() { IsSuccess = false, Message = "Desk Mode is currently unavailable. Please try later." };
               
            }
            var order = new Order
            {
                UserId = orderDTO.UserId,
                IsDeskOrder = orderDTO.IsDeskOrder,
                SeatNumberCabinNumber = orderDTO.SeatNumberCabinNumber,
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
            var c_order =await _orderRepository.AddAsync(order);
            await _notificationService.NotifyOrderStatusChangeAsync(order.Id, "Order placed successfully.");
            return new OperationResult<Order>() { IsSuccess = true, Message = "Order placed successfully.", Data= c_order };
            
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
