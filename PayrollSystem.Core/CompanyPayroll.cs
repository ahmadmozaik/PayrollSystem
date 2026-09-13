using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    public class CompanyPayroll
    {
        private readonly Repository<FullTimeEmployee> employees;
        private readonly Func<Employee, decimal> bonusCalculator;
        private readonly Func<Employee, decimal> deductionCalculator;
        private readonly Predicate<Employee> employeeFilter;

        public CompanyPayroll(Repository<FullTimeEmployee> employees, Func<Employee, decimal> bonusCalculator, Func<Employee, decimal> deductionCalculator, Predicate<Employee> employeeFilter)
        {
            ArgumentNullException.ThrowIfNull(employees);
            ArgumentNullException.ThrowIfNull(bonusCalculator);
            ArgumentNullException.ThrowIfNull(deductionCalculator);
            ArgumentNullException.ThrowIfNull(employeeFilter);
            this.employees = employees;
            this.bonusCalculator = bonusCalculator;
            this.deductionCalculator = deductionCalculator;
            this.employeeFilter = employeeFilter;
        }

        public event Action<string>? OnSalaryProcessed;

        public void RunPayroll()
        {
            foreach (FullTimeEmployee employee in employees.GetAll())
            {
                if (employee == null)
                {
                    continue;
                }

                if (!employeeFilter(employee))
                {
                    continue;
                }

                decimal bonus = bonusCalculator(employee);
                decimal grossSalary = employee.BaseSalary + bonus;
                decimal tax = grossSalary * Employee.TAX_RATE;
                decimal netSalary = grossSalary - tax;
                decimal deductions = deductionCalculator(employee);
                netSalary -= deductions;

                Money payment;
                payment.Amount = netSalary;
                payment.Currency = "TRY";

                employee.ProcessPayment(payment);

                OnSalaryProcessed?.Invoke($"Salary processed for {employee.Name}: {payment.Amount} {payment.Currency}");
            }
        }
    }   
}
