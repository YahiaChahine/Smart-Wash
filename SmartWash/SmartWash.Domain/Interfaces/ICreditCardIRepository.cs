using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;

namespace SmartWash.Domain.Interfaces
{
	public interface ICreditCardRepository
	{
		Task<CreditCard> GetByIdAsync(int id);
		Task<CreditCard> AddAsync(CreditCard creditcard);
		Task UpdateAsync(CreditCard creditcard);
		Task DeleteAsync(int id);
		Task<IEnumerable<CreditCard>> GetByUserAsync(string userID);
		Task<IEnumerable<CreditCard>> GetAllAsync();
	}
}
