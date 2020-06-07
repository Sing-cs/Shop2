using MyShop2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop2.Core.ViewModels
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }

        public ProductEditViewModel()
        {

        }

        public ProductEditViewModel(Product product, IEnumerable<ProductCategory> productCategories)
        {
            this.Product = product;
            this.ProductCategories = productCategories;
        }
    }
}
