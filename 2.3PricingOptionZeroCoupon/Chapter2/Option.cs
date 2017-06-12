using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MiniOptionPricing
{
    public class Option
    {
        private double r; //interest rate
        private double sig; //volatility
        private double K; //strike price
        private double T; //expiry date
        private double b; //cost of carry
        private double s = 1; //maturity

        private OptionType type;

        public Option()
        {
            //default call option
            init();
        }

        public Option(OptionType optionType)
        {
            init();
            type = optionType;
        }

        public Option(OptionType optionType, double expiry, double strike, double costOfCarry,
            double interest, double volatility)
        {
            type = optionType;
            T = expiry;
            K = strike;
            b = costOfCarry;
            r = interest;
            sig = volatility;
        }

        public Option(OptionType optionType, double expiry, double strike, double costOfCarry,
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

        public Option(OptionType optionType, string underlying)
        {
            init();
            type = optionType;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private (double d1, double d2, double temp) D(double U)
        {
            double temp = sig * Math.Sqrt(T);
            double d1 = (Math.Log(U / K) + (b + (sig * sig) * 0.5) * T) / temp;
            double d2 = d1 - temp;

            return (d1, d2, temp);
        }

        //Kernel functions (Haug)
        protected virtual double CallPrice(double U)
        {
            double tmp = sig * Math.Sqrt(T);

            var d = D(U);

            return (U * Math.Exp((b - r) * T) * SpecialFunctions.N(d.d1))
              - (K * Math.Exp(-r * T) * SpecialFunctions.N(d.d2));
        }

        protected virtual double PutPrice(double U)
        {
            var d = D(U);

            return (K * Math.Exp(-r * T) * SpecialFunctions.N(- d.d2))
                - (U * Math.Exp((b - r) * T) * SpecialFunctions.N(- d.d1));
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

            type = OptionType.Call;
        }

        //calculate option price and sensitivities
        public double Price(double U)
        {
            if (type == OptionType.Call)
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

        public double Vega(double U)
        {
            double tmp = sig * Math.Sqrt(T);
            double d1 = (Math.Log(U / K) + (b + (sig * sig) * 0.5) * T) / tmp;

            return s * Math.Exp((b - r) * T) * SpecialFunctions.n(d1) * Math.Sqrt(T);
            //return 0;
        }

        public double CallRho(double U)
        {
            double tmp = sig * Math.Sqrt(T);

            double d1 = (Math.Log(U / K) + (b + (sig * sig) * 0.5) * T) / tmp;
            double d2 = d1 - tmp;

            return T * Math.Exp((b - r) * T) * SpecialFunctions.N(d2);
        }

        public double PutRho(double U)
        {
            double tmp = sig * Math.Sqrt(T);

            double d1 = (Math.Log(U / K) + (b + (sig * sig) * 0.5) * T) / tmp;
            double d2 = d1 - tmp;

            return -T * Math.Exp((b - r) * T) * SpecialFunctions.N(-d2);
        }

        private double CallCharm(double U)
        {
            var (d1, d2, tmp) = D(U);

            //From Haug - Complete guide to Option Pricing formulas
            var x = -Math.Exp((b - r) * T);
            var y = SpecialFunctions.n(d1) * ((b / (sig * Math.Sqrt(T))) - (d2 / (2 * T)));
            var z = (b - r) * SpecialFunctions.N(d1);

            var charm = x * (y + z);

            return charm;
        }

        private double PutCharm(double U)
        {
            var (d1, d2, tmp) = D(U);

            //From Haug - Complete guide to Option Pricing formulas
            var x = -Math.Exp((b - r) * T);
            var y = SpecialFunctions.n(d1) * ((b / (sig * Math.Sqrt(T))) - (d2 / (2 * T)));
            var z = (b - r) * SpecialFunctions.N(-d1);

            var charm = x * (y - z);

            return charm;
        }

        /// <summary>
        /// Calculates the Call Vomma
        /// </summary>
        /// <remarks>
        /// From Ch.3 Homework
        /// </remarks>
        /// <param name="U">Price of the underlying instrument</param>
        /// <returns>The value of Vomma</returns>
        private double CallVomma(double U)
        {
            var (d1, d2, tmp) = D(U);

            return Vega(d1 * d2 / sig);
        }

        /// <summary>
        /// Calculates the Call Vomma
        /// </summary>
        /// <remarks>
        /// From Ch.3 Homework
        /// </remarks>
        /// <param name="U">Price of the underlying instrument</param>
        /// <returns>The value of Vomma</returns>
        private double PutVomma(double U)
        {
            var (d1, d2, tmp) = D(U);

            return Vega(d1 * d2 / sig);
        }
    }
}
