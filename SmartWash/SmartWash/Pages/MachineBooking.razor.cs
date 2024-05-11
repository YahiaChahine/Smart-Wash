using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using SmartWash.Domain;
using SmartWash.Domain.Entities;
using SmartWash.Domain.Interfaces;
using SmartWash.WebUI.Shared;

namespace SmartWash.WebUI.Pages
{
    public partial class MachineBooking : ComponentBase
    {
        [Parameter] public int ObjectHash { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private StateContainer StateContainer { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private ICreditCardRepository CreditCardRepository { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }

        private Booking Booking { get; set; }
        private bool IsPaymentMethodSet { get; set; }
        private bool _coinPayment;
        private float _amount;
        private string _promotionCode;

        protected override async Task OnInitializedAsync()
        {
            Booking = (Booking)StateContainer.ObjectTunnel[ObjectHash];

            _amount = Booking.Machine.Type == MachineType.WashingMachine ? Constants.WashingMachinePrice : Constants.DryingMachinePrice;
            _amount *= Booking.CycleNum;

            var authState = await AuthStateTask;

            if (authState.User.Identity.IsAuthenticated)
            {
                CreditCardRepository.GetByUserAsync((int)authState.User.Identity.Name);
            }
            //IsPaymentMethodSet = !string.IsNullOrEmpty(Booking.User.StripeCustomerId);
        }

        private void GoBack()
        {
            NavigationManager.NavigateTo("/browse");
        }

        private async Task AddPaymentMethod()
        {
            var result = await DialogService.Show<PaymentMethodDialogComponent>("Add Payment Method", new DialogOptions { MaxWidth = MaxWidth.Small }).Result;

            if (!result.Cancelled)
            {
                //Booking.User.StripeCustomerId = result.Data.ToString();
                //await StateContainer.UpdateUser(Booking.User);
                IsPaymentMethodSet = true;
            }
        }

        private void Book()
        {

        }

        private async Task<IEnumerable<string>> GetPromotionCodes(string arg)
        {
            await Task.Delay(1000);


            //return states.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));    

            return new List<string> { "PROMO1", "PROMO2" } as IEnumerable<string>;
        }
    }
}
