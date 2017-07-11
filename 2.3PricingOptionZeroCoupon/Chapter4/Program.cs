using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
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

                order1.ItemAdded += PrintItems;

                order2.ItemAdded += PrintItems;

                order1.AddItem(new OrderItem("Computer"));
                order1.AddItem(new OrderItem("Keyboard"));
                order1.Print();
                order1.Price = 25;
                order2.Price = 30;
                order2.AddItem(new OrderItem("Car"));

                var sumOrder = order1 + order2;
                sumOrder.Print();

                Console.WriteLine("The order items of the sumOrder are: ");

                for (int i = 0; i < sumOrder.NoItems; i++)
                {
                    Console.WriteLine(sumOrder[i].Name);
                }
            }
            catch (NoNameException e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.ReadLine();

        }


        public static void PrintItems(Order o)
        {
            Console.WriteLine("The order items are: ");

            for (int i = 0; i < o.NoItems; i++)
            {
                Console.WriteLine(o[i].Name);
            }
        }
    }
}
