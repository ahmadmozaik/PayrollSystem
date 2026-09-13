namespace PayrollSystem
{
    /// <summary>
    /// Represents an error that occurs during payroll or money processing, such as when attempting to combine payments with different currencies.
    /// </summary>
    public class PayrollProcessingException : Exception
    {
        /// <summary>
        /// Initializes a new instance with the specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the payroll-processing error.
        /// </param>
        public PayrollProcessingException(string message) : base(message)
        {

        }
    }
}