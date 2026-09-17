using PayrollSystem;
using System.Reflection;

class Program
{
    static async Task Main(string[] args)
    {
        DisplayAuditMetadata();
        Console.WriteLine();

        Repository<FullTimeEmployee> repository = new Repository<FullTimeEmployee>();

        EmployeeJsonService employeeJsonService = new EmployeeJsonService();

        string dataDirectory = Path.Combine(AppContext.BaseDirectory, "Data");

        Directory.CreateDirectory(dataDirectory);

        string employeeFilePath = Path.Combine(dataDirectory, "employees.json");

        string auditLogFilePath = Path.Combine(dataDirectory, "payroll-audit.log");

        string highEarnerReportFilePath = Path.Combine(dataDirectory, "high-earner-report.txt");

        AuditLogService auditLogService = new AuditLogService(auditLogFilePath);

        HighEarnerReportService highEarnerReportService = new HighEarnerReportService();

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

        Predicate<Employee> employeeFilter = employee => true; // Include all employees

        CompanyPayroll payroll = new CompanyPayroll(repository, bonusCalculator, deductionCalculator, employeeFilter);

        IReadOnlyList<FullTimeEmployee> employees;

        if (File.Exists(employeeFilePath))
        {
            try
            {
                employees = employeeJsonService.Import(employeeFilePath);
            }
            catch (PayrollProcessingException exception)
            {
                Console.WriteLine(
                    $"Employees could not be loaded: {exception.Message}");

                return;
            }

            Console.WriteLine(
                $"Loaded {employees.Count} employees from JSON.");
        }
        else
        {
            employees = CreateEmployeesFromInput();

            employeeJsonService.Export(
                employeeFilePath,
                employees);

            Console.WriteLine(
                $"Created and exported {employees.Count} employees.");
        }

        foreach (FullTimeEmployee employee in employees)
        {
            if (!TryRegisterEmployee(employee, repository, employeesById, employeeEmails, employeesBySalary))
            {
                return;
            }
        }

        Console.WriteLine("Employees available for payroll:");

        foreach (FullTimeEmployee employee in repository.GetAll())
        {
            Console.WriteLine(
                $" - {employee.Name}, " +
                $"{employee.Role}, " +
                $"{employee.Contact.Email}");
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

        highEarnerReportService.Save(
            highEarnerReportFilePath,
            repository.GetHighEarners(highEarnerThreshold),
            highEarnerThreshold);

        Console.WriteLine(
            $"High-earner report saved to: {highEarnerReportFilePath}");

        if (employeesById.TryGetValue(1, out Employee? foundEmployee))
        {
            Console.WriteLine($"Lookup found: {foundEmployee.Name}");
        }

        payroll.OnSalaryProcessed += ShowNotification;
        payroll.OnSalaryProcessed += auditLogService.WriteEntry;

        Task payrollProcessingTask = Task.Run(payroll.ProcessPayrollAsync);

        Console.WriteLine("Payroll processing started in the background.");

        Console.WriteLine($"The application remains responsive with {repository.GetAll().Count} employees available.");

        await payrollProcessingTask;

        Console.WriteLine("Background payroll processing completed.");

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

    static FullTimeEmployee[] CreateEmployeesFromInput()
    {
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

        FullTimeEmployee employee3 = new FullTimeEmployee();
        employee3.Id = 3;
        employee3.Name = "John";
        employee3.Contact.Email = "john@example.com";
        employee3.Contact.Phone = "555-1234";
        employee3.Role = EmployeeRole.Manager;
        employee3.BaseSalary = ReadValidSalary(employee3.Name);

        FullTimeEmployee employee4 = new FullTimeEmployee();
        employee4.Id = 4;
        employee4.Name = "Alice";
        employee4.Contact.Email = "alice@example.com";
        employee4.Contact.Phone = "555-5678";
        employee4.Role = EmployeeRole.Developer;
        employee4.BaseSalary = ReadValidSalary(employee4.Name);

        FullTimeEmployee employee5 = new FullTimeEmployee();
        employee5.Id = 5;
        employee5.Name = "Bob";
        employee5.Contact.Email = "bob@example.com";
        employee5.Contact.Phone = "555-9012";
        employee5.Role = EmployeeRole.Tester;
        employee5.BaseSalary = ReadValidSalary(employee5.Name);

        return new FullTimeEmployee[]
        {
        employee1,
        employee2,
        employee3,
        employee4,
        employee5
        };
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

    static bool TryRegisterEmployee(
        FullTimeEmployee employee,
        Repository<FullTimeEmployee> repository,
        Dictionary<int, Employee> employeesById,
        HashSet<string> employeeEmails,
        SortedSet<Employee> employeesBySalary)
    {
        if (!employeeEmails.Add(employee.Contact.Email))
        {
            Console.WriteLine(
                $"Duplicate email detected: {employee.Contact.Email}");
            return false;
        }
        repository.Add(employee);
        employeesById.Add(employee.Id, employee);
        employeesBySalary.Add(employee);
        return true;
    }
}