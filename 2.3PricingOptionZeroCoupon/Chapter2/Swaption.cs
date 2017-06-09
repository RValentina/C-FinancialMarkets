using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOptionPricing
{
    public class Swaption : Option
    {
        //tenor of swap in years
        private double t; 

        //no of compoundings per year in swap rate
        private double m; 

        public Swaption(double tenor, double forwardRate, double strike, double interestRate, double expiry, double volatility, double comp, OptionType optionType)
            :base(optionType, expiry, strike, 0, interestRate, volatility)
        {
            t = tenor;
            m = comp;
        }

        private double Alpha(double f, double m, double t)
        {
            return (1 - 1 / Math.Pow((1 + f / m), t * m)) / f;
        }

        protected override double CallPrice(double U)
        {
            return Alpha(U, m, t) * base.CallPrice(U);
        }

        protected override double PutPrice(double U)
        {
            return Alpha(U, m , t) * base.PutPrice(U);
        }
    }
}
