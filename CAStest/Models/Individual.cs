using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CAStest.Models
{
    public class Individual: Counterparty
    {
        
        public string Fullname { get; set; }
        public int Inn { get; set; }
        [Display(Name = "DateOfBirth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string PassportData { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string LegalAddress { get; set; }
        public string Position { get; set; }
        
        [ForeignKey("Company")]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
