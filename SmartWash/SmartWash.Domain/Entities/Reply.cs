using Microsoft.AspNetCore.Identity;

namespace SmartWash.Domain.Entities
{
    public class Reply
    {
        public int ID { get; set; }
        public int FeedbackId { get; set; }
        public string AdminId { get; set; }
        public Admin Admin { get; set; }
        public string ?Content { get; set; }
        public DateTime ReplyDateTime { get; set; }
    }
}
