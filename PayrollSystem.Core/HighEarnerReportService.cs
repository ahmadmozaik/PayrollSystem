using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem;

/// <summary>
/// Writes high-earning employee information to a text report.
/// </summary>
public sealed class HighEarnerReportService
{
    /// <summary>
    /// Saves employees meeting a salary threshold to a text file.
    /// </summary>
    /// <param name="filePath">The report file path.</param>
    /// <param name="employees">The high-earning employees.</param>
    /// <param name="threshold">The report's minimum salary.</param>
    public void Save(
        string filePath,
        IEnumerable<FullTimeEmployee> employees,
        decimal threshold)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(employees);

        using FileStream fileStream = new FileStream(
            filePath,
            FileMode.Create,
            FileAccess.Write);

        using StreamWriter streamWriter = new StreamWriter(fileStream);

        streamWriter.WriteLine("High-Earner Payroll Report");
        streamWriter.WriteLine(
            $"Minimum salary: {threshold.ToCurrencyString()}");
        streamWriter.WriteLine();

        foreach (FullTimeEmployee employee in employees)
        {
            streamWriter.WriteLine(
                $"{employee.Id} | " +
                $"{employee.Name} | " +
                $"{employee.Role} | " +
                $"{employee.BaseSalary.ToCurrencyString()}");
        }
    }
}