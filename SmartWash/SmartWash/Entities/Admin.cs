using Microsoft.AspNetCore.Identity;

namespace SmartWashProject.Entities
{
    public class Admin : IdentityUser
    {
        public ICollection<Reply> Replies { get; set; }
    }
}
