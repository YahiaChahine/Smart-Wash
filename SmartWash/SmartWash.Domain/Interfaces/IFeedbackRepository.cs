using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;

namespace SmartWash.Domain.Interfaces
{
	public interface IFeedbackRepository
	{
		Task<Feedback> GetByIdAsync(int id);
		Task<Feedback> AddAsync(Feedback feedback);
		Task UpdateAsync(Feedback feedback);
		Task DeleteAsync(int id);
		Task<IEnumerable<Feedback>> GetByUserAsync(int userID);
		Task<IEnumerable<Feedback>> GetAllAsync();
	}
}
