using Microsoft.AspNetCore.Identity;

namespace SmartWash.Domain.Entities
{
    public class Offer
    {
        public int ID { get; set; }
        public string ?Description { get; set; }
        public float Percentage { get; set; }
    }
}
