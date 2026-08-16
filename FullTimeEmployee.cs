using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    public class FullTimeEmployee : Employee, IPayable
    {
        public void ProcessPayment(Money amount)
        {
            // Implement the payment processing logic here
            Console.WriteLine($"Processing payment of {amount.Amount} {amount.Currency} for full-time employee.");
        }
    }
}
