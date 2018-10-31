using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models.ViewModels
{
    public class PermissionsGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PermissionId { get; set; }
        public Permissions Permissions { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }


    }
}
