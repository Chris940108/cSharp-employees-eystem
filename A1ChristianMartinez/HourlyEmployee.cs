using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1ChristianMartinez
{
    internal class HourlyEmployee : Employee
    {
        private double _hours;
        private double _wage;

        public double Hours
        {
            get { return _hours; }
            set
            {
                if (value < 0 || value > 168)
                {
                    Console.WriteLine("\n-----------------------!-----------------------------!--------------------\n" +
                        "Hours worked must be between 1 and 168, automaticaly asigned to 40.\nEdit employee if required." +
                        "\n-----------------------!-----------------------------!--------------------\n");
                    _hours = 40;
                }
                else
                    _hours = value;
            }
        }
        public double Wage
        {
            get { return _wage; }
            set
            {
                if (value < 0 || value > 100)
                {
                    Console.WriteLine("\n-----------------------!-----------------------------!--------------------\n" 
                        + "Wage per hour must be between 1 and 100, automaticaly asigned to 15.\nEdit employee if required."
                        + "\n-----------------------!-----------------------------!--------------------\n");
                    _wage = 15;
                }
                else
                    _wage = value;
            }
        }

        public HourlyEmployee(EmployeeType employeeType, string firstName, double hours, double wage) : base(employeeType, firstName)
        {
            Hours = hours;
            Wage = wage;
        }

        public override double GrossEarnings()
        {
            if (Hours <= 40)
                return Hours * Wage;
            else
                return (40 * Wage) + ((Hours - 40) * Wage * 1.5);
        }

        public override double Tax() => GrossEarnings() * 0.20;

        public override double NetEarnings() => GrossEarnings() * 0.80;


        public override string ToString()
        {
            return $"Hourly Employee: {base.ToString()}" +
                $"\nHours: {Hours}" +
                $"\nWage: {Wage:C}";
        }

    }
}
