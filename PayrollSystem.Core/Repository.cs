using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    public class Repository<T> where T : Employee
    {
        private readonly List<T> items = new List<T>();

        public void Add(T item) 
        {
            ArgumentNullException.ThrowIfNull(item);
            items.Add(item);
        }

        public IReadOnlyList<T> GetAll()
        {
            return items.AsReadOnly();
        }
    }
}
