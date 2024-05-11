using Microsoft.EntityFrameworkCore;
using SmartWash.Domain.Entities;
using SmartWash.Domain.Interfaces;
using SmartWash.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWash.Infrastructure.Repositories
{
	//build feedback repository
	public class FeedbackRepository : IFeedbackRepository
	{
		private readonly DataContext _context;
		public FeedbackRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<Feedback> AddAsync(Feedback feedback)
		{
			var result = await _context.Feedbacks.AddAsync(feedback);
			await _context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			var feedback = await GetByIdAsync(id);
			if (feedback != null)
			{
				_context.Feedbacks.Remove(feedback);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Feedback>> GetAllAsync()
		{
			return await _context.Feedbacks.ToListAsync();
		}

		public async Task<Feedback> GetByIdAsync(int id)
		{
			return await _context.Feedbacks.FindAsync(id);
		}

		public async Task<IEnumerable<Feedback>> GetByUserAsync(string userID)
		{
			return await _context.Feedbacks
				.Where(f => f.UserId == userID)
				.ToListAsync();
		}

		public async Task UpdateAsync(Feedback feedback)
		{
			_context.Feedbacks.Update(feedback);
			await _context.SaveChangesAsync();
		}



	}
}
