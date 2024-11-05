namespace NetEvolve.Extensions.Data;

using System.Data;
using NetEvolve.Arguments;

/// <summary>
/// Provides a set of extension methods for <see cref="IDataReader"/>.
/// </summary>
public static class IDataReaderExtensions
{
    /// <summary>
    /// Determines whether the <see cref="IDataReader"/> contains a column with the specified name.
    /// </summary>
    /// <param name="reader">The <see cref="IDataReader"/> to check.</param>
    /// <param name="name">The name of the column to find.</param>
    /// <returns><see langword="true"/> if the <see cref="IDataReader"/> contains a column with the specified name; otherwise, <see langword="false"/>.</returns>
    public static bool HasColumn(this IDataReader reader, string name)
    {
        Argument.ThrowIfNull(reader);
        Argument.ThrowIfNullOrWhiteSpace(name);

        return Enumerable
            .Range(0, reader.FieldCount)
            .Any(i => reader.GetName(i).Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}
