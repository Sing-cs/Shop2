using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop2.Core.Contracts;
using MyShop2.Core.Models;
using MyShop2.Core.ViewModels;
using MyShop2.WebUI;
using MyShop2.WebUI.Controllers;

namespace MyShop2.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageCanReturnProduct()
        {
            // Arrange
            IRepository<Product> ProductContext = new Mocks.MockContext<Product>();
            IRepository<ProductCategory> ProductCategoryContext = new Mocks.MockContext<ProductCategory>();
            HomeController controller = new HomeController(ProductContext, ProductCategoryContext);
            ProductContext.Insert(new Product());

            // Act
            ViewResult result = controller.Index() as ViewResult;
            HomeIndexViewModel viewModel = result.ViewData.Model as HomeIndexViewModel;

            // Assert
            Assert.AreEqual(1, viewModel.Products.Count());
        }

        

    }
}
