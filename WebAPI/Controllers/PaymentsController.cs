using Microsoft.AspNetCore.Mvc;
using TastyByThreeMusketeers.Applications.Services;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        public PaymentsController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody] Payment payment)
        {
            var result = await _paymentService.MakePaymentAsync(payment);
            return Ok(result);
        }
    }
}
