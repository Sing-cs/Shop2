using MyShop2.Core.Contracts;
using MyShop2.Core.Models;
using MyShop2.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> ProductContext;
        IRepository<ProductCategory> ProductCategoryContext;
        
        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            this.ProductContext = productContext;
            this.ProductCategoryContext = productCategoryContext;
        }

        public ActionResult Index(string Category = null)
        {
            IEnumerable<Product> products;

            if (Category == null)
            {
                products = ProductContext.Collection().ToList();
            }
            else
            {
                products = ProductContext.Collection().Where(p => p.ProductCategory == Category).ToList();
            }


            IEnumerable<ProductCategory> productCategories = ProductCategoryContext.Collection().ToList();
            HomeIndexViewModel viewModel = new HomeIndexViewModel(products, productCategories);

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}