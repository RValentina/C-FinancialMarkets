using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    public class ExternalOrder : Order
    {
        private double _discount;

        public ExternalOrder() : base() { }

        public ExternalOrder(string name, DateTime registrationDate, string company) :
            base(name, registrationDate)
        {
            this.Company = company;
        }

        public ExternalOrder(ExternalOrder externalOrder) : 
            base(externalOrder)
        {
            this.Company = externalOrder.Company;
        }

        public string Company { get; set; }

        public override double Discount
        {
            get
            {
                return _discount;
            }
            set
            {
                _discount = value;
            }
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine(Company.ToString());
        }
    }
}
