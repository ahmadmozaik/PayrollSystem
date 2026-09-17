# PayrollSystem V1

PayrollSystem V1 is a C# console application for managing employees, validating salaries, calculating payroll after tax, processing payments, and issuing salary notifications.

## Features

- Employee roles using `EmployeeRole`
- Salary representation using a `Money` struct
- Operator overloading for money addition
- Currency validation with a custom exception
- Employee inheritance and the `IPayable` interface
- Nested employee contact information
- Minimum base-salary validation
- Constant 10% tax calculation
- Employee storage using an array and indexer
- Payroll notifications using a custom delegate and event
- Safe salary input using `decimal.TryParse`
- Reusable input validation
- Basic exception handling

## Application Flow

1. Create the payroll collection.
2. Create two full-time employees.
3. Read and validate their salaries.
4. Add the employees through the payroll indexer.
5. Subscribe to salary notifications.
6. Calculate tax and net salary.
7. Process each payment.
8. Raise the salary-processed event.

## Documentation

- [Development Plan](docs/DevelopmentPlan.md)
- [Architecture Diagram](docs/PayrollSystemArchitecture.md)
- [Payroll Flow](docs/PayrollFlow.md)

## Technology

- C#
- .NET Console Application
- Visual Studio
- Git and GitHub

## Version History

This branch preserves the original V1 implementation. PayrollSystem V2 is developed separately on the `v2-enterprise-upgrade` branch.
