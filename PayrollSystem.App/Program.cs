using PayrollSystem;
using PayrollSystem;

class Program
{
    static void Main(string[] args)
    {
        CompanyPayroll payroll = new CompanyPayroll(2);

        FullTimeEmployee employee1 = new FullTimeEmployee();
        employee1.Name = "Ahmad";
        employee1.Role = EmployeeRole.Developer;
        employee1.BaseSalary = ReadValidSalary(employee1.Name);

        FullTimeEmployee employee2 = new FullTimeEmployee();
        employee2.Name = "Sara";
        employee2.Role = EmployeeRole.Tester;
        employee2.BaseSalary = ReadValidSalary(employee2.Name);

        payroll[0] = employee1;
        payroll[1] = employee2;

        payroll.OnSalaryProcessed += ShowNotification;

        payroll.RunPayroll();
    }
    static void ShowNotification(string message)
    {
        Console.WriteLine($"Notification: {message}");
    }
    static decimal ReadValidSalary(string employeeName)
    {
        while (true)
        {
            Console.Write($"Enter {employeeName}'s base salary: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            if (salary < 1000)
            {
                Console.WriteLine("Salary must be at least 1000.");
                continue;
            }

            return salary;
        }
    }
}