using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class Favorites
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}
