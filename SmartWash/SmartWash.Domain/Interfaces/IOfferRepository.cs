using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;

namespace SmartWash.Domain.Interfaces
{
	public interface IOfferRepository
	{
		Task<Offer> GetByIdAsync(int id);
		Task<Offer> AddAsync(Offer offer);
		Task UpdateAsync(Offer offer);
		Task DeleteAsync(int id);
		Task<IEnumerable<Offer>> GetAllAsync();
	}
}
