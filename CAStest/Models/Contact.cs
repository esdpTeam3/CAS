using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [ForeignKey("Counterparty")]
        public int CounterpartyId { get; set; }
        [ForeignKey("ContactType")]
        public int ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
    }
}
