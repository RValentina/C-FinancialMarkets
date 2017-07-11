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
    }
}
