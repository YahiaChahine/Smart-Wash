using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;

namespace SmartWash.Application.BookingSystem
{
    public interface IBookingService
    {
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking> CancelBookingAsync(int bookingId);
        Task<Booking> ModifyBookingAsync(Booking updatedBooking);
    }
}
