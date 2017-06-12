using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOptionPricing
{
    public class ConsoleEuropeanOptionFactory : IOptionFactory
    {
        public Option create()
        {
            Console.Write("\n***; Data for option object ***\n");

            double r;       //Interest rate
            double sig;     //Volatility
            double K;       //Strike pricee
            double T;       //Expiry date
            double b;       //Cost of carry
            double s;       //Maturity

            string type;    //Option name (call, put)

            Console.Write("Strike: ");
            K = Convert.ToDouble(Console.ReadLine());

            Console.Write("Volatility: ");
            sig = Convert.ToDouble(Console.ReadLine());

            Console.Write("Interest rate: ");
            r = Convert.ToDouble(Console.ReadLine());

            Console.Write("Cost of carry: ");
            b = Convert.ToDouble(Console.ReadLine());

            Console.Write("Expiry date: ");
            T = Convert.ToDouble(Console.ReadLine());

            Console.Write("Maturity: ");
            s = Convert.ToDouble(Console.ReadLine());

            Console.Write("1. Call, 2. Put: ");
            type = Convert.ToString(Console.ReadLine());

            Option opt = new Option(OptionType.Call, T, K, b, r, sig, s);
            return opt;
        }
    }
}
