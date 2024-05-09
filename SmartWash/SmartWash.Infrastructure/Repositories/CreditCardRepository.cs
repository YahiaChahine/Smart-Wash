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
	public class CreditCardRepository : ICreditCardRepository
	{
		private readonly DataContext _context;

		public CreditCardRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<CreditCard> AddAsync(CreditCard creditcard)
		{
			var result = await _context.CreditCards.AddAsync(creditcard);
			await _context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			var creditcard = await GetByIdAsync(id);
			if (creditcard != null)
			{
				_context.CreditCards.Remove(creditcard);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<CreditCard>> GetAllAsync()
		{
			return await _context.CreditCards.ToListAsync();
		}

		public async Task<CreditCard> GetByIdAsync(int id)
		{
			return await _context.CreditCards.FindAsync(id);
		}

		public async Task UpdateAsync(CreditCard creditcard)
		{
			_context.CreditCards.Update(creditcard);
			await _context.SaveChangesAsync();
		}

		//get by user id
		public async Task<IEnumerable<CreditCard>> GetByUserAsync(int userID)
		{
			return await _context.CreditCards
			.Where(f => f.UserId == userID)
			.ToListAsync();
		}


	}
}
