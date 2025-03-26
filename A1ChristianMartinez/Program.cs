using ConsoleTables;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace A1ChristianMartinez
{
    internal class Program
    {
        static int colorsInd = 1;
        static void Main(string[] args)
        {
            CultureInfo culture = new CultureInfo("en-US"); 
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            List<Employee> employees = new List<Employee>();

            // Salaried Employees
            employees.Add(new SalariedEmployee(EmployeeType.SalariedEmployee, "John", 500.0));
            employees.Add(new SalariedEmployee(EmployeeType.SalariedEmployee, "Emily", 600.0));
            employees.Add(new SalariedEmployee(EmployeeType.SalariedEmployee, "Michael", 700.0));

            // Hourly Employees
            employees.Add(new HourlyEmployee(EmployeeType.HourlyEmployee, "Anna", 50, 10.0));
            employees.Add(new HourlyEmployee(EmployeeType.HourlyEmployee, "James", 40, 12.5));
            employees.Add(new HourlyEmployee(EmployeeType.HourlyEmployee, "Olivia", 60, 15.0));

            // Commission Employees
            employees.Add(new CommissionEmployee(EmployeeType.CommissionEmployee, "Mark", 10000.0, 5));
            employees.Add(new CommissionEmployee(EmployeeType.CommissionEmployee, "Sophia", 15000.0, 6));
            employees.Add(new CommissionEmployee(EmployeeType.CommissionEmployee, "Ethan", 20000.0, 7));

            // Salary Plus Commission Employees
            employees.Add(new SalaryPlusCommissionEmployee(EmployeeType.SalaryPlusCommissionEmployee, "Sue", 10000.0, 6, 500.0));
            employees.Add(new SalaryPlusCommissionEmployee(EmployeeType.SalaryPlusCommissionEmployee, "Jack", 12000.0, 5, 600.0));
            employees.Add(new SalaryPlusCommissionEmployee(EmployeeType.SalaryPlusCommissionEmployee, "Lily", 14000.0, 7, 700.0));

            MainMenu(employees);
            


        }

        static void MainMenu(List<Employee> employees)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Employee Management System (EMS)\n");
            Console.WriteLine("\n+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+\n");

            Console.WriteLine("Select an option to continue:\n");
            Console.WriteLine("1 - Add Employee");
            Console.WriteLine("2 - Edit Employee");
            Console.WriteLine("3 - Delete Employee");
            Console.WriteLine("4 - View Employee");
            Console.WriteLine("5 - Search Employee");
            Console.WriteLine("6 - Exit");

            while (true)
            {


                Console.WriteLine("\nPlease enter an option (1-6):");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddEmployee(employees);
                        break;
                    case "2":
                        EditEmployee(employees);
                        break;
                    case "3":
                        DeleteEmployee(employees);
                        break;
                    case "4":
                        ViewEmployee(employees);
                        break;
                    case "5":
                        SearchEmployee(employees);
                        break;
                    case "6":
                        Console.WriteLine("Are you sure you want to exit? (y/n)");
                        string confirm = Console.ReadLine()?.ToLower();

                        if (confirm == "y" || confirm == "yes")
                        {
                            Console.WriteLine("Exiting the program. Goodbye!");
                            Thread.Sleep(1000);
                            Environment.Exit(0);
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option. You must enter a number between 1 and 6.");
                        break;
                }
            }
        }

        static void AddEmployee(List<Employee> employees)
        {
            Console.Clear();
            Console.WriteLine("Select the type of employee you want to add: ");
            EmployeeType employeeType = SelectEmployType(employees);
            int count = 0;
            foreach (Employee employee in employees)
            {
                if (employee.EmployeeType == employeeType)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                PrintTable(employees, employeeType);
            }
            Console.WriteLine("\nEnter the info for the new employee: ");
            string name = EnterName();

            switch (employeeType)
            {
                case EmployeeType.SalariedEmployee:
                    Console.WriteLine("Enter Employee Salary: ");
                    double salary = EnterNumber();
                    employees.Add(new SalariedEmployee(employeeType, name, salary));
                    break;

                case EmployeeType.CommissionEmployee:
                    Console.WriteLine("Enter Gross Sales: ");
                    double grossSales = EnterNumber();
                    Console.WriteLine("Enter Commission Rate: ");
                    double commissionRate = EnterNumber();
                    employees.Add(new CommissionEmployee(employeeType, name, grossSales, commissionRate));
                    break;

                case EmployeeType.SalaryPlusCommissionEmployee:
                    Console.WriteLine("Enter Gross Sales: ");
                    double grossSalesSPC = EnterNumber();
                    Console.WriteLine("Enter Commission Rate: ");
                    double commissionRateSPC = EnterNumber();
                    Console.WriteLine("Enter Salary: ");
                    double salarySPC = EnterNumber();
                    employees.Add(new SalaryPlusCommissionEmployee(employeeType, name, grossSalesSPC, commissionRateSPC, salarySPC));
                    break;

                case EmployeeType.HourlyEmployee:
                    Console.WriteLine("Enter Hours Worked: ");
                    double hours = EnterNumber();
                    Console.WriteLine("Enter Wage: ");
                    double wage = EnterNumber();
                    employees.Add(new HourlyEmployee(employeeType, name, hours, wage));
                    break;
            }

            Console.WriteLine("Employee added successfully!\n");
            PrintTable(employees, employeeType);
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            MainMenu(employees);
        }

        static void EditEmployee(List<Employee> employees)
        {
            Console.Clear();
            Console.WriteLine("Select the type of employee you want to edit: ");
            EmployeeType employeeType = SelectEmployType(employees);
            int count = 0;
            foreach (Employee employee in employees)
            {
                if (employee.EmployeeType == employeeType)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                PrintTable(employees, employeeType);
            }
            else
            {
                Console.WriteLine("There are no employees with this type to edit.");
                Console.WriteLine("Press any key to select another type.");
                Console.ReadKey();
                EditEmployee(employees);
            }
                

            Console.WriteLine("Enter the id of the employee you want to edit: ");
            int employeeId = SelectEmployeeId(employees, employeeType);
            string name = EnterName();

            switch (employeeType)
            {
                case EmployeeType.SalariedEmployee:
                    Console.WriteLine("Enter Employee Salary: ");
                    double salary = EnterNumber();
                    foreach(Employee employee in employees)
                    {
                        if (employeeId == employee.EmployeeId)
                        {
                            employee.FirstName = name;
                            ((SalariedEmployee)employee).WeeklySalary = salary;
                            break;
                        }
                    }
                    break;
                    
                case EmployeeType.CommissionEmployee:
                    Console.WriteLine("Enter Gross Sales: ");
                    double grossSales = EnterNumber();
                    Console.WriteLine("Enter Commission Rate: ");
                    double commissionRate = EnterNumber();
                    foreach (Employee employee in employees)
                    {
                        if (employeeId == employee.EmployeeId)
                        {
                            employee.FirstName = name;
                            ((CommissionEmployee)employee).GrossSales = grossSales;
                            ((CommissionEmployee)employee).CommissionRate = commissionRate;
                            break;
                        }
                    }
                    break;
                    

                case EmployeeType.SalaryPlusCommissionEmployee:
                    Console.WriteLine("Enter Gross Sales: ");
                    double grossSalesSPC = EnterNumber();
                    Console.WriteLine("Enter Commission Rate: ");
                    double commissionRateSPC = EnterNumber();
                    Console.WriteLine("Enter Salary: ");
                    double salarySPC = EnterNumber();
                    foreach (Employee employee in employees)
                    {
                        if (employeeId == employee.EmployeeId)
                        {
                            employee.FirstName = name;
                            ((SalaryPlusCommissionEmployee)employee).GrossSales = grossSalesSPC;
                            ((SalaryPlusCommissionEmployee)employee).CommissionRate = commissionRateSPC;
                            ((SalaryPlusCommissionEmployee)employee).Salary = salarySPC;
                            break;
                        }
                    }
                    break;

                case EmployeeType.HourlyEmployee:
                    Console.WriteLine("Enter Hours Worked: ");
                    double hours = EnterNumber();
                    Console.WriteLine("Enter Wage: ");
                    double wage = EnterNumber();
                    foreach (Employee employee in employees)
                    {
                        if (employeeId == employee.EmployeeId)
                        {
                            employee.FirstName = name;
                            ((HourlyEmployee)employee).Hours = hours;
                            ((HourlyEmployee)employee).Wage = wage;
                            break;
                        }
                    }
                    break;
            }

            Console.WriteLine("Employee edited successfully!\n");
            PrintTable(employees, employeeType);
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            MainMenu(employees);

        }

        static void DeleteEmployee(List<Employee> employees)
        {
            Console.Clear();
            Console.WriteLine("Select the type of employee you want to delete: ");
            EmployeeType employeeType = SelectEmployType(employees);
            int count = 0;
            foreach (Employee employee in employees)
            {
                if (employee.EmployeeType == employeeType)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                PrintTable(employees, employeeType);
            }
            else
            {
                Console.WriteLine("There are no employees with this type to delete.");
                Console.WriteLine("Press any key to select another type.");
                Console.ReadKey();
                DeleteEmployee(employees);
            }

            Console.WriteLine("Enter the id of the employee you want to delete: ");
            int employeeId = SelectEmployeeId(employees, employeeType);

            foreach (Employee employee in employees)
            {
                if (employee.EmployeeId == employeeId){
                    employees.Remove(employee);
                    break;
                }
            }
            
            Console.WriteLine("Employee deleted successfully!\n");
            count = 0;
            foreach (Employee employee in employees)
            {
                if (employee.EmployeeType == employeeType)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                PrintTable(employees, employeeType);
            }
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey(); ;
            MainMenu(employees);
        }

        static void ViewEmployee(List<Employee> employees)
        {
            Console.Clear();
            PrintTable(employees);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            MainMenu(employees);
        }

        static void SearchEmployee(List<Employee> employees)
        {
            Console.Clear();

            Console.WriteLine("Enter the complete or partial name of the employee you want to search for: ");
            string name = EnterName();

            List<Employee> employeesSearched = new List<Employee>();
            bool salaried = false;
            bool commission= false;
            bool commissionSalaried = false;
            bool hourly = false;
            foreach (Employee employee in employees)
            {
                if (employee.FirstName.ToLower().Contains(name.ToLower()))
                {
                    employeesSearched.Add(employee);
                    if (employee.EmployeeType == EmployeeType.CommissionEmployee)
                    {
                        commission = true;
                    }
                    if (employee.EmployeeType == EmployeeType.HourlyEmployee)
                    {
                        hourly = true;                        
                    }
                    if (employee.EmployeeType == EmployeeType.SalariedEmployee)
                    {
                        salaried = true;
                    }
                    if (employee.EmployeeType == EmployeeType.SalaryPlusCommissionEmployee)
                    {
                        commissionSalaried = true;
                    }
                }
            }

            if (employeesSearched.Count > 0)
            {
                
                if (commission)
                {
                    PrintTable(employeesSearched, EmployeeType.CommissionEmployee);
                }
                if (commissionSalaried)
                {
                    PrintTable(employeesSearched, EmployeeType.SalaryPlusCommissionEmployee);
                }
                if (salaried)
                {
                    PrintTable(employeesSearched, EmployeeType.SalariedEmployee);
                }
                if (hourly)
                {
                    PrintTable(employeesSearched, EmployeeType.HourlyEmployee);
                }
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
                MainMenu(employees);
            }
            else
            {
                Console.WriteLine("Not match results.");
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
                MainMenu(employees);
            }



        }

        static EmployeeType SelectEmployType(List<Employee> employees)
        {
            Console.WriteLine("\n1 - Hourly Employee");
            Console.WriteLine("2 - Commission Employee");
            Console.WriteLine("3 - Salaried Employee");
            Console.WriteLine("4 - Salary + Commission Employee");
            Console.WriteLine("5 - Back to main menu");

            
            while (true)
            {
                Console.WriteLine("\nPlease enter an option (1-5):");
                string input = Console.ReadLine();

                EmployeeType employeeType;
                switch (input)
                {
                    case "1":
                        employeeType = EmployeeType.HourlyEmployee;
                        break;
                    case "2":
                        employeeType = EmployeeType.CommissionEmployee;
                        break;
                    case "3":
                        employeeType = EmployeeType.SalariedEmployee;
                        break;
                    case "4":
                        employeeType = EmployeeType.SalaryPlusCommissionEmployee;
                        break;
                    case "5":
                        MainMenu(employees);
                        continue;
                    default:
                        Console.WriteLine("Invalid option. You must enter a number between 1 and 5.");
                        continue;
                }

                return employeeType;
            }

        }
            
        static string EnterName()
        {
            while (true)
            {
                Console.WriteLine("Name of the Employee (Only Letters): ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name) || !Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    Console.WriteLine("Invalid input.\n");
                    continue;
                }
                else
                {
                    return name;
                }
            }

        }

        static double EnterNumber()
        {
            while (true)
            {
                
                string salarystr = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(salarystr) || !Regex.IsMatch(salarystr, @"^[0-9.,]+$"))
                {
                    Console.WriteLine("You can only enter numbers.");
                    continue;
                }
                else if ((double.Parse(salarystr)) == 0)
                {
                    Console.WriteLine("You must enter a number greater than 0.");
                }
                else
                {
                    return double.Parse(salarystr);
                }

            }
        }

        static int SelectEmployeeId(List<Employee> employees, EmployeeType employeeType)
        {
            while (true)
            {
                string id = Console.ReadLine();
                for (int i = 0; i < employees.Count; i++)
                {
                    if (employees[i].EmployeeType == employeeType)
                    {
                        if ((employees[i].EmployeeId).ToString() == id)
                        {
                            return int.Parse(id);
                        }
                    }
                }
                Console.WriteLine("Enter a valid value.");
            }
        }

        static void PrintTable(List<Employee> employee, EmployeeType employeeType) {

            Random rnd = new Random();
            List<ConsoleColor> colors = [ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Gray, ConsoleColor.Yellow];


            Console.ForegroundColor = colors[colorsInd++ % 4];

            switch (employeeType)

            {
                case EmployeeType.HourlyEmployee:
                    Console.WriteLine("Hourly Employees: ");
                    var table = new ConsoleTable("Id", "Name", "Hours Worked", "Hourly Wage", "Gross Earnings", "Tax (20%)", ("Net Earnings"));
                                                for (int i = 0; i < employee.Count; i++) {
                        if (employee[i].EmployeeType == employeeType)
                        {
                            table.AddRow(employee[i].EmployeeId, employee[i].FirstName, ((HourlyEmployee)employee[i]).Hours, $"${((HourlyEmployee)employee[i]).Wage.ToString("N2")}", $"${employee[i].GrossEarnings().ToString("N2")}", $"${(employee[i].Tax()).ToString("N2")}", $"${(employee[i].NetEarnings()).ToString("N2")}");

                        }
                    }
                    table.Write(Format.Alternative);
                    Console.WriteLine();

                    break;

                case EmployeeType.SalariedEmployee:
                    Console.WriteLine("Salaried Employees: ");
                    table = new ConsoleTable("Id", "Name", "Salary", "Gross Earnings", "Tax (20%)", ("Net Earnings)"));
                                                for (int i = 0; i < employee.Count; i++)
                    {
                        if (employee[i].EmployeeType == employeeType)
                        {
                            table.AddRow(employee[i].EmployeeId, employee[i].FirstName,$"${((SalariedEmployee)employee[i]).WeeklySalary.ToString("N2")}", $"${employee[i].GrossEarnings().ToString("N2")}", $"${(employee[i].Tax()).ToString("N2")}", $"${(employee[i].NetEarnings()).ToString("N2")}");

                        }
                    }
                    table.Write(Format.Alternative);
                    Console.WriteLine();

                    break;

                case EmployeeType.CommissionEmployee:
                    Console.WriteLine("Commission Employees: ");
                    table = new ConsoleTable("Id", "Name", "Gross Sales", "Commission Rate", "Gross Earnings", "Tax (20%)", ("Net Earnings"));
                                                for (int i = 0; i < employee.Count; i++)
                    {
                        if (employee[i].EmployeeType == employeeType)
                        {
                            table.AddRow(employee[i].EmployeeId, employee[i].FirstName, $"${((CommissionEmployee)employee[i]).GrossSales.ToString("N2")}", $"{((CommissionEmployee)employee[i]).CommissionRate.ToString("N2")}%", $"${employee[i].GrossEarnings().ToString("N2")}", $"${(employee[i].Tax()).ToString("N2")}", $"${(employee[i].NetEarnings()).ToString("N2")}");

                        }
                    }
                    table.Write(Format.Alternative);
                    Console.WriteLine();

                    break;

                case EmployeeType.SalaryPlusCommissionEmployee:
                    Console.WriteLine("Salary Plus Commission Employees: ");
                    table = new ConsoleTable("Id", "Name", "Salary", "Gross Sales", "Commission Rate", "Gross Earnings", "Tax (20%)", ("Net Earnings"));
                                                for (int i = 0; i < employee.Count; i++)
                    {
                        if (employee[i].EmployeeType == employeeType)
                        {
                            table.AddRow(employee[i].EmployeeId, employee[i].FirstName, $"${((SalaryPlusCommissionEmployee)employee[i]).Salary.ToString("N2")}", $"${((SalaryPlusCommissionEmployee)employee[i]).GrossSales.ToString("N2")}", $"{((SalaryPlusCommissionEmployee)employee[i]).CommissionRate.ToString("N2")}%", $"${employee[i].GrossEarnings().ToString("N2")}", $"${(employee[i].Tax()).ToString("N2")}", $"${(employee[i].NetEarnings()).ToString("N2")}");

                        }
                    }
                    table.Write(Format.Alternative);
                    Console.WriteLine();
                    break;

            }
            
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void PrintTable(List<Employee> employee)
        {
            if (employee.Count == 0)
            {
                Console.WriteLine("There are no employees to show.\n");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                MainMenu(employee);
                
            }

            int count = 0;
            foreach (Employee emp in employee)
            {
                if (emp.EmployeeType == EmployeeType.SalaryPlusCommissionEmployee)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                PrintTable(employee, EmployeeType.SalaryPlusCommissionEmployee);
            }
            count = 0;
            foreach (Employee emp in employee)
            {
                if (emp.EmployeeType == EmployeeType.CommissionEmployee)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                PrintTable(employee, EmployeeType.CommissionEmployee);
            }
            count = 0;
            foreach (Employee emp in employee)
            {
                if (emp.EmployeeType == EmployeeType.HourlyEmployee)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                PrintTable(employee, EmployeeType.HourlyEmployee);
            }
            count = 0;
            foreach (Employee emp in employee)
            {
                if (emp.EmployeeType == EmployeeType.SalariedEmployee)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                PrintTable(employee, EmployeeType.SalariedEmployee);
            }

            
        }
        }

    
}
