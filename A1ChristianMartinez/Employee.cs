using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1ChristianMartinez
{
    public enum EmployeeType
    {
        CommissionEmployee, HourlyEmployee, SalariedEmployee, SalaryPlusCommissionEmployee
    }
    public abstract class Employee
    {
        private int _employeeId;
        private EmployeeType _employeeType;
        private string _firstName;
        private static int _idCount = 100;
        


        public int EmployeeId
        {
            get { return _employeeId; }
            private set { _employeeId = value; }
        }

        public EmployeeType EmployeeType
        {
            get { return _employeeType; }
            private set { _employeeType = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public abstract double GrossEarnings();

        public abstract double Tax();

        public abstract double NetEarnings();

        public Employee(EmployeeType employeeType, string firstName)
        {
            EmployeeId = _idCount++;
            EmployeeType = employeeType;
            FirstName = firstName;
        }

        public override string ToString()
        {
            return $"{FirstName}\nEmployee Id: {EmployeeId}" +
                $"\nEmployee Type: {EmployeeType}"; 
        }

    }
}
