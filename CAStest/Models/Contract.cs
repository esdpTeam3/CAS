using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
        public string ContractNumber { get; set; }
        public DateTime DateOfOffer { get; set; }
        public DateTime ContractTime { get; set; }
        public string Country { get; set; }
        public bool Autorolongation { get; set; }

        public List<ContractProperties> ContractProperties { get; set; }
        public List<Act> Acts { get; set; }
        public List<Supplementary> Supplementaries { get; set; }
        public List<ContractFile> ContractFiles { get; set; }
    }
}
