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
		public readonly DataContext _context;

		public ReplyRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<Reply> AddAsync(Reply reply)
		{
			var result = await _context.Replies.AddAsync(reply);
			await _context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			var reply = await GetByIdAsync(id);
			if (reply != null)
			{
				_context.Replies.Remove(reply);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Reply>> GetAllAsync()
		{
			return await _context.Replies.ToListAsync();
		}

		public async Task<Reply> GetByIdAsync(int id)
		{
			return await _context.Replies.FindAsync(id);
		}

		public async Task<IEnumerable<Reply>> GetByFeedbackAsync(int feedbackID)
		{
			return await _context.Replies
				.Where(r => r.FeedbackId == feedbackID)
				.ToListAsync();
		}

		public async Task UpdateAsync(Reply reply)
		{
			_context.Replies.Update(reply);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Reply>> GetByAdminAsync(int adminID)
		{
			return await _context.Replies
				.Where(r => r.AdminId == adminID)
				.ToListAsync();
		}
	}
}
