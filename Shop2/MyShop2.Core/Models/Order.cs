using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop2.Core.Models
{
    public class Order : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string status { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }

    }
}
