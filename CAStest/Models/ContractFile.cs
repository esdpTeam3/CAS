using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class ContractFile : CASFile
    {
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}
