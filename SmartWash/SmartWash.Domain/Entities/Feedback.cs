using Microsoft.AspNetCore.Identity;

namespace SmartWash.Domain.Entities
{
    public class Feedback
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string ?Title { get; set; }
        public string ?Content { get; set; }
        public DateTime FeedbackDateTime { get; set; }
    }
}
