using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter6
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector<int> v = new Vector<int>(3, 0);

            v[0] = 3;
            v[1] = 4;
            v[2] = 5;

            var sum = v.Sum();
            var avg = v.Average();
            var gMean = v.GeometricMean();

            Console.WriteLine(sum);
            Console.WriteLine(avg);
            Console.WriteLine(gMean);

            Console.ReadLine();
        }
    }
}
