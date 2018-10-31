using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Fullname { get; set; }
        public DateTime DatePassword { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
