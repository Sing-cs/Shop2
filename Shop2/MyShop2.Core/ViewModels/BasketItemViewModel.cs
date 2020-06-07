using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop2.Core.ViewModels
{
    public class BasketItemViewModel
    {
        public string Id { get; set; } //itemId
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
