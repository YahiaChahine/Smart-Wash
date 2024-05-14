using SmartWash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWash.Domain.Interfaces
{

	public interface IMachineRepository
	{
		Task<Machine> GetByIdAsync(int id);
		Task<Machine> AddAsync(Machine machine);
		Task UpdateAsync(Machine machine);
		Task DeleteAsync(int id);
		Task<IEnumerable<Machine>> GetAllAsync();
        Task UpdateStatus(int machineId, string status);

    }

}
