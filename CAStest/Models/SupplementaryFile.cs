        using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class SupplementaryFile : CASFile
    {
        public int SupplementaryId { get; set; }
        public Supplementary Supplementary { get; set; }
    }
}
