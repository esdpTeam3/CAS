using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class Message
    {
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}
