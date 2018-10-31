using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<UnionUserGroup> Groups { get; set; }
    }
}
