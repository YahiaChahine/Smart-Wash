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
		private readonly IDbContextFactory<DataContext> _contextFactory;
		public MachineRepository(IDbContextFactory<DataContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<Machine> AddAsync(Machine machine)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			var result = await context.Machines.AddAsync(machine);
			await context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task<Machine> GetByIdAsync(int machineId)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.Machines.FindAsync(machineId);
		}

		public async Task<IEnumerable<Machine>> GetAllAsync()
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.Machines.ToListAsync();
		}

		public async Task UpdateAsync(Machine machine)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			context.Machines.Update(machine);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int machineId)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			var machine = await GetByIdAsync(machineId);
			if (machine != null)
			{
				context.Machines.Remove(machine);
				await context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Machine>> GetAvailableMachinesAsync()
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.Machines.Where(m => m.IsAvailable).ToListAsync();
		}

        public async Task UpdateStatus(int machineId, string status)
        {
			await using var context = await _contextFactory.CreateDbContextAsync();

            var machine = await GetByIdAsync(machineId);
            if (machine != null)
            {
				machine.Status = status;
				context.Machines.Update(machine);
                await context.SaveChangesAsync();
            }
        }

	}
	
}
