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
		private readonly DataContext _context;

		public OfferRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<Offer> AddAsync(Offer offer)
		{
			var result = await _context.Offers.AddAsync(offer);
			await _context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			var offer = await GetByIdAsync(id);
			if (offer != null)
			{
				_context.Offers.Remove(offer);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Offer>> GetAllAsync()
		{
			return await _context.Offers.ToListAsync();
		}

		public async Task<Offer> GetByIdAsync(int id)
		{
			return await _context.Offers.FindAsync(id);
		}

		public async Task UpdateAsync(Offer offer)
		{
			_context.Offers.Update(offer);
			await _context.SaveChangesAsync();
		}
	}
}
