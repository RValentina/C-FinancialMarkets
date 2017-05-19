using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointExample;

namespace UnitTestPoint
{
    [TestClass]
    public class LineSegmentTest
    {
        [TestMethod]
        public void LineLengthSamePoints()
        {
            LineSegment l = new LineSegment(new Point(10, 20), new Point(10, 20));

            Assert.AreEqual(0, l.length());
        }

        [TestMethod]
        public void LineLengthDifferentPoints()
        {
            LineSegment l = new LineSegment(new Point(-1000, 20000), new Point(1555, -1000));

            Assert.AreEqual(21154.8582, l.length());
        }

        [TestMethod]
        public void MidpointSamePoints()
        {
            LineSegment l = new LineSegment(new Point(10, 20), new Point(10, 20));

            Assert.AreEqual(new Point(10,20), l.MidPoint());
        }

        [TestMethod]
        public void MidpointDifferentPoints()
        {
            LineSegment l = new LineSegment(new Point(10,20), new Point(-10,-20));

            Assert.AreEqual(new Point(0, 0), l.MidPoint());
        }
    }
}
