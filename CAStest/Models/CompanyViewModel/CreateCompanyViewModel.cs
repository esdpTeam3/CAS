using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CAStest.Models.CompanyViewModel
{
    public class CreateCompanyViewModel
    {
        [Required]
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        [Remote(action: "CheckINN", controller: "Company", AdditionalFields = "CompanyINN", ErrorMessage = "Компания с таким ИНН уже создан")]
        public int CompanyINN { get; set; }
        [Required]
        public string Country { get; set; }
        public SelectList Countries { get; set; }
        [Required]
        public string IndividualAddress { get; set; } // физ. адрес
        [Required]
        public string LegalAddress { get; set; } // юр. адрес
        [EmailAddress(ErrorMessage = "Неверный адрес электронной почты")]
        public string MailAddress { get; set; } // почтовый адрес
        public string BIC { get; set; } // банковский идентефикационный код
        public string CheckingAccount { get; set; } // расчетный счет

        public SelectList Individuals { get; set; }

        //public List<Individual> Individuals { get; set; }
    }
}
