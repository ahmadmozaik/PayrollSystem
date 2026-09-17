using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    /// <summary>
    /// Represents a full-time employee in the payroll system, extending the Employee class and implementing the IPayable interface for payment processing.
    /// </summary>
    public class FullTimeEmployee : Employee, IPayable
    {
        /// <summary>
        /// Processes a payment for the full-time employee.
        /// </summary>
        /// <param name="amount">The amount to be paid.</param>
        [AuditTrail("Process employee payment", "PayrollSystem")]
        public void ProcessPayment(Money amount)
        {
            Console.WriteLine($"Processing payment of {amount.Amount} {amount.Currency} for full-time employee.");
        }
    }
}
