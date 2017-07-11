using System;
using System.Runtime.CompilerServices;
using Chapter6;

namespace MiniOptionPricing
{
    public class Option
    {
        private double _r; //interest rate
        private double _sig; //volatility
        private double _k; //strike price
        private double _t; //expiry date
        private double _b; //cost of carry
        private readonly double _s = 1; //maturity
        private readonly double? _underlyingPrice;

        private OptionType _type;

        public Option()
        {
            //default call option
            Init();
        }

        public Option(OptionType optionType)
        {
            Init();
            _type = optionType;
        }

        public Option(OptionType optionType, double expiry, double strike, double costOfCarry,
            double interest, double volatility)
        {
            _type = optionType;
            _t = expiry;
            _k = strike;
            _b = costOfCarry;
            _r = interest;
            _sig = volatility;
        }

        public Option(OptionType optionType, double expiry, double strike, double costOfCarry,
            double interest, double volatility, double maturity)
        {
            _type = optionType;
            _t = expiry;
            _k = strike;
            _b = costOfCarry;
            _r = interest;
            _sig = volatility;
            _s = maturity;

        }

        //constructor for option on the equity basket
        public Option(OptionType optionType, double expiry, double strike, double interest, Vector<double> pricesVector,
            Vector<double> weightsVector, NumericMatrix<double> correlationMatrix, Vector<double> volatilityVector) 
        {
            _type = optionType;
            _t = expiry;
            _k = strike;
            _b = 0;
            _r = interest;
            _sig = VolatilityConvertibleBond(pricesVector, weightsVector, correlationMatrix, volatilityVector);
            _s = expiry;
            _underlyingPrice = M(pricesVector, weightsVector, correlationMatrix, volatilityVector).M1;
        }

