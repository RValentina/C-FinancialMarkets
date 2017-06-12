using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniOptionPricing;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SwaptionCallPrice()
        {
            var t = 4;
            var m = 2;
            var F = 0.07;
            var K = 0.075;
            var T = 2;
            var r = 0.06;
            var v = 0.2;

            var option = new Swaption(t, F, K, r, T, v, m, OptionType.Call);

            var swaptionPrice = option.Price(F);

            Assert.AreEqual(0.01796, swaptionPrice, 0.01);

        }
    }
}
