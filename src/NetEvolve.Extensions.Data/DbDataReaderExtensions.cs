namespace NetEvolve.Extensions.Data;

using System.Data.Common;
using NetEvolve.Arguments;

/// <summary>
/// Provides extension methods for <see cref="DbDataReader"/> to safely retrieve field values with default fallback.
/// </summary>
public static class DbDataReaderExtensions
{
    /// <summary>
    /// Gets the value of the specified column as the requested type.
    /// </summary>
    /// <typeparam name="T">The type of the value to be returned.</typeparam>
    /// <param name="reader">The <see cref="DbDataReader"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <returns>The value of the column as the specified type.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the column name specified in <paramref name="name"/> does not exist.</exception>
    /// <exception cref="InvalidCastException">Thrown if the column value cannot be cast to the specified type <typeparamref name="T"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the column contains a <see langword="null"/> value and the specified type <typeparamref name="T"/> is not nullable.</exception>
    public static T GetFieldValue<T>(this DbDataReader reader, string name)
    {
        Argument.ThrowIfNull(reader);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var ordinal = reader.GetOrdinal(name);
        return reader.GetFieldValue<T>(ordinal);
    }

    /// <summary>
    /// Asynchronously gets the value of the specified column as the requested type.
    /// </summary>
    /// <typeparam name="T">The type of the value to be returned.</typeparam>
    /// <param name="reader">The <see cref="DbDataReader"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the value of the column as the specified type.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the column name specified in <paramref name="name"/> does not exist.</exception>
    /// <exception cref="InvalidCastException">Thrown if the column value cannot be cast to the specified type <typeparamref name="T"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the column contains a <see langword="null"/> value and the specified type <typeparamref name="T"/> is not nullable.</exception>
    /// <exception cref="OperationCanceledException">Thrown if the <paramref name="cancellationToken"/> is canceled.</exception>
    public static async Task<T> GetFieldValueAsync<T>(
        this DbDataReader reader,
        string name,
        CancellationToken cancellationToken = default
    )
    {
        Argument.ThrowIfNull(reader);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var ordinal = reader.GetOrdinal(name);
        return await reader.GetFieldValueAsync<T>(ordinal, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the value of the specified column as the requested type, or the default value of the type if the column is <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to be returned.</typeparam>
    /// <param name="reader">The <see cref="DbDataReader"/> instance.</param>
    /// <param name="ordinal">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The value to return if the column is <see langword="null"/>. The default value is <see langword="default"/>(<typeparamref name="T"/>).</param>
    /// <returns>The value of the column if it is not <see langword="null"/>; otherwise, the default value of <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the column ordinal specified in <paramref name="ordinal"/> is out of range.</exception>
    /// <exception cref="InvalidCastException">Thrown if the column value cannot be cast to the specified type <typeparamref name="T"/>.</exception>
    public static T GetFieldValueOrDefault<T>(this DbDataReader reader, int ordinal, T defaultValue = default!)
    {
        Argument.ThrowIfNull(reader);

        if (reader.IsDBNull(ordinal))
        {
            return defaultValue;
        }

        return reader.GetFieldValue<T>(ordinal);
    }

    /// <summary>
    /// Gets the value of the specified column as the requested type, or the default value of the type if the column is <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to be returned.</typeparam>
    /// <param name="reader">The <see cref="DbDataReader"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The value to return if the column is <see langword="null"/>. The default value is <see langword="default"/>(<typeparamref name="T"/>).</param>
    /// <returns>The value of the column if it is not <see langword="null"/>; otherwise, the default value of <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the column name specified in <paramref name="name"/> does not exist.</exception>
    /// <exception cref="InvalidCastException">Thrown if the column value cannot be cast to the specified type <typeparamref name="T"/>.</exception>
    public static T GetFieldValueOrDefault<T>(this DbDataReader reader, string name, T defaultValue = default!)
    {
        Argument.ThrowIfNull(reader);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var ordinal = reader.GetOrdinal(name);

        if (reader.IsDBNull(ordinal))
        {
            return defaultValue;
        }

        return reader.GetFieldValue<T>(ordinal);
    }

    /// <summary>
    /// Asynchronously gets the value of the specified column as the requested type, or the default value of the type if the column is <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to be returned.</typeparam>
    /// <param name="reader">The <see cref="DbDataReader"/> instance.</param>
    /// <param name="ordinal">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The value to return if the column is <see langword="null"/>. The default value is <see langword="default"/>(<typeparamref name="T"/>).</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the value of the column if it is not <see langword="null"/>; otherwise, the default value of <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the column ordinal specified in <paramref name="ordinal"/> is out of range.</exception>
    /// <exception cref="InvalidCastException">Thrown if the column value cannot be cast to the specified type <typeparamref name="T"/>.</exception>
    /// <exception cref="OperationCanceledException">Thrown if the <paramref name="cancellationToken"/> is canceled.</exception>
    public static async Task<T> GetFieldValueOrDefaultAsync<T>(
        this DbDataReader reader,
        int ordinal,
        T defaultValue = default!,
        CancellationToken cancellationToken = default
    )
    {
        Argument.ThrowIfNull(reader);

        if (await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false))
        {
            return defaultValue;
        }

        return await reader.GetFieldValueAsync<T>(ordinal, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously gets the value of the specified column as the requested type, or the default value of the type if the column is <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to be returned.</typeparam>
    /// <param name="reader">The <see cref="DbDataReader"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The value to return if the column is <see langword="null"/>. The default value is <see langword="default"/>(<typeparamref name="T"/>).</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the value of the column if it is not <see langword="null"/>; otherwise, the default value of <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the column name specified in <paramref name="name"/> does not exist.</exception>
    /// <exception cref="InvalidCastException">Thrown if the column value cannot be cast to the specified type <typeparamref name="T"/>.</exception>
    /// <exception cref="OperationCanceledException">Thrown if the <paramref name="cancellationToken"/> is canceled.</exception>
    public static async Task<T> GetFieldValueOrDefaultAsync<T>(
        this DbDataReader reader,
        string name,
        T defaultValue = default!,
        CancellationToken cancellationToken = default
    )
    {
        Argument.ThrowIfNull(reader);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var ordinal = reader.GetOrdinal(name);

        if (await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false))
        {
            return defaultValue;
        }

        return await reader.GetFieldValueAsync<T>(ordinal, cancellationToken).ConfigureAwait(false);
    }
}
