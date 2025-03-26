using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1ChristianMartinez
{
    internal class SalaryPlusCommissionEmployee : CommissionEmployee
    {
        private double _salary;

        public double Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }

        public SalaryPlusCommissionEmployee(EmployeeType employeeType, string firstName, double grossSales, double commissionRate, double salary) : base(employeeType, firstName, grossSales, commissionRate)
        {
            Salary = salary;
        }

        
        public override double GrossEarnings()
        {
            return Salary + base.GrossEarnings();
        }

        public override double Tax() => GrossEarnings() * 0.20;

        public override double NetEarnings() => GrossEarnings() * 0.80;


        public override string ToString()
        {
            return $"Salary Plus {base.ToString()}" +
                $"\nSalary {Salary:C}";
        }
    }
}
