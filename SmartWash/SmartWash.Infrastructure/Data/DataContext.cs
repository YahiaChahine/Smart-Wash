﻿using SmartWash.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmartWash.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> option):base(option)
        {
            
        }

        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  
        }
    }
}
