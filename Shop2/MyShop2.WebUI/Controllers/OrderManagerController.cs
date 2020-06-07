using MyShop2.Core.Contracts;
using MyShop2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop2.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderManagerController : Controller
    {
        IRepository<Customer> customerContext;
        IOrderService orderService;

        public OrderManagerController(IRepository<Customer> customerContext, IOrderService orderService)
        {
            this.customerContext = customerContext;
            this.orderService = orderService;
        }

        public ActionResult Index()
        {
            List<Order> orders = orderService.GetOrderList();
            return View(orders);
        }

        public ActionResult Edit(string Id)
        {
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "OrderShipped",
                "Order Completed"
            };

            if (string.IsNullOrEmpty(Id))
            {
                return RedirectToAction("Index");
            }

            Order order = orderService.GetOrder(Id);
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(string Id, Order order)
        {
            Order orderToUpdate = orderService.GetOrder(Id);
            orderToUpdate.status = order.status;
            orderService.updateOrder(orderToUpdate);
            return RedirectToAction("Index");
        }

    }
}