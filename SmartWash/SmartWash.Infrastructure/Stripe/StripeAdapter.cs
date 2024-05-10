using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Interfaces;
using Stripe;

namespace SmartWash.Infrastructure.Stripe
{
	public class StripeAdapter : IStripeAdapter
	{
		private readonly StripeClient _stripeClient;

		public StripeAdapter(string apiKey)
		{
			_stripeClient = new StripeClient(apiKey);
		}

		public async Task<Charge> CreateChargeAsync(decimal amount, string currency, string cardToken)
		{
			var options = new ChargeCreateOptions
			{
				Amount = (long)(amount * 100), // Stripe requires amount in cents
				Currency = currency,
				Source = cardToken
			};

			var service = new ChargeService(_stripeClient);
			return await service.CreateAsync(options);
		}

		public async Task<string> CreateCardTokenAsync(string cardNumber, int expMonth, int expYear, string cvc)
		{
			var tokenOptions = new TokenCreateOptions
			{
				Card = new TokenCardOptions
				{
					Number = cardNumber,
					ExpMonth = expMonth.ToString(),
					ExpYear = expYear.ToString(),
					Cvc = cvc
				}
			};

			var tokenService = new TokenService(_stripeClient);
			var token = await tokenService.CreateAsync(tokenOptions);

			return token.Id;
		}

	}
}
