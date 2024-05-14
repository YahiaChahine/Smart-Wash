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
	public class ReplyRepository : IReplyRepository
	{
		public readonly IDbContextFactory<DataContext> _contextFactory;

		public ReplyRepository(IDbContextFactory<DataContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<Reply> AddAsync(Reply reply)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			var result = await context.Replies.AddAsync(reply);
			await context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			var reply = await GetByIdAsync(id);
			if (reply != null)
			{
                context.Replies.Remove(reply);
				await context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Reply>> GetAllAsync()
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.Replies.ToListAsync();
		}

		public async Task<Reply> GetByIdAsync(int id)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();
			
            return await context.Replies.FindAsync(id);
		}

		public async Task<IEnumerable<Reply>> GetByFeedbackAsync(int feedbackID)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.Replies
				.Where(r => r.FeedbackId == feedbackID)
				.ToListAsync();
		}

		public async Task UpdateAsync(Reply reply)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

            context.Replies.Update(reply);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Reply>> GetByAdminAsync(string adminID)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();
		
            return await context.Replies
				.Where(r => r.UserId == adminID)
				.ToListAsync();
		}
	}
}
