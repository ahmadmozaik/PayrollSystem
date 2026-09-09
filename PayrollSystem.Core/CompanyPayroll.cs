using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    public class CompanyPayroll
    {
        private readonly Repository<FullTimeEmployee> employees;

        public CompanyPayroll(Repository<FullTimeEmployee> employees)
        {
            ArgumentNullException.ThrowIfNull(employees);
            this.employees = employees;
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
                
                decimal tax = employee.BaseSalary * Employee.TAX_RATE;
                decimal netSalary = employee.BaseSalary - tax;

                Money payment;
                payment.Amount = netSalary;
                payment.Currency = "TRY";

                employee.ProcessPayment(payment);

                OnSalaryProcessed?.Invoke($"Salary processed for {employee.Name}: {payment.Amount} {payment.Currency}");
            }
        }
    }   
}
