using System;

namespace Chapter6
{
    public static class ExtensionMethod
    {
        public static T Sum<T>(this Vector<T> v)
        {
            BinaryOperatorT<T, T, T> addTt = GenericOperatorFactory<T, T, T, Vector<T>>.Add;

            T result = default(T);

            for (int i = v.MinIndex; i < v.Length; i++)
            {
                result = addTt(result, v[i]);
            }

            return result;
        }

        public static T Average<T>(this Vector<T> v)
        {
            return v.Sum()/v;
        }

        public static double GeometricMean<T>(this Vector<T> v) where T : IConvertible
        {
            BinaryOperatorT<T, T, T> multiplyTt = GenericOperatorFactory<T, T, T, Vector<T>>.Multiply;

            T result = default(T);

            for (int i = v.MinIndex; i< v.Length; i++)
            {
                result = multiplyTt(result, v[i]);
            }

            return Math.Sqrt(Convert.ToDouble(result));
        }

        public static NumericMatrix<double> Exp<T>(this NumericMatrix<T> m) where T: IConvertible
        {
            NumericMatrix<double> expMatrix = new NumericMatrix<double>(m.Rows, m.Columns, 0, 0);

            for (int i = 0; i < m.Rows; i++)
            for (int j = 0; j < m.Columns; j++)
            {
                expMatrix[i, j] = Math.Exp(Convert.ToDouble(m[i, j]));
            }

            return expMatrix;
        }
    }
}
