using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class Company : Counterparty
    {
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public int CompanyINN { get; set; }
        public string Country { get; set; }
        public string IndividualAddress { get; set; } // физ. адрес
        public string LegalAddress { get; set; } // юр. адрес
        [EmailAddress(ErrorMessage = "Неверный адрес электронной почты")]
        public string MailAddress { get; set; } // почтовый адрес
        public string BIC { get; set; } // банковский идентефикационный код
        public string CheckingAccount { get; set; } // расчетный счет
        public List<Individual> Individuals { get; set; }

    }
}
