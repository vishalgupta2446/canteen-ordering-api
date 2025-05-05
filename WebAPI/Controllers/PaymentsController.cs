using Microsoft.AspNetCore.Mvc;
using TastyByThreeMusketeers.Applications.Services;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Core.Models.Enums;
using TastyByThreeMusketeers.Infrastructure.Data;
using TastyByThreeMusketeers.WebAPI.DTOs;

namespace TastyByThreeMusketeers.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] Payment payment)
        {
            var result = await _paymentService.MakePaymentAsync(payment);
            if ((result.PaymentStatus == PaymentStatus.Completed))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
