using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;
using SmartWash.Domain.Interfaces;
using Stripe;

namespace SmartWash.Application.PaymentSystem
{
	public class PaymentService : IPaymentService
	{
		private readonly IStripeAdapter _stripeAdapter;

		public PaymentService(IStripeAdapter paymentService)
		{
			_stripeAdapter = paymentService;
		}

		public async Task<Charge> MakePaymentAsync(CreditCard creditCard, decimal amount, string currency)
		{
			var token = await _stripeAdapter.CreateCardTokenAsync(creditCard.CardNumber, creditCard.ExpirationMonth,
				creditCard.ExpirationYear, creditCard.CVV);

			if (token == null)
			{
				throw new Exception("Failed to create card token");
			}

			var createdPayment = await _stripeAdapter.CreateChargeAsync(amount, currency, token);
			return createdPayment;
		}
	}
}
