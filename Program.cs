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
        Console.Write("Enter Ahmad's base salary: ");

        if (decimal.TryParse(Console.ReadLine(), out decimal salary1))
        {
            try
            {
                employee1.BaseSalary = salary1;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Invalid salary input.");
        }

        FullTimeEmployee employee2 = new FullTimeEmployee();
        employee2.Name = "Sara";
        employee2.Role = EmployeeRole.Tester;
        Console.Write("Enter Sara's base salary: ");

        if (decimal.TryParse(Console.ReadLine(), out decimal salary2))
        {
            try
            {
                employee2.BaseSalary = salary2;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Invalid salary input.");
        }

        payroll[0] = employee1;
        payroll[1] = employee2;

        payroll.OnSalaryProcessed += ShowNotification;

        payroll.RunPayroll();
    }
    static void ShowNotification(string message)
    {
        Console.WriteLine($"Notification: {message}");
    }
}