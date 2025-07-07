namespace NetEvolve.Extensions.Data;

using System.Data;
using NetEvolve.Arguments;

/// <summary>
/// Provides extension methods for <see cref="IDataReader"/> to enhance data reader operations.
/// </summary>
public static class IDataReaderExtensions
{
    /// <summary>
    /// Determines whether the data reader contains a column with the specified name.
    /// </summary>
    /// <param name="reader">The data reader to search for the column.</param>
    /// <param name="name">The name of the column to locate.</param>
    /// <returns><see langword="true"/> if the column exists; otherwise, <see langword="false"/>.</returns>
    public static bool HasColumn(this IDataReader reader, string name)
    {
        Argument.ThrowIfNull(reader);
        Argument.ThrowIfNullOrWhiteSpace(name);

        return Enumerable
            .Range(0, reader.FieldCount)
            .Any(i => reader.GetName(i).Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}
