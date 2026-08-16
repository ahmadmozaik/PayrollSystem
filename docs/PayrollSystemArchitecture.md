classDiagram

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
    +operator +(Money, Money) Money
}

class IPayable {
    <<interface>>
    +ProcessPayment(Money amount) void
}

class Employee {
    +string Name
    +EmployeeRole Role
    +decimal TAX_RATE
    -decimal _baseSalary
    +decimal BaseSalary
    +~Employee()
}

class ContactInfo {
    +string Email
    +string Phone
}

class FullTimeEmployee {
    +ProcessPayment(Money amount) void
}

class PayrollHandler {
    <<delegate>>
    +Invoke(string message) void
}

class CompanyPayroll {
    -FullTimeEmployee[] employees
    +CompanyPayroll(int size)
    +this[int index] FullTimeEmployee
    +PayrollHandler OnSalaryProcessed
    +RunPayroll() void
}

class Program {
    +Main(string[] args) void
    -ShowNotification(string message) void
    -ReadValidSalary(string employeeName) decimal
}

Employee <|-- FullTimeEmployee
IPayable <|.. FullTimeEmployee

Employee --> EmployeeRole : has role
Employee *-- ContactInfo : nested type

FullTimeEmployee --> Money : processes

CompanyPayroll --> FullTimeEmployee : stores
CompanyPayroll --> PayrollHandler : event
CompanyPayroll --> Money : creates payment

Program --> CompanyPayroll : creates
Program --> FullTimeEmployee : creates
Program --> EmployeeRole : assigns