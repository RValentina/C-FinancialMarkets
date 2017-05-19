using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointExample
{
    public struct LineSegment
    {
        private Point startPoint;
        private Point endPoint;

        public LineSegment(Point p1, Point p2)
        {
            startPoint = p1;
            endPoint = p2;
        }

        public LineSegment(LineSegment l)
        {
            this = l;
        }

        Point start()
        {
            return startPoint;
        }

        Point end()
        {
            return endPoint;
        }

        void start(Point pt)
        {
            startPoint = pt;
        }

        void end(Point pt)
        {
            endPoint = pt;
        }

        public double length()
        {
            return Math.Round(Math.Sqrt((start().x - end().x) * (start().x - end().x) +
                                    (start().y - end().y) * (start().y - end().y)), 4);
        }

        public Point MidPoint()
        {
            Point midPoint = new Point();
            midPoint.x = (start().x + end().x) / 2;
            midPoint.y = (start().y + end().y) / 2;

            return midPoint;
        }
    }
}