        //constructor for exchange option
        public Option(OptionType optionType, double expiry, double strike, double interestU, double interestV,
            double volatilityU, double volatilityV, double correlationUv) : this(optionType, expiry, strike, interestU - interestV, interestU, 
                                                            Math.Sqrt(volatilityU * volatilityU + volatilityV * volatilityV - 2 * correlationUv * volatilityU * volatilityV))
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private (double d1, double d2, double temp) D(double u)
        {
            double temp = _sig * Math.Sqrt(_t);
            double d1 = (Math.Log(u / _k) + (_b + (_sig * _sig) * 0.5) * _t) / temp;
            double d2 = d1 - temp;

            return (d1, d2, temp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private (double M1, double M2) M(Vector<double> pricesVector, Vector<double> weightsVector, NumericMatrix<double> correlationMatrix, Vector<double> volatilityVector)
        {
            Vector<double> pricesWeightsVector = pricesVector * weightsVector;
            double m1 = pricesWeightsVector.Sum();

            NumericMatrix<double> volatilityMatrix = new NumericMatrix<double>(volatilityVector.Length, volatilityVector.Length, 0, 0);

            for (int i = 0; i < volatilityVector.Length; i++)
            for (int j = 0; j < volatilityVector.Length; j++)
            {
                volatilityMatrix[i,j] = volatilityVector[i] * volatilityVector[j] * correlationMatrix[i, j] * _t;
            }

            NumericMatrix<double> expMatrix = volatilityMatrix.Exp();

            double m2 = 0;

            for (int row = 0; row < expMatrix.Rows; row++)
            {
                m2 += (pricesWeightsVector * expMatrix.getRow(row)).Sum() * weightsVector[row] * pricesVector[row];
            }

            return (m1, m2);
        }

        private double VolatilityConvertibleBond(Vector<double> pricesVector, Vector<double> weightsVector, NumericMatrix<double> correlationMatrix, Vector<double> volatilityVector)
        {
            var m = M(pricesVector, weightsVector, correlationMatrix, volatilityVector);

            return Math.Sqrt(Math.Log(m.M2 / (m.M1 * m.M1)) * 1/_t);
        }

        //Kernel functions (Haug)
        protected virtual double CallPrice(double u)
        {
            var d = D(u);

            return (u * Math.Exp((_b - _r) * _t) * SpecialFunctions.N(d.d1))
              - (_k * Math.Exp(-_r * _t) * SpecialFunctions.N(d.d2));
        }

        protected virtual double PutPrice(double u)
        {
            var d = D(u);

            return (_k * Math.Exp(-_r * _t) * SpecialFunctions.N(- d.d2))
                - (u * Math.Exp((_b - _r) * _t) * SpecialFunctions.N(- d.d1));
        }

        public double UnderlyingPrice()
        {
            return Math.Exp(-_r * _s) / Math.Exp(-_r * _t);
        }

        public void Init()
        {
            //default values
            _r = 0.08;
            _sig = 0.30;
            _k = 65.0;
            _t = 0.25;
            _b = _r;

            _type = OptionType.Call;
        }

        //calculate option price and sensitivities
        public double Price(double u)
        {
            if (_type == OptionType.Call)
                return CallPrice(u);
            else
                return PutPrice(u);
        }

        //calculate option price for a zero coupon bond
        public double Price()
        {
            return Price(_underlyingPrice ?? UnderlyingPrice());
            
        }

        public double Vega(double u)
        {
            double tmp = _sig * Math.Sqrt(_t);
            double d1 = (Math.Log(u / _k) + (_b + (_sig * _sig) * 0.5) * _t) / tmp;

            return _s * Math.Exp((_b - _r) * _t) * SpecialFunctions.n(d1) * Math.Sqrt(_t);
            //return 0;
        }

        public double CallRho(double u)
        {
            double tmp = _sig * Math.Sqrt(_t);

            double d1 = (Math.Log(u / _k) + (_b + (_sig * _sig) * 0.5) * _t) / tmp;
            double d2 = d1 - tmp;

            return _t * Math.Exp((_b - _r) * _t) * SpecialFunctions.N(d2);
        }

        public double PutRho(double u)
        {
            double tmp = _sig * Math.Sqrt(_t);

            double d1 = (Math.Log(u / _k) + (_b + (_sig * _sig) * 0.5) * _t) / tmp;
            double d2 = d1 - tmp;

            return -_t * Math.Exp((_b - _r) * _t) * SpecialFunctions.N(-d2);
        }

        public double CallCharm(double u)
        {
            var (d1, d2, tmp) = D(u);

            //From Haug - Complete guide to Option Pricing formulas
            var x = -Math.Exp((_b - _r) * _t);
            var y = SpecialFunctions.n(d1) * ((_b / (_sig * Math.Sqrt(_t))) - (d2 / (2 * _t)));
            var z = (_b - _r) * SpecialFunctions.N(d1);

            var charm = x * (y + z);

            return charm;
        }

        public double PutCharm(double u)
        {
            var (d1, d2, tmp) = D(u);

            //From Haug - Complete guide to Option Pricing formulas
            var x = -Math.Exp((_b - _r) * _t);
            var y = SpecialFunctions.n(d1) * ((_b / (_sig * Math.Sqrt(_t))) - (d2 / (2 * _t)));
            var z = (_b - _r) * SpecialFunctions.N(-d1);

            var charm = x * (y - z);

            return charm;
        }

        /// <summary>
        /// Calculates the Call Vomma
        /// </summary>
        /// <remarks>
        /// From Ch.3 Homework
        /// </remarks>
        /// <param name="u">Price of the underlying instrument</param>
        /// <returns>The value of Vomma</returns>
        public double CallVomma(double u)
        {
            var (d1, d2, tmp) = D(u);

            return Vega(d1 * d2 / _sig);
        }

        /// <summary>
        /// Calculates the Call Vomma
        /// </summary>
        /// <remarks>
        /// From Ch.3 Homework
        /// </remarks>
        /// <param name="u">Price of the underlying instrument</param>
        /// <returns>The value of Vomma</returns>
        public double PutVomma(double u)
        {
            var (d1, d2, tmp) = D(u);

            return Vega(d1 * d2 / _sig);
        }
    }
}
