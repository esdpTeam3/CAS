using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models.ContractViewModel
{
    public class ActSuppFile
    {
        public string Number { get; set; }
        public IFormFile File { get; set; }
    }
}
