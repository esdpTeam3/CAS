using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models.IndividualEntrepreneurViewModels
{
    public class DetailsViewModel
    {
        public IndividualEntrepreneur IndividualEntrepreneur { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
