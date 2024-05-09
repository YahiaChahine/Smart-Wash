using Microsoft.AspNetCore.Identity;

namespace SmartWashProject.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public ICollection<Booking> ?Bookings { get; set; }
        public int PointNum { get; set; }
    }
}
