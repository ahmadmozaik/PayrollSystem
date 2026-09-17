namespace PayrollSystem
{
    /// <summary>
    /// Stores and retrieves employee records, providing a simple in-memory repository for payroll processing.
    /// </summary>
    /// <typeparam name="T">
    /// The employee type to store in the repository, constrained to be a subclass of Employee.
    /// </typeparam>
    public class Repository<T> where T : Employee
    {
        private readonly List<T> items = new List<T>();

        /// <summary>
        /// Adds an employee record to the repository, ensuring that the item is not null.
        /// </summary>
        /// <param name="item">
        /// The employee record to add to the repository.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="item"/> is null.
        /// </exception>
        public void Add(T item) 
        {
            ArgumentNullException.ThrowIfNull(item);
            items.Add(item);
        }

        /// <summary>
        /// Retrieves all employee records from the repository.
        /// </summary>
        /// <returns>
        /// A read-only list of all employee records.
        /// </returns>
        public IReadOnlyList<T> GetAll()
        {
            return items.AsReadOnly();
        }

        /// <summary>
        /// Lazily returns employees whose base salary is greater than or equal to the specified threshold.
        /// </summary>
        /// <param name="threshold">The minimum base salary for included employees.</param>
        /// <returns>
        /// Employees whose base salary is greater than or equal to the threshold.
        /// </returns>
        public IEnumerable<T> GetHighEarners(decimal threshold)
        {
            foreach (T employee in items)
            {
                if (employee.BaseSalary >= threshold)
                {
                    yield return employee;
                }
            }
        }
    }
}
