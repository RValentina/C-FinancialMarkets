using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    public class NoNameException : ApplicationException
    {
        public NoNameException(string message) : base(message)
        { }
    }
}
