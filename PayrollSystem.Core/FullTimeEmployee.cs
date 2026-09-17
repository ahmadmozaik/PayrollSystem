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
        /// <returns>A task representing the payment operation.</returns>
        [AuditTrail("Process employee payment asynchronously", "PayrollSystem")]
        public async Task ProcessPaymentAsync(Money amount)
        {
            Console.WriteLine($"Sending payment of {amount.Amount} {amount.Currency} to the payment gateway...");
            await Task.Delay(1000); // Simulate async operation
            Console.WriteLine($"Payment of {amount.Amount} {amount.Currency} completed for {Name}");
        }
    }
}
