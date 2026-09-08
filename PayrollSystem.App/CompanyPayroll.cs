using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    public class CompanyPayroll
    {
        private FullTimeEmployee[] employees;

        public CompanyPayroll(int size)
        {
            employees = new FullTimeEmployee[size];
        }

        public FullTimeEmployee this[int index]
        {
            get {return employees[index];}
            set {employees[index] = value;}
        }
        public event PayrollHandler OnSalaryProcessed;

        public void RunPayroll()
        {
            foreach (FullTimeEmployee employee in employees)
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
