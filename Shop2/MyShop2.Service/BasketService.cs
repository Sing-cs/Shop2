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
    public class BasketService : IBasketService
    {
        IRepository<Basket> basketContext;
        IRepository<Product> productContext;

        private const string sessionName = "eCommerceBasket2";

        public BasketService(IRepository<Basket> BasketContext, IRepository<Product> ProductContext)
        {
            this.basketContext = BasketContext;
            this.productContext = ProductContext;
        }

        public void AddToBasket(HttpContextBase httpContext, string ProductId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == ProductId);

            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = ProductId,
                    Quantity = 1
                };

                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + 1;
            }

            basketContext.Commit();

        }

        public void RemoveFromBasket(HttpContextBase httpContext, string ItemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == ItemId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }

        }

        public void ClearBasket(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            basket.BasketItems.Clear();
            basketContext.Commit();
        }

        private Basket GetBasket(HttpContextBase httpContext, bool CreateIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(sessionName);
            Basket basket = new Basket();


            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if (CreateIfNull)
                    {
                        basket = CreateBasket(httpContext);
                    }
                }
            }
            else
            {
                if (CreateIfNull)
                {
                    basket = CreateBasket(httpContext);
                }
            }

            return basket;
        }

        private Basket CreateBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();

            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(sessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            BasketSummaryViewModel viewModel = new BasketSummaryViewModel(0, 0);

            Basket basket = GetBasket(httpContext, false);
            if (basket == null)
            {
                return viewModel;
            }

            int? basketCount = (from item in basket.BasketItems
                                select item.Quantity).Sum();

            decimal? basketTotal = (from item in basket.BasketItems
                                    join product in productContext.Collection()
                                    on item.ProductId equals product.Id
                                    select product.Price * item.Quantity).Sum();

            viewModel.BasketCount = basketCount ?? 0;
            viewModel.BasketTotal = basketTotal ?? decimal.Zero;

            return viewModel;
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);

            if (basket == null)
            {
                return new List<BasketItemViewModel>();
            }

            var result = (from item in basket.BasketItems
                          join product in productContext.Collection()
                          on item.ProductId equals product.Id
                          select new BasketItemViewModel()
                          {
                              Id = item.Id,
                              ProductId = product.Id,
                              ProductName = product.Name,
                              Price = product.Price,
                              Image = product.Image,
                              Quantity = item.Quantity
                          }).ToList();

            return result;
        }

    }
}
