using SmartWash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Interfaces;

namespace SmartWash.Application.BookingSystem
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            // Check if the booking does not overlap with another booking
            var bookings = await _bookingRepository.GetAllAsync();
            var overlappingBooking = bookings.FirstOrDefault(b => b.StartTime < booking.EndTime && b.EndTime > booking.StartTime);
            if (overlappingBooking != null)
            {
                throw new Exception("Booking overlaps with another booking");
            }

            var createdBooking = await _bookingRepository.AddAsync(booking);

            //Add reward points
            if (booking.User is not null)
                booking.User.PointNum += 10;

            return createdBooking;
        }


        public async Task<Booking> CancelBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
            {
                throw new Exception("Booking not found");
            }

            booking.Cancel();

            await _bookingRepository.UpdateAsync(booking);

            return booking;
        }

        public async Task<Booking> ModifyBookingAsync(Booking updatedBooking)
        {
            var booking = await _bookingRepository.GetByIdAsync(updatedBooking.ID);
            if (booking == null)
            {
                throw new Exception("Booking not found");
            }

            // Apply changes and business logic

            await _bookingRepository.UpdateAsync(updatedBooking);

            return updatedBooking;
        }
    }

}
