using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    /// <summary>
    /// Represents a monetary value with an amount and currency, providing functionality for arithmetic operations while ensuring currency consistency.
    /// </summary>
    public struct Money
    {
        public decimal Amount;
        public string Currency;

        /// <summary>
        /// Adds two monetary values together, ensuring they have the same currency.
        /// </summary>
        /// <param name="first">The first monetary value.</param>
        /// <param name="second">The second monetary value.</param>
        /// <returns>A new Money value representing the sum</returns>
        /// <exception cref="PayrollProcessingException">
        /// Thrown when the two Money values have different currencies.
        /// </exception>
        public static Money operator +(Money first, Money second)
        {
            if (first.Currency != second.Currency)
            {
                throw new PayrollProcessingException("Cannot combine payments with different currencies.");
            }

            Money result;

            result.Amount = first.Amount + second.Amount;
            result.Currency = first.Currency;

            return result;
        }
    }
}