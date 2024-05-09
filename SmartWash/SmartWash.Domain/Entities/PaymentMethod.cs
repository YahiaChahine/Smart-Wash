using Microsoft.AspNetCore.Identity;

namespace SmartWash.Domain.Entities
{
    public class PaymentMethod
    {
        public PaymentMethodType Type { get; set; }
    }

    public enum PaymentMethodType
    {
        Coin = 0,
        Card = 1
    }
}
