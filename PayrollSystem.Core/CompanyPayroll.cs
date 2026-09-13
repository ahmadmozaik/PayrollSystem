using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    public class CompanyPayroll
    {
        private readonly Repository<FullTimeEmployee> employees;
        private readonly Func<Employee, decimal> bonusCalculator;

        public CompanyPayroll(Repository<FullTimeEmployee> employees, Func<Employee, decimal> bonusCalculator)
        {
            ArgumentNullException.ThrowIfNull(employees);
            ArgumentNullException.ThrowIfNull(bonusCalculator);
            this.employees = employees;
            this.bonusCalculator = bonusCalculator;
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

                decimal bonus = bonusCalculator(employee);
                decimal grossSalary = employee.BaseSalary + bonus;
                decimal tax = grossSalary * Employee.TAX_RATE;
                decimal netSalary = grossSalary - tax;

                Money payment;
                payment.Amount = netSalary;
                payment.Currency = "TRY";

                employee.ProcessPayment(payment);

                OnSalaryProcessed?.Invoke($"Salary processed for {employee.Name}: {payment.Amount} {payment.Currency}");
            }
        }
    }   
}
