using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem
{
    /// <summary>
    /// Marks a payroll operation method for audit information.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AuditTrailAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the audited operation.
        /// </summary>
        public string OperationName { get; }

        /// <summary>
        /// Gets the author of the audited operation.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Initializes audit information for a payroll operation.
        /// </summary>
        /// <param name="operationName">The readable operation name.</param>
        /// <param name="author">The author responsible for the operation.</param>
        public AuditTrailAttribute(string operationName, string author)
        {
            OperationName = operationName;
            Author = author;
        }
    }
}
