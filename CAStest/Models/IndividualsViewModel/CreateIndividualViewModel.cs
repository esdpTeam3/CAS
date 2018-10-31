using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CAStest.Models.IndividualsViewModel
{
    public class CreateIndividualViewModel
    {
        public int? CompanyId { get; set; }
        public SelectList Companies { get; set; }
        public SelectList Countries { get; set; }
        public string Country { get; set; }


        [Required]
        public string Fullname { get; set; }
        [Remote(action: "CheckINN", controller: "Individuals", AdditionalFields = "Inn", ErrorMessage = "Физ.Лицо с таким ИНН уже создан")]
        public int Inn { get; set; }
        [Required]
        [Display(Name = "DateOfBirth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string PassportData { get; set; }
        public string Address { get; set; }
        public string LegalAddress { get; set; }
        [Required]
        public string Position { get; set; }

        
    }
}
