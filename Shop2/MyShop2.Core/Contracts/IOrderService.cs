using System.Collections.Generic;
using MyShop2.Core.Models;
using MyShop2.Core.ViewModels;

namespace MyShop2.Core.Contracts
{
    public interface IOrderService
    {
        void CreateOrder(Order order, List<BasketItemViewModel> basketItems);
        List<Order> GetOrderList();
        Order GetOrder(string Id);
        void updateOrder(Order order);
    }
}