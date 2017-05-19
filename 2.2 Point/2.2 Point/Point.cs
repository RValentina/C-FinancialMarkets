using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointExample
{
    public struct Point
    {
        public double x;
        public double y;
        public Point(double xVal, double yVal)
        {
            x = xVal;
            y = yVal;
        }

        public double distance(Point p2)
        {
            return Math.Round(Math.Sqrt((x - p2.x) * (x - p2.x) + (y - p2.y) * (y - p2.y)), 4);
        }

        public override string ToString()
        {
            return string.Format("Point ({0}, {1})", x, y);
        }
    }

}
