using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models.ContractViewModel
{
    public class DetailsContractViewModel
    {
        public Contract Contract { get; set; }
        public List<Counterparty> Counterparties { get; set; }
    }
}
