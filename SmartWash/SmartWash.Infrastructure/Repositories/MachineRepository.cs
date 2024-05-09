using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartWash.Domain.Entities;
using SmartWash.Domain.Interfaces;
using SmartWash.Infrastructure.Data;

namespace SmartWash.Infrastructure.Repositories
{
	public class MachineRepository : IMachineRepository
	{
		//build the Machine Repository
		private readonly DataContext _context;
		public MachineRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<Machine> AddAsync(Machine machine)
		{
			var result = await _context.Machines.AddAsync(machine);
			await _context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task<Machine> GetByIdAsync(int machineId)
		{
			return await _context.Machines.FindAsync(machineId);
		}

		public async Task<IEnumerable<Machine>> GetAllAsync()
		{
			return await _context.Machines.ToListAsync();
		}

		public async Task UpdateAsync(Machine machine)
		{
			_context.Machines.Update(machine);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int machineId)
		{
			var machine = await GetByIdAsync(machineId);
			if (machine != null)
			{
				_context.Machines.Remove(machine);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Machine>> GetAvailableMachinesAsync()
		{
			return await _context.Machines.Where(m => m.IsAvailable).ToListAsync();
		}


	}
	
}
