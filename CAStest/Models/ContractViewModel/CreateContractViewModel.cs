using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CAStest.Models.ContractViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CAStest.Models.AgrementViewModel
{
    public class CreateContractViewModel
    {
        public int Id { get; set; }
        [Required]
        public string ContractNumber { get; set; }


        public SelectList Countries { get; set; }
        public string Country { get; set; }

        [Display(Name = "DateOfOffer")]
        [DataType(DataType.Date)]
        public DateTime DateOfOffer { get; set; }

        [Display(Name = "ContractTime")]
        [DataType(DataType.Date)]
        public DateTime ContractTime { get; set; }

        public List<int> propertyId { get; set; }
        public List<string> valueProperty { get; set; }

        public bool CheckAutorolongation { get; set; }

        public SelectList Properties { get; set; }

        public List<UsersCheckList> IndividualCheckLists { get; set; }

        public List<Individual> Individuals { get; set; }
        public List<IndividualEntrepreneur> IndividualEntrepreneurs { get; set; }
        public List<Company> Companies { get; set; }
        public List<int> CounterpartiesId { get; set; }
        
        public List<string> actNumbers { get; set; }
        public List<string> supplementaryNumbers { get; set; }

        public List<IFormFile> contractFiles { get; set; }
        public List<ActSuppFile> actFiles { get; set; }
        public List<ActSuppFile> suppFiles { get; set; }
    }
}
