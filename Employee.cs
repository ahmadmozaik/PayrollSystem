using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    internal class Employee
    {
        public const decimal TAX_RATE = 0.10m; // 10% constant tax rate

        private decimal _baseSalary;

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
        public class ContactInfo
        {
            public string Email;
            public string Phone;
        }

        ~Employee()
        {
            Console.WriteLine("Employee object removed from memory.");
        }
    }
}
