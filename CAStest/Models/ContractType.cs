using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CAStest.Models
{
    public class ContractType
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public int ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}
