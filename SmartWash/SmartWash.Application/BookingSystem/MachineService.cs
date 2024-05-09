using SmartWash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Interfaces;

namespace SmartWash.Application.BookingSystem
{
	public class MachineService
	{
		private readonly IMachineRepository _machineRepository;

		public MachineService(IMachineRepository machineRepository)
		{
			_machineRepository = machineRepository;
		}

		public async Task<Machine> CreateMachineAsync(Machine machine)
		{
			// Check if the booking does not overlap with another booking

			var createdBooking = await _machineRepository.AddAsync(machine);

			return createdBooking;
		}

		public async Task<Machine> CancelMachineAsync(int machineId)
		{
			var machine = await _machineRepository.GetByIdAsync(machineId);
			if (machine == null)
			{
				throw new Exception("Machine not found");
			}

			machine.Cancel();

			await _machineRepository.UpdateAsync(machine);

			return machine;
		}

		public async Task<Machine> ModifyMachineAsync(Machine updatedMachine)
		{
			var machine = await _machineRepository.GetByIdAsync(updatedMachine.ID);
			if (machine == null)
			{
				throw new Exception("Machine not found");
			}

			// Apply changes and business logic

			await _machineRepository.UpdateAsync(updatedMachine);

			return updatedMachine;
		}
	}
}
