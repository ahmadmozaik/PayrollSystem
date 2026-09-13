using PayrollSystem;

class Program
{
    static void Main(string[] args)
    {
        Repository<FullTimeEmployee> repository = new Repository<FullTimeEmployee>();

        Dictionary<int, Employee> employeesById = new Dictionary<int, Employee>();

        Func<Employee, decimal> bonusCalculator = employee =>  // Calculates a bonus based on employee role
        {
            if (employee.Role == EmployeeRole.Developer)
            {
                return employee.BaseSalary * 0.1m; // 10% bonus for developers
            }
            else if (employee.Role == EmployeeRole.Tester)
            {
                return employee.BaseSalary * 0.05m; // 5% bonus for testers
            }
            else
            {
                return 0m; // No bonus for other roles
            }
        };

        Func<Employee, decimal> deductionCalculator = employee =>  // Calculates deductions based on employee role
        {
            if (employee.Role == EmployeeRole.Developer)
            {
                return employee.BaseSalary * 0.05m; // 5% deduction for developers
            }
            else if (employee.Role == EmployeeRole.Tester)
            {
                return employee.BaseSalary * 0.02m; // 2% deduction for testers
            }
            else
            {
                return 0m; // No deduction for other roles
            }
        };

        Predicate<Employee> employeeFilter = employee => employee.Role == EmployeeRole.Developer;

        CompanyPayroll payroll = new CompanyPayroll(repository, bonusCalculator, deductionCalculator, employeeFilter);

        FullTimeEmployee employee1 = new FullTimeEmployee();
        employee1.Id = 1;
        employee1.Name = "Ahmad";
        employee1.Role = EmployeeRole.Developer;
        employee1.BaseSalary = ReadValidSalary(employee1.Name);

        FullTimeEmployee employee2 = new FullTimeEmployee();
        employee2.Id = 2;
        employee2.Name = "Sara";
        employee2.Role = EmployeeRole.Tester;
        employee2.BaseSalary = ReadValidSalary(employee2.Name);

        repository.Add(employee1);
        employeesById.Add(employee1.Id, employee1);
        repository.Add(employee2);
        employeesById.Add(employee2.Id, employee2);
        
        if (employeesById.TryGetValue(1, out Employee? foundEmployee))
        {
            Console.WriteLine($"Lookup found: {foundEmployee.Name}");
        }

        payroll.OnSalaryProcessed += ShowNotification;

        payroll.RunPayroll();
        payroll.ProcessPendingPayments();
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