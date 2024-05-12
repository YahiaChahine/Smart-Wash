using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWash.Domain.Entities
{
    public class UserInfo
    {
        public required string UserID { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
    }
}
