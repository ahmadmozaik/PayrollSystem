using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    internal class CompanyPayroll
    {
        private FullTimeEmployee[] employees;

        public CompanyPayroll(int size)
        {
            employees = new FullTimeEmployee[size];
        }

        public FullTimeEmployee this[int index]
        {
            get {return employees[index];}
            set {employees[index] = value;}
        }
    public event PayrollHandler OnSalaryProcessed;
    }   
}
