using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class ActFile : CASFile
    {
        public int ActId { get; set; }
        public Act Act { get; set; }
    }
}
