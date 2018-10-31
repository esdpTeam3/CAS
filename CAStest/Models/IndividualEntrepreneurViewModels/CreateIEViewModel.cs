using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models.IndividualEntrepreneurViewModels
{
    public class CreateIEViewModel
    {
       
        public SelectList Countries { get; set; }
        public string Country { get; set; }


        [Required]
        public string Fullname { get; set; }
        public int Inn { get; set; }
        [Display(Name = "DateOfBirth")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string PassportData { get; set; }
        public string Address { get; set; }
        public string LegalAddress { get; set; }
    }
}
