using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;
using Stripe;

namespace SmartWash.Application.PaymentSystem
{
    public class DemoPaymentService : IPaymentService
    {
        public async Task<Charge> MakePaymentAsync(CreditCard creditCard, decimal amount, string currency)
        {
            await Task.Delay(2000);

            return new Charge
            {
                Amount = (long)(amount * 100),
                Currency = currency,
                Created = DateTime.UtcNow,
                Id = Guid.NewGuid().ToString(),
                Paid = true,
                Status = "succeeded"
            };
        }
    }
}
