using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    public class Order : IPricing
    {
        private static int number;

        private double _price;

        static Order()
        {
            number = 1;
        }

        public Order() 
        {
            this.RegistrationDate = DateTime.Today;

            this.Name = "order#" + number;

            number++;
        }

        public Order(Order order)
        {
            this.Name = order.Name;
            this.RegistrationDate = order.RegistrationDate;
        }

        public Order(string name, DateTime registrationDate) 
        {
            this.Name = name;
            this.RegistrationDate = registrationDate;
        }

        public string Name { get; set; }

        public DateTime RegistrationDate { get; set; }

        public double Price 
        {
            get
            {
                return _price - Discount;
            }
            set
            {
                _price = value;
            }
        }

        public virtual double Discount 
        {
            get
            {
                return 0;
            }
            set { }
        }

        public static int Number 
        {
            get 
            {
                return number;
            }
        }

        public virtual void Print()
        {
            Console.WriteLine(Name.ToString());
            Console.WriteLine(RegistrationDate.ToString());
        }


    }
}
