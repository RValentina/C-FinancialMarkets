using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOptionPricing
{
    public struct Mediator
    {
        static IOptionFactory getFactory()
        {
            return new ConsoleEuropeanOptionFactory();
        }

        public void calculate()
        {
            IOptionFactory fac = getFactory();

            Option myOption = fac.create();

            

            //Console.Write("Give the underlying price: ");
            //double S = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Price: {0}", myOption.Price());

        }
    }
}
