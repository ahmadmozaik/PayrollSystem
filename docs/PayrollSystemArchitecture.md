# PayrollSystem V2 Architecture

The Core project contains the payroll domain, collections, persistence services, reporting, metadata, and asynchronous payment workflow. The App project provides startup configuration and console interaction.

```mermaid
classDiagram
    direction LR

    class EmployeeRole {
        <<enumeration>>
        Developer
        Manager
        Tester
    }

    class Money {
        <<struct>>
        +decimal Amount
        +string Currency
        +Add(Money other) Money
    }

    class IPayable {
        <<interface>>
        +ProcessPaymentAsync(Money amount) Task
    }

    class Employee {
        +int Id
        +string Name
        +EmployeeRole Role
        +decimal BaseSalary
        +ContactInfo Contact
        +decimal TAX_RATE
    }

    class ContactInfo {
        +string Email
        +string Phone
    }

    class FullTimeEmployee {
        +ProcessPaymentAsync(Money amount) Task
    }

    class Repository {
        <<generic>>
        -List items
        +Add(T item) void
        +GetAll() IReadOnlyList
        +GetHighEarners(decimal threshold) IEnumerable
    }

    class PaymentTransaction {
        +FullTimeEmployee Employee
        +Money Payment
    }

    class CompanyPayroll {
        -Repository employees
        -Queue pendingPayments
        -Stack operationHistory
        -Func bonusCalculator
        -Func deductionCalculator
        -Predicate employeeFilter
        +OnSalaryProcessed
        +RunPayroll() void
        +ProcessPayrollAsync() Task
        +ProcessPendingPaymentsAsync() Task
        +GetLatestOperation() string
    }

    class EmployeeJsonService {
        +Export(string filePath, IEnumerable employees) void
        +Import(string filePath) IReadOnlyList
    }

    class AuditLogService {
        -string filePath
        +WriteEntry(string message) void
    }

    class HighEarnerReportService {
        +Save(string filePath, IEnumerable employees, decimal threshold) void
    }

    class AuditTrailAttribute {
        +string OperationName
        +string Author
    }

    class DecimalExtensions {
        <<static>>
        +ToCurrencyString(decimal amount) string
    }

    class PayrollProcessingException {
        +PayrollProcessingException(string message)
        +PayrollProcessingException(string message, Exception innerException)
    }

    class Program {
        <<application>>
        +Main(string[] args) Task
        -DisplayAuditMetadata() void
        -CreateEmployeesFromInput() FullTimeEmployee[]
        -TryRegisterEmployee(...) bool
        -ReadValidSalary(string employeeName) decimal
    }

    Employee <|-- FullTimeEmployee
    IPayable <|.. FullTimeEmployee

    Employee --> EmployeeRole : has role
    Employee *-- ContactInfo : owns
    FullTimeEmployee --> Money : processes

    Repository o-- Employee : stores
    PaymentTransaction --> FullTimeEmployee : employee
    PaymentTransaction --> Money : payment

    CompanyPayroll --> Repository : reads employees
    CompanyPayroll o-- PaymentTransaction : queues
    CompanyPayroll --> AuditTrailAttribute : metadata

    Program --> CompanyPayroll : coordinates
    Program --> EmployeeJsonService : persists
    Program --> AuditLogService : subscribes
    Program --> HighEarnerReportService : creates reports
    Program --> Repository : registers employees

    EmployeeJsonService --> FullTimeEmployee : serializes
    HighEarnerReportService --> FullTimeEmployee : reports
    DecimalExtensions ..> Money : formats amounts
    Money ..> PayrollProcessingException : throws
```
