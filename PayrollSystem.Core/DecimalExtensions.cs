namespace PayrollSystem
{
    /// <summary>
    /// Provides formatting extensions for decimal monetary values.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// Formats a decimal value as TRY currency.
        /// </summary>
        /// <param name="amount">The monetary amount to format.</param>
        /// <returns>
        /// The amount formatted with two decimal places and the "TRY" currency code.
        /// </returns>
        public static string ToCurrencyString(this decimal amount)
        {
            return $"{amount:N2} TRY";
        }
    }
}
