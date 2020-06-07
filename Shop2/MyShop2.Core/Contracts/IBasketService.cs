using System.Collections.Generic;
using System.Web;
using MyShop2.Core.ViewModels;

namespace MyShop2.Core.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContext, string ProductId);
        void ClearBasket(HttpContextBase httpContext);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
        void RemoveFromBasket(HttpContextBase httpContext, string ItemId);
    }
}