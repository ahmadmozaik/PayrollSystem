using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    public struct Money
    {
        public decimal Amount;
        public string Currency;

        public static Money operator +(Money first, Money second)
        {
            if (first.Currency != second.Currency)
            {
                throw new InvalidOperationException("Currencies must be the same.");
            }

            Money result;

            result.Amount = first.Amount + second.Amount;
            result.Currency = first.Currency;

            return result;
        }
    }
}