using PayrollSystem;
class Program
{
    static void Main(string[] args)
    {
        //Testing the FullTimeEmployee class and its ProcessPayment method
        FullTimeEmployee employee = new FullTimeEmployee();

        employee.BaseSalary = 5000;

        Money salary;
        salary.Amount = 4500;
        salary.Currency = "TRY";

        employee.ProcessPayment(salary);
    }
}