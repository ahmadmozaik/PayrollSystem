namespace PayrollSystem
{
    /// <summary>
    /// Represents a payment transaction for an employee, including the employee and the payment details.
    /// </summary>
    public class PaymentTransaction
    {
        /// <summary>
        /// Gets the employee associated with this payment transaction.
        /// </summary>
        public FullTimeEmployee Employee { get; }
        /// <summary>
        /// Gets the payment details for this transaction.
        /// </summary>
        public Money Payment { get; }

        /// <summary>
        /// Initializes a new instance of the PaymentTransaction class with the specified employee and payment details.
        /// </summary>
        /// <param name="employee">The employee for whom the payment is being made.</param>
        /// <param name="payment">The payment details.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="employee"/> is null.
        /// </exception>
        public PaymentTransaction(FullTimeEmployee employee, Money payment)
        {
            ArgumentNullException.ThrowIfNull(employee);
            Employee = employee;
            Payment = payment;
        }
    }
}
