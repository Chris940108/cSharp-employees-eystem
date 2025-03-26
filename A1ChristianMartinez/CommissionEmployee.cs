using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1ChristianMartinez
{
    internal class CommissionEmployee : Employee
    {
        private double _grossSales;
        private double _commissionRate;

        public double GrossSales
        {
            get { return _grossSales; }

            set { _grossSales = value; }
        }
        public double CommissionRate
        {
            get { return _commissionRate; }
            set
            {
                if (value < 0 || value > 100)
                {
                    Console.WriteLine("\n-----------------------!-----------------------------!--------------------\n" 
                        + "Commission must be between 1 and 100, automaticaly asigned to 20.\nEdit employee if required." +
                        "\n-----------------------!-----------------------------!--------------------\n");
                    _commissionRate = 20;
                }
                else
                    _commissionRate = value;
            }
        }

        public CommissionEmployee(EmployeeType employeetype, string firstName, double grossSales, double commissionRate) : base(employeetype, firstName)
        {
            GrossSales = grossSales;
            CommissionRate = commissionRate;
        }

        public override double GrossEarnings()
        {
            return GrossSales * CommissionRate / 100;
        }

        public override double Tax() => GrossEarnings() * 0.20;

        public override double NetEarnings() => GrossEarnings() * 0.80;


        public override string ToString()
        {
            return $"Commission Employee: {base.ToString()}" +
                $"\nGross Sales: {GrossSales:C}" +
                $"\nCommission Rate: {CommissionRate:P}";
        }
    }
}
