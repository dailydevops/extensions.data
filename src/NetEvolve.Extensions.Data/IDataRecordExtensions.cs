namespace NetEvolve.Extensions.Data;

using System;
using System.Data;
using NetEvolve.Arguments;

/// <summary>
/// Provides a set of static methods for querying objects that implement <see cref="IDataRecord"/>.
/// </summary>
public static class IDataRecordExtensions
{
    /// <summary>
    /// Gets the value of the specified column as a <see cref="bool"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static bool? GetNullableBoolean(
        this IDataRecord record,
        int i,
        bool? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetBoolean(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="bool"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static bool? GetNullableBoolean(
        this IDataRecord record,
        string name,
        bool? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetBoolean(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="byte"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static byte? GetNullableByte(this IDataRecord record, int i, byte? defaultValue = null)
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetByte(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="byte"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static byte? GetNullableByte(
        this IDataRecord record,
        string name,
        byte? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetByte(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="char"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static char? GetNullableChar(this IDataRecord record, int i, char? defaultValue = null)
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetChar(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="char"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static char? GetNullableChar(
        this IDataRecord record,
        string name,
        char? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetChar(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="DateTime"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static DateTime? GetNullableDateTime(
        this IDataRecord record,
        int i,
        DateTime? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetDateTime(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="DateTime"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static DateTime? GetNullableDateTime(
        this IDataRecord record,
        string name,
        DateTime? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetDateTime(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="decimal"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static decimal? GetNullableDecimal(
        this IDataRecord record,
        int i,
        decimal? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetDecimal(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="decimal"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static decimal? GetNullableDecimal(
        this IDataRecord record,
        string name,
        decimal? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetDecimal(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="double"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static double? GetNullableDouble(
        this IDataRecord record,
        int i,
        double? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetDouble(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="double"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static double? GetNullableDouble(
        this IDataRecord record,
        string name,
        double? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetDouble(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="float"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static float? GetNullableFloat(
        this IDataRecord record,
        int i,
        float? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetFloat(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="float"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static float? GetNullableFloat(
        this IDataRecord record,
        string name,
        float? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetFloat(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="Guid"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static Guid? GetNullableGuid(this IDataRecord record, int i, Guid? defaultValue = null)
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetGuid(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="Guid"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static Guid? GetNullableGuid(
        this IDataRecord record,
        string name,
        Guid? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetGuid(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="short"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static short? GetNullableInt16(
        this IDataRecord record,
        int i,
        short? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetInt16(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="short"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static short? GetNullableInt16(
        this IDataRecord record,
        string name,
        short? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetInt16(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="int"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static int? GetNullableInt32(this IDataRecord record, int i, int? defaultValue = null)
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetInt32(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="int"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static int? GetNullableInt32(
        this IDataRecord record,
        string name,
        int? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetInt32(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="long"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static long? GetNullableInt64(this IDataRecord record, int i, long? defaultValue = null)
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetInt64(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="long"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static long? GetNullableInt64(
        this IDataRecord record,
        string name,
        long? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetInt64(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="string"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static string? GetNullableString(
        this IDataRecord record,
        int i,
        string? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetString(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="string"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static string? GetNullableString(
        this IDataRecord record,
        string name,
        string? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetString(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="object"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="i">The zero-based column ordinal.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="IDataRecord.FieldCount"/>.</exception>
    public static object? GetNullableValue(
        this IDataRecord record,
        int i,
        object? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);

        return record.IsDBNull(i) ? defaultValue : record.GetValue(i);
    }

    /// <summary>
    /// Gets the value of the specified column as a <see cref="object"/> or <paramref name="defaultValue"/> if the database value is <see cref="DBNull.Value"/>.
    /// </summary>
    /// <param name="record">The <see cref="IDataRecord"/> instance.</param>
    /// <param name="name">The name of the column.</param>
    /// <param name="defaultValue">The default value to return if the database value is <see cref="DBNull.Value"/>.</param>
    /// <returns>The value of the specified column.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="record"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="name"/> is empty or whitespace.</exception>
    /// <exception cref="IndexOutOfRangeException">The column name passed was not found.</exception>
    public static object? GetNullableValue(
        this IDataRecord record,
        string name,
        object? defaultValue = null
    )
    {
        Argument.ThrowIfNull(record);
        Argument.ThrowIfNullOrWhiteSpace(name);

        var i = record.GetOrdinal(name);
        return record.IsDBNull(i) ? defaultValue : record.GetValue(i);
    }
}
