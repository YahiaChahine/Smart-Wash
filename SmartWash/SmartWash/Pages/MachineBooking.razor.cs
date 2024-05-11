using Microsoft.AspNetCore.Components;
using SmartWash.Domain.Entities;

namespace SmartWash.WebUI.Pages
{
    public partial class MachineBooking : ComponentBase
    {
        [Parameter]
        public int ObjectHash { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private StateContainer StateContainer { get; set; }

        private Booking Booking { get; set; }

        protected override void OnInitialized()
        {
            Booking = (Booking)StateContainer.ObjectTunnel[ObjectHash];
        }

        private void GoBack()
        {
            NavigationManager.NavigateTo("/browse");
        }

        private void Book()
        {

        }
    }
}
