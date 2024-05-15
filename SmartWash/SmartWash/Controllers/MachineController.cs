using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SmartWash.Domain.Interfaces;
using SmartWash.Infrastructure.SignalR;

namespace SmartWash.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IHubContext<LaundryHub> _hubContext;
        private readonly IMachineRepository _machineRepository;
        private readonly IBookingRepository _bookingRepository;

        public MachineController(IHubContext<LaundryHub> hubContext, IMachineRepository machineRepository, IBookingRepository bookingRepository)
        {
            _hubContext = hubContext;
            _machineRepository = machineRepository;
            _bookingRepository = bookingRepository;
        }

        [HttpPost("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] MachineStatusUpdate update)
        {
            await _hubContext.Clients.All
                .SendAsync("MachineStatusUpdated", update.MachineId, update.Status);

            await _machineRepository.UpdateStatus(update.MachineId, update.Status);

            return Ok();
        }


        [HttpPost("access")]
        public async Task<IActionResult> AccessMachine([FromBody] MachineAccess access)
        {
            var bookings = await _bookingRepository.GetAllAsync();
            var booking = bookings.FirstOrDefault(b => b.MachineId == access.MachineId && b.StartTime < DateTime.Now && b.EndTime > DateTime.Now);

            if (booking == null)
            {
                return BadRequest("Booking not found");
            }

            if (booking.AccessPassword != access.Password)
            {
                return BadRequest("Invalid password");
            }

            return Ok(booking);
        }

        public class MachineStatusUpdate
        {
            public int MachineId { get; set; }
            public string Status { get; set; }
        }

        public class MachineAccess
        {
            public int MachineId { get; set; }
            public string Password { get; set; }
        }
    }
}
