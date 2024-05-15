using Microsoft.AspNetCore.Identity;

namespace SmartWash.Domain.Entities
{
    public class ApplicationUser : Account
    {
        public DateTime RegistrationDate { get; set; }
        public IList<Booking> ?Bookings { get; set; }
        public int PointNum { get; set; }
        public bool IsGuest { get; set; }
        public IList<Reply>? Replies { get; set; }
    }
}
