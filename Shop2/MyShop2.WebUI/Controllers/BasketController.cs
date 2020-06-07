using MyShop2.Core.Contracts;
using MyShop2.Core.Models;
using MyShop2.Core.ViewModels;
using MyShop2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop2.WebUI.Controllers
{
    public class BasketController : Controller
    {

        IBasketService basketService;
        IOrderService orderService;
        IRepository<Customer> customerContext;

        public BasketController(IBasketService basketService, IOrderService orderService, IRepository<Customer> customerContext)
        {
            this.basketService = basketService;
            this.orderService = orderService;
            this.customerContext = customerContext;
    }


        // GET: Basket
        public ActionResult Index()
        {

            var model = basketService.GetBasketItems(this.HttpContext);

            return View(model);
        }

        public ActionResult AddToBasket(string ProductId)
        {
            basketService.AddToBasket(this.HttpContext, ProductId);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string ItemId)
        {
            basketService.RemoveFromBasket(this.HttpContext, ItemId);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var model = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(model);
        }

        [Authorize]
        public ActionResult CheckOut()
        {
            Customer customer = customerContext.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer == null)
            {
                return RedirectToAction("Error");
            }

            Order order = new Order()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address,
            };

            return View(order);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CheckOut(Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }
            
            List<BasketItemViewModel> basketItems = basketService.GetBasketItems(this.HttpContext);
            order.status = "Order Created";
            order.Email = User.Identity.Name;
            //Payment success
            order.status = "Payment Processed";
            orderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);
            return RedirectToAction("ThankYou", new { OrderId = order.Id });

        }

        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }


    }
}
