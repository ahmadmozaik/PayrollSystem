using PayrollSystem;
class Program
{
    static void Main(string[] args)
    {
        //EmployeeRole Class Test
        EmployeeRole role = EmployeeRole.Developer;
        Console.WriteLine(role);

        //Money struct Test
        Money firstMoney;
        firstMoney.Amount = 1500;
        firstMoney.Currency = "TRY";
        Money secondMoney;
        secondMoney.Amount = 500;
        secondMoney.Currency = "TRY";
        Money total = firstMoney + secondMoney;
        Console.WriteLine($"Total: {total.Amount} {total.Currency}");
        
        Console.ReadLine();
    }
}