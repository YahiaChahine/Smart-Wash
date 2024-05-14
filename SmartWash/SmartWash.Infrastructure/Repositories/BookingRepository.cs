using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartWash.Domain.Entities;
using SmartWash.Domain.Interfaces;
using SmartWash.Infrastructure.Data;

namespace SmartWash.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public BookingRepository(IDbContextFactory<DataContext> contextFactory)
        {
			_contextFactory = contextFactory;
		}

        public async Task<Booking> AddAsync(Booking booking)
        {
			await using var context = await _contextFactory.CreateDbContextAsync();

			var result = await context.Bookings.AddAsync(booking);
			await context.SaveChangesAsync();
			return result.Entity;
		}
		public async Task<Booking> GetByIdAsync(int bookingId)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.Bookings.FindAsync(bookingId);
		}
		public async Task<IEnumerable<Booking>> GetAllAsync()
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			return await context.Bookings.ToListAsync();
		}
		public async Task UpdateAsync(Booking booking)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

            context.Bookings.Update(booking);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int bookingId)
		{
			await using var context = await _contextFactory.CreateDbContextAsync();

			var booking = await GetByIdAsync(bookingId);
			if (context != null)
			{
                context.Bookings.Remove(booking);
				await context.SaveChangesAsync();
			}
		}

    }
}
