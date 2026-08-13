using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    public interface IPayable
    {
        //to be defined later in FullTimeEmployee as it implements the ProcessPayment method from the IPayable interface
        void ProcessPayment(Money amount);
    }
}
