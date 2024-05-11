using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;
using SmartWash.Domain.Interfaces;

namespace SmartWash.Application.MachineSystem
{
    public class MachineService : IMachineService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMachineRepository _machineRepository;


        public MachineService(IBookingRepository bookingRepository, IMachineRepository machineRepository)
        {
            _bookingRepository = bookingRepository;
            _machineRepository = machineRepository;
        }

        public async Task<IEnumerable<Machine>> GetAvailableMachinesAsync(DateTime startTime, DateTime endTime)
        {
            var bookings = await _bookingRepository.GetAllAsync();
            var machines = await _machineRepository.GetAllAsync();

            var availableMachines = machines.Where(m =>
            {
                var overlappingBooking = bookings.FirstOrDefault(b => b.MachineId == m.ID && b.StartTime < endTime && b.EndTime > startTime);
                return overlappingBooking == null;
            });

            return availableMachines;
        }
    }
}
