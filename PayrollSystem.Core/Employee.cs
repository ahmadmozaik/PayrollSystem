using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    /// <summary>
    /// Represents an employee in the payroll system, including their name, role, base salary, and contact information.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the employee's unique identifier.
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public EmployeeRole Role { get; set; }

        public const decimal TAX_RATE = 0.10m; // 10% constant tax rate

        private decimal _baseSalary;

        /// <summary>
        /// Gets or sets the base salary of the employee.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the value is less than 1000.
        /// </exception>
        public decimal BaseSalary
        {
            get { return _baseSalary; }
            set
            {
                if (value < 1000)
                {
                    throw new ArgumentOutOfRangeException(nameof(BaseSalary), "Base salary cannot be less than 1000.");
                }
                _baseSalary = value;
            }
        }
        /// <summary>
        /// Gets the employee's contact information, including email and phone number.
        /// </summary>
        public ContactInfo Contact { get; } = new ContactInfo();
        public class ContactInfo
        {
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
        }

        ~Employee()
        {
            Console.WriteLine("Employee object removed from memory.");
        }
    }
}
