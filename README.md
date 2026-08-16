# PayrollSystem

A simple C# Console Application for managing employees, validating salaries, processing payroll after tax deductions, and issuing payment notifications.

## Features

- Employee roles using `enum`
- Salary representation using a `Money` struct
- Operator overloading for money addition
- Employee inheritance and `IPayable` interface
- Base salary validation
- 10% tax calculation
- Payroll processing using arrays and indexers
- Delegates and events for salary notifications
- Safe user input using `decimal.TryParse`
- Reusable salary validation using `ReadValidSalary`
- Exception handling

## Documentation

The project was planned and implemented step by step, with separate Git commits for the main requirements.

The `docs` folder contains:

- `DevelopmentPlan.md` — implementation plan
- `Architecture.md` — class structure and relationships
- `PayrollFlow.md` — payroll processing flowchart

## Technology

- C#
- .NET Console Application
- Visual Studio
- Git & GitHub