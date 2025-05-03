using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")] // Protect it with Admin Role
    [Route("api/[controller]")]
    [ApiController]
    public class CanteenController : ControllerBase
    {
        private readonly CanteenConfig _canteenConfig;
        public CanteenController(CanteenConfig canteenConfig)
        {
            _canteenConfig = canteenConfig;
        }
        [HttpPost("toggleDeskMode")]
        public IActionResult ToggleDeskMode()
        {
            _canteenConfig.IsDeskModeAvailable = !_canteenConfig.IsDeskModeAvailable;
            return Ok(new { message = $"Desk Mode is now {(_canteenConfig.IsDeskModeAvailable ? "enabled" : "disabled")}" });
        }

    }

}
