using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SmartWash.Infrastructure.SignalR
{
    public class LaundryHub : Hub
    {
        public async Task BookMachine()
        {
            // Code to book the machine...

            // Notify all clients that the machine has been booked
            await Clients.Others.SendAsync("MachineBooked");
        }
    }
}
