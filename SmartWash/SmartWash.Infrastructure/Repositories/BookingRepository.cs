using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly DataContext _context;

        public BookingRepository(DataContext context)
        {
			_context = context;
		}

        public async Task<Booking> AddAsync(Booking booking)
        {
			var result = await _context.Bookings.AddAsync(booking);
			await _context.SaveChangesAsync();
			return result.Entity;
		}
		public async Task<Booking> GetByIdAsync(int bookingId)
		{
			return await _context.Bookings.FindAsync(bookingId);
		}
		public async Task<IEnumerable<Booking>> GetAllAsync()
		{
			return await _context.Bookings.ToListAsync();
		}
		public async Task UpdateAsync(Booking booking)
		{
			_context.Bookings.Update(booking);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int bookingId)
		{
			var booking = await GetByIdAsync(bookingId);
			if (booking != null)
			{
				_context.Bookings.Remove(booking);
				await _context.SaveChangesAsync();
			}
		}
    }
}
