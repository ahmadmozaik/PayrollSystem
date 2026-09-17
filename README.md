# PayrollSystem V2

PayrollSystem is a C# console application that demonstrates an enterprise-style payroll workflow using object-oriented programming, generic collections, delegates, events, serialization, streams, reflection, and asynchronous processing.

The original V1 implementation remains available on the `master` branch. V2 development is maintained on the `v2-enterprise-upgrade` branch.

## Features

### Employee Management

- Full-time employees with unique IDs
- Employee roles using `EmployeeRole`
- Nested contact information
- Minimum salary validation
- Case-insensitive duplicate-email validation using `HashSet`
- Employee lookup using `Dictionary`
- Automatic salary ordering using `SortedSet`
- Generic employee repository

### Payroll Processing

- Configurable bonus and deduction rules using `Func`
- Employee selection using `Predicate`
- Constant tax calculation
- FIFO payment processing using `Queue`
- Operation history using `Stack`
- Lazy high-earner filtering using `yield return`
- Custom `Money` value type and currency validation
- Custom `PayrollProcessingException`

### Async Processing and Events

- Async payment processing with `Task` and `await`
- Simulated payment gateway delay
- Background payroll execution with `Task.Run`
- Salary-processed event notifications
- Multiple event subscribers for console and audit logging

### Persistence and Reporting

- Employee export and import using Newtonsoft.Json
- JSON roles stored as readable enum names
- File access through `FileStream`, `StreamReader`, and `StreamWriter`
- Timestamped audit logs using Istanbul time
- High-earner text report
- Manual employee creation when no JSON file exists

### Metadata and Formatting

- `ToCurrencyString()` decimal extension method
- Custom `AuditTrailAttribute`
- Reflection-based audit metadata discovery at startup

## Project Structure

```text
PayrollSystem
|-- PayrollSystem.App
|   `-- Program.cs
|-- PayrollSystem.Core
|   |-- Employee.cs
|   |-- FullTimeEmployee.cs
|   |-- CompanyPayroll.cs
|   |-- Repository.cs
|   |-- EmployeeJsonService.cs
|   |-- AuditLogService.cs
|   |-- HighEarnerReportService.cs
|   `-- supporting domain types
`-- docs
    |-- V1_DevelopmentPlan.md
    |-- V2_DevelopmentPlan.md
    |-- PayrollSystemArchitecture.md
    `-- PayrollFlow.md
```

## Application Flow

1. Display audit metadata using reflection.
2. Load employees from JSON when saved data exists.
3. Otherwise, create five employees from user input and export them.
4. Validate unique emails and register employees in the collections.
5. Display employees ordered by salary.
6. Filter and save the high-earner report.
7. Calculate and queue payroll transactions.
8. Process payments asynchronously in FIFO order.
9. Notify event subscribers and append audit-log entries.
10. Display the latest payroll operation.

## Runtime Files

The application creates these files inside its output `Data` directory:

- `employees.json` — persisted employee records
- `payroll-audit.log` — timestamped payment notifications
- `high-earner-report.txt` — employees meeting the salary threshold

## Running the Application

1. Open `PayrollSystem.sln` in Visual Studio.
2. Set `PayrollSystem.App` as the startup project.
3. Run the application.

On the first run, enter a valid salary of at least `1000` for each employee. Later runs load the saved employees from JSON automatically.

## Documentation

- [V2 Development Plan](docs/V2_DevelopmentPlan.md)
- [V1 Development Plan](docs/V1_DevelopmentPlan.md)
- [Architecture Diagram](docs/PayrollSystemArchitecture.md)
- [Payroll Flow](docs/PayrollFlow.md)

## Technology

- C#
- .NET 10
- Newtonsoft.Json
- Visual Studio
- Git and GitHub
