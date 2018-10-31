using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CAStest.Models.GroupUserViewModel
{
    public class IndexUserGroupViewModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public MultiSelectList Users { get; set; }
        public List<string> UserId { get; set; }
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }
    }
}
