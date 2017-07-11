using System;
using System.Collections;
using System.Data;
using System.Globalization;

namespace Chapter4
{
    public class Order : IPricing
    {
        public delegate void OrderDelegate(Order order);

        public event OrderDelegate ItemAdded;

        private static int _number;

        private double _price;

        private ArrayList _items;

        static Order()
        {
            _number = 1;
        }

        public Order() 
        {
            RegistrationDate = DateTime.Today;

            Name = "order#" + _number;

            _items = new ArrayList();

            _number++;
        }

        public Order(Order order)
        {
            Name = order.Name;
            RegistrationDate = order.RegistrationDate;
            _items = new ArrayList();

            foreach (OrderItem item in order.Items)
            {
                AddItem(item);
            }
        }

        public Order(string name, DateTime registrationDate) 
        {
            Name = name;
            RegistrationDate = registrationDate;
            _items = new ArrayList();
        }

        public string Name { get; set; }

        public DateTime RegistrationDate { get; set; }

        public double Price 
        {
            get => _price - Discount;
            set => _price = value;
        }

        public virtual double Discount 
        {
            get => 0;
            set { }
        }

        public IEnumerable Items
        {
            get => _items;
        }

        public int NoItems => _items.Count;

        public static int Number => _number;

        public void AddItem(OrderItem orderItem)
        {
            if (String.IsNullOrEmpty(orderItem.Name))
            {
                throw new NoNameException("The name of the order item is empty!");
            }
            _items.Add(orderItem);
            orderItem.Order = this;

            ItemAdded(this);
        }

        public virtual void Print()
        {
            Console.WriteLine(Name);
            Console.WriteLine(RegistrationDate.ToString(CultureInfo.InvariantCulture));

            if (Items != null)
            {
                foreach (OrderItem item in Items)
                {
                    Console.WriteLine("Item name: {0}; Part of order: {1}", item.Name, item.Order.Name);
                }
            }
        }

        public static Order operator +(Order order1, Order order2)
        {
            Order newOrder = new Order
            {
                Name = order1.Name + order2.Name,
                Price = order1.Price + order2.Price
            };

            if (order1.ItemAdded != null)
            {
                newOrder.ItemAdded += order1.ItemAdded;
            }
            else if (order2.ItemAdded != null)
            {
                newOrder.ItemAdded += order2.ItemAdded;
            }

            foreach (OrderItem item in order1.Items)
            {
                newOrder.AddItem(item);
            }

            foreach (OrderItem item in order2.Items)
            {
                newOrder.AddItem(item);
            }

            return newOrder;
        }

        public OrderItem this[int key]
        {
            get
            {
                if (Items != null)
                {
                    return (OrderItem) _items[key];
                }
                return null;
            }
        }

    }
}
