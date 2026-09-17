using PayrollSystem;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        DisplayAuditMetadata();
        Console.WriteLine();

        Repository<FullTimeEmployee> repository = new Repository<FullTimeEmployee>();

        EmployeeJsonService employeeJsonService = new EmployeeJsonService();

        Dictionary<int, Employee> employeesById = new Dictionary<int, Employee>();

        HashSet<string> employeeEmails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        Comparer<Employee> employeeSalaryComparer =
            Comparer<Employee>.Create((first, second) =>
            {
                int salaryComparison =
                    first.BaseSalary.CompareTo(second.BaseSalary);

                if (salaryComparison != 0)
                {
                    return salaryComparison;
                }

                return first.Id.CompareTo(second.Id);
            });

        SortedSet<Employee> employeesBySalary = new SortedSet<Employee>(employeeSalaryComparer);

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
        employee1.Contact.Email = "ahmad@example.com";
        employee1.Contact.Phone = "123-456-7890";
        employee1.Role = EmployeeRole.Developer;
        employee1.BaseSalary = ReadValidSalary(employee1.Name);

        FullTimeEmployee employee2 = new FullTimeEmployee();
        employee2.Id = 2;
        employee2.Name = "Sara";
        employee2.Contact.Email = "sara@example.com";
        employee2.Contact.Phone = "098-765-4321";
        employee2.Role = EmployeeRole.Tester;
        employee2.BaseSalary = ReadValidSalary(employee2.Name);

        if (!employeeEmails.Add(employee1.Contact.Email))
        {
            Console.WriteLine($"Duplicate email detected: {employee1.Contact.Email}");
            return;
        }

        if (!employeeEmails.Add(employee2.Contact.Email))
        {
            Console.WriteLine($"Duplicate email detected: {employee2.Contact.Email}");
            return;
        }

        repository.Add(employee1);
        employeesById.Add(employee1.Id, employee1);
        employeesBySalary.Add(employee1);

        repository.Add(employee2);
        employeesById.Add(employee2.Id, employee2);
        employeesBySalary.Add(employee2);

        string dataDirectory = Path.Combine(AppContext.BaseDirectory, "Data");

        Directory.CreateDirectory(dataDirectory);

        string employeeFilePath = Path.Combine(dataDirectory, "employees.json");
        string auditLogFilePath = Path.Combine(dataDirectory, "payroll-audit.log");

        AuditLogService auditLogService = new AuditLogService(auditLogFilePath);

        employeeJsonService.Export(
            employeeFilePath,
            repository.GetAll());

        IReadOnlyList<FullTimeEmployee> importedEmployees = employeeJsonService.Import(employeeFilePath);

        Console.WriteLine(
            $"Exported and imported {importedEmployees.Count} employees:");

        foreach (FullTimeEmployee importedEmployee in importedEmployees)
        {
            Console.WriteLine(
                $" - {importedEmployee.Name}, " +
                $"{importedEmployee.Role}, " +
                $"{importedEmployee.Contact.Email}");
        }

        Console.WriteLine($"JSON file: {employeeFilePath}");
        Console.WriteLine();

        Console.WriteLine("Employees sorted by base salary:");
        foreach (Employee employee in employeesBySalary)
        {
            Console.WriteLine($" - {employee.Name}: {employee.BaseSalary.ToCurrencyString()}");
        }

        decimal highEarnerThreshold = 1500m;

        Console.WriteLine($"Employees earning at least {highEarnerThreshold.ToCurrencyString()}:");

        foreach (FullTimeEmployee employee in repository.GetHighEarners(highEarnerThreshold))
        {
            Console.WriteLine($" - {employee.Name}: {employee.BaseSalary.ToCurrencyString()}");
        }

        if (employeesById.TryGetValue(1, out Employee? foundEmployee))
        {
            Console.WriteLine($"Lookup found: {foundEmployee.Name}");
        }

        payroll.OnSalaryProcessed += ShowNotification;
        payroll.OnSalaryProcessed += auditLogService.WriteEntry;

        payroll.RunPayroll();
        payroll.ProcessPendingPayments();

        Console.WriteLine($"Audit log: {auditLogFilePath}");

        string? latestOperation = payroll.GetLatestOperation();

        if (latestOperation != null)
        {
            Console.WriteLine($"Latest operation: {latestOperation}");
        }
    }

    static void DisplayAuditMetadata()
    {
        Console.WriteLine("Audit Payroll Operations:");

        Assembly payrollAssembly = typeof(CompanyPayroll).Assembly;

        foreach (Type type in payrollAssembly.GetTypes())
        {
            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in methods)
            {
                AuditTrailAttribute? auditTrail =
                    method.GetCustomAttribute<AuditTrailAttribute>();

                if (auditTrail != null)
                {
                    Console.WriteLine(
                        $" - {auditTrail.OperationName} by {auditTrail.Author}");
                }
            }
        }
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