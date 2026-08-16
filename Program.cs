using PayrollSystem;
class Program
{
    static void Main(string[] args)
    {
        //Test the FullTimeEmployee class
        FullTimeEmployee employee = new FullTimeEmployee();

        employee.Name = "Ahmad";
        employee.Role = EmployeeRole.Developer;
        employee.BaseSalary = 5000;

        Console.WriteLine(employee.Name);
        Console.WriteLine(employee.Role);
        Console.WriteLine(employee.BaseSalary);
    }
}