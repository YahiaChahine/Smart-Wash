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
		private readonly IDbContextFactory<DataContext> _contextFactory;
		public FeedbackRepository(IDbContextFactory<DataContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<Feedback> AddAsync(Feedback feedback)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			var result = await context.Feedbacks.AddAsync(feedback);
			await context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			var feedback = await GetByIdAsync(id);
			if (feedback != null)
			{
                context.Feedbacks.Remove(feedback);
				await context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Feedback>> GetAllAsync()
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.Feedbacks.ToListAsync();
		}

		public async Task<Feedback> GetByIdAsync(int id)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();
			
            return await context.Feedbacks.FindAsync(id);
		}

		public async Task<IEnumerable<Feedback>> GetByUserAsync(string userID)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.Feedbacks
				.Where(f => f.UserId == userID)
				.ToListAsync();
		}

		public async Task UpdateAsync(Feedback feedback)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

            context.Feedbacks.Update(feedback);
			await context.SaveChangesAsync();
		}



	}
}
