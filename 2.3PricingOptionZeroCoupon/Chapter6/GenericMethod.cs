using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter6
{
    public class GenericMethod<T> where T : IComparable<T>
    {
        public T Min(T type1, T type2)
        {
            return (Comparer<T>.Default.Compare(type1, type2) < 0) ? type1 : type2;
        }

        public T Max(T type1, T type2)
        {
            return (Comparer<T>.Default.Compare(type1, type2) > 0) ? type1 : type2;
        }
    }
}
