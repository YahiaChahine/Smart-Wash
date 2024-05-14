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
		private readonly IDbContextFactory<DataContext> _contextFactory;

		public CreditCardRepository(IDbContextFactory<DataContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<CreditCard> AddAsync(CreditCard creditcard)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			var result = await context.CreditCards.AddAsync(creditcard);
			await context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			var creditcard = await GetByIdAsync(id);
			if (creditcard != null)
			{
                context.CreditCards.Remove(creditcard);
				await context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<CreditCard>> GetAllAsync()
		{
			await using var context = await _contextFactory.CreateDbContextAsync();
			
            return await context.CreditCards.ToListAsync();
		}

		public async Task<CreditCard> GetByIdAsync(int id)
		{	
			await using var context = await _contextFactory.CreateDbContextAsync();
			return await context.CreditCards.FindAsync(id);
		}

		public async Task UpdateAsync(CreditCard creditcard)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();
            context.CreditCards.Update(creditcard);
			await context.SaveChangesAsync();
		}

		//get by user id
		public async Task<IEnumerable<CreditCard>> GetByUserAsync(string userID)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.CreditCards
			.Where(f => f.UserId == userID)
			.ToListAsync();
		}


	}
}
