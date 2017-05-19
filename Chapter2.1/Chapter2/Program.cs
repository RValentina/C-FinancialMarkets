using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOptionPricing
{
    class Program
    {
        static void Main(string[] args)
        {
            Mediator med = new Mediator();
            med.calculate();

            double nan = 0.0f / 0.0f;
            try
            {
                double d = SpecialFunctions.N(nan);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }



    }
}
