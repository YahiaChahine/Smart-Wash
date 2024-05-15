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
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public NotificationRepository(IDbContextFactory<DataContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Notification> AddAsync(Notification notification)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var result = await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var notification = await GetByIdAsync(id);
            if (notification != null)
            {
                context.Notifications.Remove(notification);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            return await context.Notifications.ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Notifications.FindAsync(id);
        }

        public async Task UpdateAsync(Notification notification)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Notifications.Update(notification);
            await context.SaveChangesAsync();
        }

        //get by user id
        public async Task<IEnumerable<Notification>> GetByUserAsync(string userID)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            return await context.Notifications
            .Where(f => f.UserID == userID)
            .ToListAsync();
        }


    }
}
