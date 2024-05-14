using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SmartWash.Application.UserSystem;
using SmartWash.Domain.Entities;
using SmartWash.Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SmartWash.Infrastructure.SignalR
{
    public class LaundryHub : Hub
    {
        public async Task BookMachine()
        {
            // Code to book the machine...

            // Notify all clients that the machine has been booked
            await Clients.Others.SendAsync("MachineBooked");
        }

        public async Task SendFeedback()
        {
            await Clients.All.SendAsync("FeedbackReceived");
        }

        public async Task SendReply(string userId)
        {
            await Clients.User(userId).SendAsync("ReplyReceived");
        }

        public class UserConnection
        {
            public string ConnectionId { get; set; }
            public string UserId { get; set; }
            public string Role { get; set; }
        }
    }
}
