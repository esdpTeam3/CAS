using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class Counterparty
    {
        [Key]
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
        public List<Contact> Contacts { get; set; }

    }
}
