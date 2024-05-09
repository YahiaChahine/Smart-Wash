using SmartWash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWash.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> GetByIdAsync(int id);
        Task<Booking> AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(int id);
        Task<IEnumerable<Booking>> GetAllAsync();
    }

}
