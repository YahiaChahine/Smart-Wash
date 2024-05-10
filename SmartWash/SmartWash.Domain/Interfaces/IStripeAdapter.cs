using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWash.Domain.Interfaces
{
	public interface IStripeAdapter
	{
		Task<Charge> CreateChargeAsync(decimal amount, string currency, string cardToken);
		Task<string> CreateCardTokenAsync(string cardNumber, int expMonth, int expYear, string cvc);
	}
}
