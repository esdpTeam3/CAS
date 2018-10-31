using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CAStest.Models.GroupUserViewModel
{
    public class CreateUserGroupViewModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int UserId { get; set; }
        public ApplicationUser Users { get; set; }
        public List<SelectListItem> UserSelectList { get; set; }


    }
}
