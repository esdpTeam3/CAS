using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public class CategoryProperties
    {
        public int Id { get; set; }
        [ForeignKey("Property")]

        public int PropertyId { get; set; }
        [ForeignKey("Category")]

        public int CategoryId { get; set; }
    }
}
