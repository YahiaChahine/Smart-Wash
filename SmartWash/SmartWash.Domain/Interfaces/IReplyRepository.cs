using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;

namespace SmartWash.Domain.Interfaces
{
	public interface IReplyRepository
	{
		Task<Reply> GetByIdAsync(int id);
		Task<Reply> AddAsync(Reply reply);
		Task UpdateAsync(Reply reply);
		Task DeleteAsync(int id);
		Task<IEnumerable<Reply>> GetByAdminAsync(int adminID);
		Task<IEnumerable<Reply>> GetByFeedbackAsync(int feedbackID);
		Task<IEnumerable<Reply>> GetAllAsync();
	}
}
