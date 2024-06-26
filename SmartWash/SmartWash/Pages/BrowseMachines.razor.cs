﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SmartWash.Application.MachineSystem;
using SmartWash.Application.SignalRSystem;
using SmartWash.Domain;
using SmartWash.Domain.Entities;

namespace SmartWash.WebUI.Pages
{
    public partial class BrowseMachines : ComponentBase
    {
        [Parameter]
        public string TypeStr { get; set; }

        [Inject] private ISignalRService SignalR { get; set; }
        [Inject] private IMachineService MachineService { get; set; }

        private MachineType? Type { get; set; }

        private DateTime? SelectedDate { get; set; } = DateTime.Today;
        private TimeSpan? SelectedTime { get; set; } = Constants.OpeningTime;
        private int Cycles { get; set; } = 1;

        private ICollection<object> SelectedTypes { get; set; }

        private IEnumerable<Machine>? AvailableMachines { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await SignalR.StartAsync();

            SignalR.OnMachineBooked(async () =>
            {
                await UpdateMachines();
            });

            Type = TypeStr switch
            {
                "washing-machine" => MachineType.WashingMachine,
                "drying-machine" => MachineType.DryingMachine,
                _ => null
            };

            SelectedTypes = Type == null ? new List<object>() : new List<object> { Type };

            await UpdateMachines();
        }

        private async Task OnTimeChoose(TimeSpan time)
        {
            SelectedTime = time;

            await UpdateMachines();
        }

        private async Task OnDateChoose(DateTime? date)
        {
            SelectedDate = date;

            await UpdateMachines();
        }

        private void OnTypeChoose(ICollection<object> selection)
        {
            SelectedTypes = selection;

            Type = (MachineType?)selection.FirstOrDefault();
        }

        private async Task OnCycleNumChoose(int cycles)
        {
            Cycles = cycles;

            await UpdateMachines();
        }

        private void OnMachineClick(Machine machine)
        {
            
            //StateContainer.ObjectTunnel[booking.GetHashCode()] = booking;

            var uri = NavigationManager.GetUriWithQueryParameters("/Account/machine", new Dictionary<string, object?>()
            {
                { "machineId", machine.ID },
                { "machineType", machine.Type.ToString() },
                { "startTime", SelectedDate.Value.Add(SelectedTime.Value) },
                { "cycleNum", Cycles },
            });

            NavigationManager.NavigateTo(uri);

            //NavigationManager.NavigateTo($"/machine/{booking.GetHashCode()}");
        }

        private async Task UpdateMachines()
        {
            if (SelectedDate == null || SelectedTime == null)
            {
                return;
            }

            var datetime = SelectedDate.Value.Add(SelectedTime.Value);
            AvailableMachines = await MachineService.GetAvailableMachinesAsync(datetime, datetime.Add(Constants.SlotDuration * Cycles));

            // Get the machines that match the selected type
            AvailableMachines = AvailableMachines.Where(m => Type == null || m.Type == Type);

            await this.InvokeAsync(this.StateHasChanged);
        }
    }

}
