using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CAStest.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool IsBlocked { get; set; }
        public string Fullname { get; set; }
        public DateTime DatePassword { get; set; }
        [ForeignKey("Individual")]
        public int? IndividualId { get; set; }
        public Individual Individual { get; set; }
        public int PasswordNotifications { get; set; }
        public int ContractNotifications { get; set; }
        public virtual ICollection<UnionUserGroup> ApplicationUsers { get; set; }
        public List<Favorites> Favorites { get; set; }
        public List<Notification> Notifications { get; set; }



    }
}
