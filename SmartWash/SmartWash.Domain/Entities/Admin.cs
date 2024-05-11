using Microsoft.AspNetCore.Identity;

namespace SmartWash.Domain.Entities
{
    public class Admin : Account
    {
        public ICollection<Reply> Replies { get; set; }
        public ICollection<string> Notifications { get; set; }
    }
}
