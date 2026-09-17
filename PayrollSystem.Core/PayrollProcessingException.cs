namespace PayrollSystem
{
    /// <summary>
    /// Represents an error that occurs during payroll processing.
    /// </summary>
    public class PayrollProcessingException : Exception
    {
        /// <summary>
        /// Initializes a new instance with the specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the payroll-processing error.
        /// </param>
        public PayrollProcessingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes an instance with a message and the original exception.
        /// </summary>
        /// <param name="message">
        /// The payroll-processing error message.
        /// </param>
        /// <param name="innerException">
        /// The exception that caused this error.
        /// </param>
        public PayrollProcessingException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }
    }
}