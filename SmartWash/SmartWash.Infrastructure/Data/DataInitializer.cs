using Stripe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace SmartWash.Infrastructure.Data
{
    public static class DataInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Machines.Any())
            {
                var machines = new Machine[]
                {
                    new() {Type=MachineType.WashingMachine},
                    new() {Type=MachineType.DryingMachine},
                    new() {Type=MachineType.WashingMachine},
                    new() {Type=MachineType.WashingMachine},
                    new() {Type=MachineType.DryingMachine},
                    new() {Type=MachineType.WashingMachine},
                    new() {Type=MachineType.WashingMachine},
                };
                
                foreach (var m in machines)
                {
                    context.Machines.Add(m);
                }
                
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var hasher = new PasswordHasher<ApplicationUser>();

                var users = new ApplicationUser[]
                {
                    new() { Id = "1", FullName="Waleed",UserName = "admin", PasswordHash = hasher.HashPassword(null, "admin") },
                    new() { Id = "2", FullName="Muath",UserName = "user", PasswordHash = hasher.HashPassword(null, "user") },
                };

                foreach (var u in users)
                {
                    context.Users.Add(u);
                }

                context.SaveChanges();

            }

            if (!context.Bookings.Any())
            {
                var bookings = new Booking[]
                {
                    //new() {MachineId=1, User = 1, StartTime=DateTime.Today.AddHours(8), CycleNum = 2},
                    //new() {MachineId=2, UserId="1", StartTime=DateTime.Today.AddHours(13), CycleNum = 1},
                };

                foreach (var booking in bookings)
                {
                    context.Bookings.Add(booking);
                }

                context.SaveChanges();
            }
        }
    }
}
