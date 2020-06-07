using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop2.Core.Contracts;
using MyShop2.Core.Models;
using MyShop2.Core.ViewModels;
using MyShop2.Service;
using MyShop2.WebUI.Controllers;
using MyShop2.WebUI.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyShop2.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketServiceTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            //Arrange
            IRepository<Basket> basketContext = new MockContext<Basket>();
            IRepository<Product> productContext = new MockContext<Product>();
            IRepository<Order> orderContext = new MockContext<Order>();
            IRepository<Customer> customerContext = new MockContext<Customer>();
            HttpContextBase httpContext = new MockHttpContext();
            
            IBasketService basketService = new BasketService(basketContext, productContext);
            IOrderService orderService = new OrderService(orderContext);

            BasketController controller = new BasketController(basketService, orderService, customerContext);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            basketService.AddToBasket(httpContext, "1");
            Basket basket = basketContext.Collection().FirstOrDefault();

            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count());
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);
        }


        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Product> ProductContext = new Mocks.MockContext<Product>();
            IRepository<Basket> BasketContext = new Mocks.MockContext<Basket>();
            IRepository<Order> orderContext = new MockContext<Order>();
            IRepository<Customer> customerContext = new MockContext<Customer>();
            IBasketService basketService = new BasketService(BasketContext, ProductContext);
            HttpContextBase httpContext = new MockHttpContext();
            IOrderService orderService = new OrderService(orderContext);
            Basket basket = new Basket();
            BasketController controller = new BasketController(basketService, orderService, customerContext);

            ProductContext.Insert(new Product { Id = "1", Price = 3.00m });
            ProductContext.Insert(new Product { Id = "2", Price = 2.00m });
            basket.BasketItems.Add(new BasketItem { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem { ProductId = "2", Quantity = 2 });
            BasketContext.Insert(basket);
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket2") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            PartialViewResult result = controller.BasketSummary() as PartialViewResult;
            BasketSummaryViewModel viewModel = result.ViewData.Model as BasketSummaryViewModel;

            Assert.IsNotNull(viewModel);
            Assert.AreEqual(4, viewModel.BasketCount);
            Assert.AreEqual(10.00m, viewModel.BasketTotal);


        }

        [TestMethod]
        public void CanCheckOutAndCreateOrder()
        {
            IRepository<Product> ProductContext = new Mocks.MockContext<Product>();
            IRepository<Basket> BasketContext = new Mocks.MockContext<Basket>();
            IRepository<Order> orderContext = new MockContext<Order>();
            IRepository<Customer> customerContext = new MockContext<Customer>();
            IBasketService basketService = new BasketService(BasketContext, ProductContext);
            HttpContextBase httpContext = new MockHttpContext();
            IOrderService orderService = new OrderService(orderContext);
            Basket basket = new Basket();
            BasketController controller = new BasketController(basketService, orderService, customerContext);

            customerContext.Insert(new Customer { Id = "1", Email = "test@test.com" });
            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("test@test.com", "Forms"), null);

            ProductContext.Insert(new Product { Id = "1", Price = 3.00m });
            ProductContext.Insert(new Product { Id = "2", Price = 2.00m });
            basket.BasketItems.Add(new BasketItem { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem { ProductId = "2", Quantity = 2 });
            BasketContext.Insert(basket);
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket2") { Value = basket.Id });
            httpContext.User = fakeUser;

            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            Order order = new Order();
            controller.CheckOut(order);
            Order OrderInRepo = orderContext.Find(order.Id);

            Assert.AreEqual(2, order.OrderItems.Count);
            Assert.AreEqual(0, basket.BasketItems.Count);
            Assert.AreEqual(2, OrderInRepo.OrderItems.Count);
        }

    }

}
