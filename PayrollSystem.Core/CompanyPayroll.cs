using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    /// <summary>
    /// Coordinates payroll processing using configurable bonus, deduction, and employee-filtering rules.
    /// </summary>
    public class CompanyPayroll
    {
        private readonly Repository<FullTimeEmployee> employees;
        private readonly Func<Employee, decimal> bonusCalculator;
        private readonly Func<Employee, decimal> deductionCalculator;
        private readonly Predicate<Employee> employeeFilter;
        private readonly Queue<PaymentTransaction> pendingPayments = new Queue<PaymentTransaction>();

        /// <summary>
        /// Initializes payroll processing with its employee source and calculation rules.
        /// </summary>
        /// <param name="employees">The repository containing employees to process.</param>
        /// <param name="bonusCalculator">The rule used to calculate employee bonuses.</param>
        /// <param name="deductionCalculator">The rule used to calculate employee deductions.</param>
        /// <param name="employeeFilter">The rule used to select employees for processing.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when any constructor argument is null.
        /// </exception>
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

        /// <summary>
        /// Occurs after an employee's salary has been processed successfully, providing a notification message.
        /// </summary>
        public event Action<string>? OnSalaryProcessed;

        /// <summary>
        /// Calculates payroll for employees that satisfy the configured filter,
        /// and adds their payment transactions to the pending queue for processing.
        /// </summary>
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

                pendingPayments.Enqueue(new PaymentTransaction(employee, payment));
            }
        }

        /// <summary>
        /// Processes queued payments in FIFO order.
        /// </summary>
        public void ProcessPendingPayments()
        {
            while (pendingPayments.Count > 0)
            {
                PaymentTransaction transaction = pendingPayments.Dequeue();
                transaction.Employee.ProcessPayment(transaction.Payment);

                OnSalaryProcessed?.Invoke($"Salary processed for {transaction.Employee.Name}: {transaction.Payment.Amount} {transaction.Payment.Currency}");
            }
        }
    }   
}
