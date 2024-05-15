using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;

namespace SmartWash.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> GetByIdAsync(int id);
        Task<Notification> AddAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(int id);
        Task<IEnumerable<Notification>> GetByUserAsync(string userID);
        Task<IEnumerable<Notification>> GetAllAsync();
    }
}
