using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    /// <summary>
    /// Defines a contract for processing payments to employees, ensuring that any implementing class provides a method to handle payment processing.
    /// </summary>
    public interface IPayable
    {
        /// <summary>
        /// Processes a payment for the employee.
        /// </summary>
        /// <param name="amount">The amount to be paid.</param>
        void ProcessPayment(Money amount);
    }
}
