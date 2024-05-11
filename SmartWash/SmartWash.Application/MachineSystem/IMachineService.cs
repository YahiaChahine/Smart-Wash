using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;

namespace SmartWash.Application.MachineSystem
{
    public interface IMachineService
    {
        Task<IEnumerable<Machine>> GetAvailableMachinesAsync(DateTime startTime, DateTime endTime);
    }
}
