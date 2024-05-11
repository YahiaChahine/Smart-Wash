using SmartWash.Domain.Entities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWash.Application.PaymentSystem
{
    public interface IPaymentService
    {
        Task<Charge> MakePaymentAsync(CreditCard creditCard, decimal amount, string currency);
    }
}
