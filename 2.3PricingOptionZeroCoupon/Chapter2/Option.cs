using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniOptionPricing
{
    public class Option
    {
        private double r;
        private double sig;
        private double K;
        private double T;
        private double b;
        private double s = 1;

        private string type;

        public Option()
        {
            //default call option
            init();
        }

        public Option(string optionType)
        {
            init();
            type = optionType;

            if (type == "c")
                type = "C";
        }

        public Option(string optionType, double expiry, double strike, double costOfCarry,
            double interest, double volatility)
        {
            type = optionType;
            T = expiry;
            K = strike;
            b = costOfCarry;
            r = interest;
            sig = volatility;
        }

        public Option(string optionType, double expiry, double strike, double costOfCarry,
            double interest, double volatility, double maturity)
        {
            type = optionType;
            T = expiry;
            K = strike;
            b = costOfCarry;
            r = interest;
            sig = volatility;
            s = maturity;

        }

        public Option(string optionType, string underlying)
        {
            init();
            type = optionType;
        }

        //Kernel functions (Haug)
        private double CallPrice(double U)
        {
            double tmp = sig * Math.Sqrt(T);

            double priceMaturity = Math.Exp(-r * s) / Math.Exp(-r * T);

            double d1 = (Math.Log(U/K) + (b + (sig*sig) * 0.5) * T) / tmp;
            double d2 = d1 - tmp;

            return (U * Math.Exp((b - r) * T) * SpecialFunctions.N(d1))
              - (K * Math.Exp(-r * T) * SpecialFunctions.N(d2));
        }

        public double PutPrice(double U)
        {
            double tmp = sig * Math.Sqrt(T);

            double d1 = (Math.Log(U / K) + (b + (sig * sig) * 0.5) * T) / tmp;
            double d2 = d1 - tmp;

            return (K * Math.Exp(-r * T) * SpecialFunctions.N(- d2))
                - (U * Math.Exp((b - r) * T) * SpecialFunctions.N(- d1));
        }

        public double UnderlyingPrice()
        {
            return Math.Exp(-r * s) / Math.Exp(-r * T);
        }

        public void init()
        {
            //default values
            r = 0.08;
            sig = 0.30;
            K = 65.0;
            T = 0.25;
            b = r;

            type = "C";
        }

        //calculate option price and sensitivities
        public double Price(double U)
        {
            if (type == "1")
                return CallPrice(U);
            else
                return PutPrice(U);
        }

        //calculate option price for a zero coupon bond
        public double Price()
        {
            double U = UnderlyingPrice();

            return Price(U);
            
        }
    }
}
