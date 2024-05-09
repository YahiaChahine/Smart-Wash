using Microsoft.AspNetCore.Identity;

namespace SmartWash.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public ICollection<Booking> ?Bookings { get; set; }
        public int PointNum { get; set; }
    }
}
