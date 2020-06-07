using MyShop2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop2.Core.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }

        public HomeIndexViewModel()
        {
        }

        public HomeIndexViewModel(IEnumerable<Product> products, IEnumerable<ProductCategory> productCategories)
        {
            this.Products = products;
            this.ProductCategories = productCategories;
        }
    }



}
