using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order1 = new Order();
            Order order2 = new Order(order1);
            Order order3 = new Order();

            order1.Print();
            order2.Print();
            order3.Print();

            ExternalOrder extOrder = new ExternalOrder("ext", DateTime.Today, "TestCompany");

            extOrder.Print();

            ((Order)extOrder).Print();
            
            //Test discount behaviour Order class.
            IPricing orderPricing = new Order();

            orderPricing.Price = 200;
            orderPricing.Discount = 50;

            Console.WriteLine(orderPricing.Price);

            //Test discount behaviour ExternalOrder class.
            IPricing extOrderPricing = new ExternalOrder();
            extOrderPricing.Price = 200;
            extOrderPricing.Discount = 50;

            Console.WriteLine(extOrderPricing.Price);

            Console.ReadLine();

        }
    }
}
