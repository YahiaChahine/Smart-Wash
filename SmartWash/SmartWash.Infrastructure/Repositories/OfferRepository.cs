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
	public class OfferRepository : IOfferRepository
	{
		private readonly IDbContextFactory<DataContext> _contextFactory;

		public OfferRepository(IDbContextFactory<DataContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<Offer> AddAsync(Offer offer)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			var result = await context.Offers.AddAsync(offer);
			await context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			var offer = await GetByIdAsync(id);
			if (offer != null)
			{
                context.Offers.Remove(offer);
				await context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Offer>> GetAllAsync()
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.Offers.ToListAsync();
		}

		public async Task<Offer> GetByIdAsync(int id)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.Offers.FindAsync(id);
		}

		public async Task UpdateAsync(Offer offer)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

            context.Offers.Update(offer);
			await context.SaveChangesAsync();
		}
	}
}
