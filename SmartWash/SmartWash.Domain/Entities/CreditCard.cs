using Microsoft.AspNetCore.Identity;

namespace SmartWash.Domain.Entities
{
    public class CreditCard
    {
        public int ID { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
        public string CardHolderName { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
