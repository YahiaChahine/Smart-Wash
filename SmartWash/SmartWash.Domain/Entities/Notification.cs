using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWash.Domain.Entities
{
    public class Notification
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

    }
}
