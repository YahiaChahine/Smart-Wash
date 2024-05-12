using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace SmartWash.Application.SignalRSystem
{
    public class SignalRService : ISignalRService
    {
        private HubConnection _hubConnection;
        private readonly NavigationManager _navigationManager;

        public SignalRService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            CreateHubConnection();
        }

        private void CreateHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_navigationManager.ToAbsoluteUri("/laundry-hub"))
                .Build();
        }

        public async Task StartAsync()
        {
            CreateHubConnection();
            await _hubConnection.StartAsync();
        }

        public async Task SendBookingAsync()
        {
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.SendAsync("BookMachine");
            }
        }

        public void OnMachineBooked(Action handler)
        {
            _hubConnection.On("MachineBooked", handler);
        }
    }

}
