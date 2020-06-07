using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop2.Core.Models
{
    public class Product : BaseEntity
    {
        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.00, 9999.99)]
        public decimal Price { get; set; }

        public string ProductCategory { get; set; }

        public string Image { get; set; }

    }
}
