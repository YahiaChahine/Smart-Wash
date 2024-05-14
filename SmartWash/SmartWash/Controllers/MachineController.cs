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

        public MachineController(IHubContext<LaundryHub> hubContext, IMachineRepository machineRepository)
        {
            _hubContext = hubContext;
            _machineRepository = machineRepository;
        }

        [HttpPost("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] MachineStatusUpdate update)
        {
            await _hubContext.Clients.All
                .SendAsync("MachineStatusUpdated", update.MachineId, update.Status);

            await _machineRepository.UpdateStatus(update.MachineId, update.Status);

            return Ok();
        }

        public class MachineStatusUpdate
        {
            public int MachineId { get; set; }
            public string Status { get; set; }
        }
    }
}
