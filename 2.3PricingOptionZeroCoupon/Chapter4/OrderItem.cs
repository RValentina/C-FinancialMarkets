using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    public class OrderItem
    {
        public string Name { get; set; }
        public Order Order { get; set; }

        public OrderItem()
        {
            Order = null;
        }

        public OrderItem(string name)
        {
            Name = name;
            Order = null;
        }

        public OrderItem(OrderItem orderItem)
        {
            Name = orderItem.Name;
            Order = orderItem.Order;
        }
    }
}
