using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class ContractCounterparty
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Contract")]
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        [ForeignKey("Counterparty")]
        public int CounterpartyId { get; set; }
        public Counterparty Counterparty { get; set; }
    }
}
