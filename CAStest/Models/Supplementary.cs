using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class Supplementary
    {
        public int Id { get; set; }
        public string SupplementaryNumber { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }
        public Contract Contract { get; set; }

        public List<SupplementaryFile> SupplementaryFiles { get; set; }
    }
}
