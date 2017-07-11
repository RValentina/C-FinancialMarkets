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
            //Mediator med = new Mediator();
            //med.calculate();

            //Console.ReadLine();

            //convertible bond with option on equity basket chapter 7
            Vector<double> pricesVector = new Vector<double>(new double[] {1, 2}, 0);
            Vector<double> weightsVector = new Vector<double>(new double[] { 1, 2 }, 0);
            Vector<double> volatilityVector = new Vector<double>(new double[] { 1, 2 }, 0);
            NumericMatrix<double> correlationMatrix = new NumericMatrix<double>(2, 2, 0, 0);
            correlationMatrix.setRow(new Array<double>(new double[] { 1, 2 }, 0), 0);
            correlationMatrix.setRow(new Array<double>(new double[] { 1, 2 }, 0), 1);

            Option o = new Option(OptionType.Put, 2, 2, 3, pricesVector,weightsVector, correlationMatrix, volatilityVector);

            Console.WriteLine(o.Price());

            //dual currency linked bonds with exchange option chapter 7
            Option exchangeOption = new Option(OptionType.Call, 2, 2, 2, 3, 0.5, 0.4, 1);

            Console.WriteLine(exchangeOption.Price());

            Console.ReadLine();

        }



    }
}
