using MyShop2.Core.Contracts;
using MyShop2.Core.Models;
using MyShop2.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop2.Service
{
    public class OrderService : IOrderService
    {
        IRepository<Order> OrderContext;
        public OrderService(IRepository<Order> OrderContext)
        {
            this.OrderContext = OrderContext;
        }

        public void CreateOrder(Order order, List<BasketItemViewModel> basketItems)
        {


            foreach (var item in basketItems)
            {
                order.OrderItems.Add(new OrderItem()
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Image = item.Image,
                    Price = item.Price,
                    Quantity = item.Quantity
                }
                );
            }

            OrderContext.Insert(order);
            OrderContext.Commit();
        }

        public List<Order> GetOrderList()
        {
            return OrderContext.Collection().ToList();
        }

        public Order GetOrder(string Id)
        {
            return OrderContext.Find(Id);
        }

        public void updateOrder(Order order)
        {
            OrderContext.Update(order);
            OrderContext.Commit();
        }
    }
}
