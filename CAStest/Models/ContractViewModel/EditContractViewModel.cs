using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models.ContractViewModel
{
    public class EditContractViewModel
    {
        public Contract Contract { get; set; }

        public List<int> propertyId { get; set; }
        public List<string> valueProperty { get; set; }
        public SelectList Properties { get; set; }
        public List<Counterparty> Counterparties { get; set; }
        public List<int> CounterpartiesId { get; set; }
        public List<ContractProperties> ContractProperties { get; set; }

        public List<Individual> Individuals { get; set; }
        public List<IndividualEntrepreneur> IndividualEntrepreneurs { get; set; }
        public List<Company> Companies { get; set; }
    }
}
