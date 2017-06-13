using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    public interface IPricing
    {
        double Price { get; set; }

        double Discount { get; set; }
    }
}
