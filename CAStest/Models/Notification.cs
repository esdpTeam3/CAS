using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateOfCreate { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
