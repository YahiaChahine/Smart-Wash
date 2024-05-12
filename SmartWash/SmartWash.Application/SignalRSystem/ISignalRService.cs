using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWash.Application.SignalRSystem
{
    public interface ISignalRService
    {
        Task StartAsync();
        Task SendBookingAsync();
        void OnMachineBooked(Action handler);
    }
}
