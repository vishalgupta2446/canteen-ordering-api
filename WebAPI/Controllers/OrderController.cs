using Microsoft.AspNetCore.Mvc;
using TastyByThreeMusketeers.Applications.Services;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Core.Models.Enums;
using TastyByThreeMusketeers.WebAPI.DTOs;

namespace TastyByThreeMusketeers.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderDTO orderDto)
        {
            var result = await _orderService.PlaceOrderAsync(orderDto);
            return Ok(result);
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var orders = await _orderService.GetOrdersByUserAsync(userId);
            return Ok(orders);
        }
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromQuery] OrderStatus status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            return Ok(orderId);
        }
    }
}
