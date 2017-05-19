using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p1;

            p1.x = 10;
            p1.y = 20;

            Point p2 = new Point(10, 20);
            Point p3 = p2;

            Console.WriteLine("p1: {0}", p1);
            Console.WriteLine("p2: {0}", p2);
            Console.WriteLine("p3=p2: {0}", p3);

            p3.x = 1;
            p3.y = 2;

            Console.WriteLine("p2: {0}", p2);
            Console.WriteLine("p3: {0}", p3);


            Console.WriteLine(p1.distance(p3));

            LineSegment l = new LineSegment(p1, p3);

            Console.WriteLine("Midpoint: {0}", l.MidPoint().ToString());
            Console.WriteLine("Distance: {0}", l.length().ToString());

            Console.ReadLine();
        }
    }

}
