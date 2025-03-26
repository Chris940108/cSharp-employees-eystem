using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1ChristianMartinez
{
    internal class SalariedEmployee : Employee
    {
        private double _weeklySalary;

        public double WeeklySalary
        {
            get { return _weeklySalary; }
            set { _weeklySalary = value; }
        }

        public SalariedEmployee(EmployeeType employeeType, string firstName, double weeklySalary) : base(employeeType, firstName)
        {
            WeeklySalary = weeklySalary;
        }

        public override double GrossEarnings()
        {
            return WeeklySalary;
        }

        public override double Tax() => GrossEarnings() * 0.20;

        public override double NetEarnings() => GrossEarnings() * 0.80;

        public override string ToString()
        {
            return $"Salaried Employee: {base.ToString()}" +
                $"\nWeekly Salary: {WeeklySalary:C}";
        }
    }
}
