using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAStest.Models.ViewModels;

namespace CAStest.Models
{
    public class ConnectionUserGroup
    {
        public int Id { get; set; }
        public int PermissionGroupId { get; set; }
        public PermissionsGroup PermissionsGroup { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
