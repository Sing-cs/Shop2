using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop2.Core.Models
{
    public class ProductCategory : BaseEntity
    {
        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }
    }
}
