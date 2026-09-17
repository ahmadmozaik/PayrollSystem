using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem;

/// <summary>
/// Appends timestamped payroll activity to an audit log file.
/// </summary>
public sealed class AuditLogService
{
    private readonly string filePath;

    /// <summary>
    /// Gets the Istanbul time zone information for timestamping audit log entries.
    /// </summary>
    private static readonly TimeZoneInfo IstanbulTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Istanbul");

    /// <summary>
    /// Initializes an audit log that writes to the specified file.
    /// </summary>
    /// <param name="filePath">The audit log file path.</param>
    public AuditLogService(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        this.filePath = filePath;
    }

    /// <summary>
    /// Appends one timestamped message to the audit log.
    /// </summary>
    /// <param name="message">The payroll activity to record.</param>
    public void WriteEntry(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        DateTimeOffset istanbulTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, IstanbulTimeZone);

        using FileStream fileStream = new FileStream(
            filePath,
            FileMode.Append,
            FileAccess.Write);

        using StreamWriter streamWriter = new StreamWriter(fileStream);

        streamWriter.WriteLine(
            $"{istanbulTime:O} | {message}");
    }
}
