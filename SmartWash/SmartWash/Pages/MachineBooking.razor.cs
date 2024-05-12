using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using SmartWash.Application.BookingSystem;
using SmartWash.Application.UserSystem;
using SmartWash.Domain.Entities;
using SmartWash.Domain.Interfaces;
using SmartWash.WebUI.Account;
using SmartWash.WebUI.Shared;
using Constants = SmartWash.Domain.Constants;

namespace SmartWash.WebUI.Pages
{
    public partial class MachineBooking : ComponentBase
    {
        [Parameter] public int ObjectHash { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private ICreditCardRepository CreditCardRepository { get; set; }
        [Inject] private IBookingService BookingService { get; set; }
        [Inject] private IUserService UserService { get; set; }

        [SupplyParameterFromQuery(Name = "machineId")] private int? MachineId { get; set; }
        [SupplyParameterFromQuery(Name = "machineType")] private string? Type { get; set; }
        [SupplyParameterFromQuery(Name = "startTime")] private DateTime? StartTime { get; set; }
        [SupplyParameterFromQuery(Name = "cycleNum")] private int? CycleNum { get; set; }

        private Booking Booking { get; set; }
        private bool IsPaymentMethodSet { get; set; }
        private bool _coinPayment;
        private float _amount;
        private string _promotionCode;

        private CreditCard? CreditCard { get; set; }
        private ApplicationUser? User { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //Booking = (Booking)StateContainer.ObjectTunnel[ObjectHash];

            //var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            //User = await UserManager.GetUserAsync(authState.User);
            
            _amount = Type == MachineType.WashingMachine.ToString() ? Constants.WashingMachinePrice : Constants.DryingMachinePrice;
            _amount *= CycleNum.Value;

            User = await UserService.GetUser();

            if (User is not null)
            {
                CreditCard = (await CreditCardRepository.GetByUserAsync(User.Id)).FirstOrDefault();

                if (CreditCard != null)
                {
                    IsPaymentMethodSet = true;
                }
            }
            //IsPaymentMethodSet = !string.IsNullOrEmpty(Booking.User.StripeCustomerId);
        }

        private void GoBack()
        {
            NavigationManager.NavigateTo("/browse");
        }

        private async Task AddPaymentMethod()
        {
            var parameters = new DialogParameters
            {
                { "User", User },
            };

            var dialog = await DialogService.ShowAsync<PaymentMethodDialogComponent>("Add Payment Method", parameters, new DialogOptions { MaxWidth = MaxWidth.Medium });
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                CreditCard = (CreditCard)result.Data;
                IsPaymentMethodSet = true;
            }
        }

        private async Task RemovePaymentMethod()
        {
            await CreditCardRepository.DeleteAsync(CreditCard!.ID);

            CreditCard = null;
            IsPaymentMethodSet = false;
        }

        private async Task Book()
        {
            if (MachineId is null || StartTime is null || CycleNum is null)
            {
                NavigationManager.NavigateTo("/browse");
                throw new InvalidOperationException("MachineId, StartTime, CycleNum and User must be set");
            }

            Booking = new()
            {
                MachineId = MachineId.Value,
                StartTime = StartTime,
                CycleNum = CycleNum.Value,
                IsPaid = !_coinPayment,
                UserId = User?.Id,
                Amount = _amount,
            };

            var createdBooking = await BookingService.CreateBookingAsync(Booking);

            // Show the booking details dialog
            var parameters = new DialogParameters
            {
                { "Booking", createdBooking },
            };

            var dialog = await DialogService.ShowAsync<BookingDetailsDialogComponent>("Booking Details", parameters, new DialogOptions { MaxWidth = MaxWidth.Small });
            await dialog.Result;

            NavigationManager.NavigateTo("/");
        }

        private async Task<IEnumerable<string>> GetPromotionCodes(string arg)
        {
            await Task.Delay(1000);


            //return states.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));    

            return new List<string> { "PROMO1", "PROMO2" } as IEnumerable<string>;
        }
    }
}
