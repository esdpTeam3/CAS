using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Login { get; set; }
        public string FullName { get; set; }
        public string Id { get; set; }

        //public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "DatePassword")]
        public DateTime DatePassword { get; set; }

        [Required]
        [Display(Name = "Введите число")]
        public int PasswordNotifications { get; set; }

        [Required]
        [Display(Name = "Введите число")]
        public int ContractNotifications { get; set; }

        public string StatusMessage { get; set; }
    }
}
