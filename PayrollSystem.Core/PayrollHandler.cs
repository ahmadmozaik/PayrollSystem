using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    public delegate void PayrollHandler(string message);//To be used for OnSalaryProcessed event in the Payroll class
}
