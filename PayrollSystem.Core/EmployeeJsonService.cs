using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PayrollSystem;

/// <summary>
/// Saves and loads full-time employee data in JSON format.
/// </summary>
public sealed class EmployeeJsonService
{
    /// <summary>
    /// Exports employees to a JSON file.
    /// </summary>
    /// <param name="filePath">The destination JSON file path.</param>
    /// <param name="employees">The employees to export.</param>
    public void Export(
        string filePath,
        IEnumerable<FullTimeEmployee> employees)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(employees);

        using FileStream fileStream = new FileStream(
            filePath,
            FileMode.Create,
            FileAccess.Write);

        using StreamWriter streamWriter = new StreamWriter(fileStream);

        JsonSerializer serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented
        };

        serializer.Converters.Add(new StringEnumConverter());

        serializer.Serialize(streamWriter, employees);
    }

    /// <summary>
    /// Imports full-time employees from a JSON file.
    /// </summary>
    /// <param name="filePath">The source JSON file path.</param>
    /// <returns>
    /// The employees loaded from the file, or an empty list when the JSON
    /// contains no employee collection.
    /// </returns>
    public IReadOnlyList<FullTimeEmployee> Import(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        using FileStream fileStream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read);

        using StreamReader streamReader = new StreamReader(fileStream);
        using JsonTextReader jsonReader = new JsonTextReader(streamReader);

        JsonSerializer serializer = new JsonSerializer();

        serializer.Converters.Add(new StringEnumConverter());

        List<FullTimeEmployee>? employees =
            serializer.Deserialize<List<FullTimeEmployee>>(jsonReader);

        return employees ?? new List<FullTimeEmployee>();
    }
}