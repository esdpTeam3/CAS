using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class Property
    {
        public int Id { get; set; }

        [ForeignKey("PropertyType")]
        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }

        public string Name { get; set; }
    }
}
