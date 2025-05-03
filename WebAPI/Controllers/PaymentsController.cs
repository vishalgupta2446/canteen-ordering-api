using Microsoft.AspNetCore.Mvc;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly CanteenDbContext _context;
        public PaymentsController(CanteenDbContext context)
        {
            _context = context;
        }
        // POST: api/Payments
        [HttpPost]
        public async Task<IActionResult> PostPayment(Payment payment)
        {
            payment.PaymentId = Guid.NewGuid();
            payment.PaidAt = DateTime.UtcNow;
            _context.Payments.Add(payment);
            var order = await _context.Orders.FindAsync(payment.OrderId);
            if (order != null)
            {
                order.PaymentStatus = PaymentStatus.Paid;
                _context.Orders.Update(order);
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPayment", new { id = payment.PaymentId }, payment);
        }
        // GET: api/Payments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(Guid id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return payment;
        }
    }
}
