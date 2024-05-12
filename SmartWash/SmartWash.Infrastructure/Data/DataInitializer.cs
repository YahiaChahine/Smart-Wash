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

        }
    }
}
