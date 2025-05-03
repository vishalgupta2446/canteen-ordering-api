using Microsoft.AspNetCore.Mvc;
using TastyByThreeMusketeers.Applications.Services;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.WebAPI.DTOs;

namespace TastyByThreeMusketeers.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrderAsync([FromBody] OrderDTO order)
        {
            await _orderService.PlaceOrderAsync(order);
            return Ok(order);
        }
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderStatus status)
        {
           // await _orderService.UpdateOrderStatus(id, status);
            return Ok();
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrdersByUser(Guid userId)
        {
            //var orders = await _orderService.GetOrdersByUser(userId);
            return Ok(orders);
        }
    }
}
