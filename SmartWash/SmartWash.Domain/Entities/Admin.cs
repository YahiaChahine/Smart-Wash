using Microsoft.AspNetCore.Identity;

namespace SmartWash.Domain.Entities
{
    public class Admin : IdentityUser
    {
        public ICollection<Reply> Replies { get; set; }
    }
}
