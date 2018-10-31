using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class CounterpartyContracts
    {
        public int Id { get; set; }

        [ForeignKey("Counterparty")]
        public int ConterpartyId { get; set; }
        public Counterparty Counterparty { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }
        public Contract Contract { get; set; }

    }
}
