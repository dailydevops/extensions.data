namespace NetEvolve.Extensions.Data.Tests.Unit;

using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using NSubstitute;

[SuppressMessage("Performance", "CA1849:Call async methods when in an async method", Justification = "As designed.")]
[SuppressMessage("Major Code Smell", "S6966:Awaitable method should be used", Justification = "As designed.")]
[SuppressMessage("Usage", "VSTHRD103:Call async methods when in an async method", Justification = "As designed.")]
public class DbDataReaderExtensionsTests
{
    [Test]
    public void GetFieldValue_String_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var name = "column";

        _ = Assert.Throws<ArgumentNullException>("reader", () => reader.GetFieldValue<string>(name));
    }

    [Test]
    public void GetFieldValue_String_WhenNameIsNull_ThrowsArgumentNullException()
    {
        var reader = Substitute.For<DbDataReader>();
        string name = null!;

        _ = Assert.Throws<ArgumentNullException>("name", () => reader.GetFieldValue<string>(name));
    }

    [Test]
    public void GetFieldValue_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = string.Empty;

        _ = Assert.Throws<ArgumentException>("name", () => reader.GetFieldValue<string>(name));
    }

    [Test]
    public void GetFieldValue_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "   ";

        _ = Assert.Throws<ArgumentException>("name", () => reader.GetFieldValue<string>(name));
    }

    [Test]
    public async Task GetFieldValue_String_WhenValidName_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        _ = reader.GetOrdinal(name).Returns(ordinal);
        _ = reader.GetFieldValue<string>(ordinal).Returns(expectedValue);

        var result = reader.GetFieldValue<string>(name);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueStringData))]
    public async Task GetFieldValue_String_Theory_Expected<T>(T expected, string columnName, T fieldValue)
    {
        var reader = Substitute.For<DbDataReader>();
        var ordinal = 0;
        _ = reader.GetOrdinal(columnName).Returns(ordinal);
        _ = reader.GetFieldValue<T>(ordinal).Returns(fieldValue);

        var result = reader.GetFieldValue<T>(columnName);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(object?, string, object?)> GetFieldValueStringData =>
        [
            ("test", "stringColumn", "test"),
            (42, "intColumn", 42),
            (true, "boolColumn", true),
            (
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture),
                "dateColumn",
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture)
            ),
        ];

    [Test]
    public async Task GetFieldValueAsync_String_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var name = "column";

        _ = await Assert.ThrowsAsync<ArgumentNullException>("reader", () => reader.GetFieldValueAsync<string>(name));
    }

    [Test]
    public async Task GetFieldValueAsync_String_WhenNameIsNull_ThrowsArgumentNullException()
    {
        var reader = Substitute.For<DbDataReader>();
        string name = null!;

        _ = await Assert.ThrowsAsync<ArgumentNullException>("name", () => reader.GetFieldValueAsync<string>(name));
    }

    [Test]
    public async Task GetFieldValueAsync_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = string.Empty;

        _ = await Assert.ThrowsAsync<ArgumentException>("name", () => reader.GetFieldValueAsync<string>(name));
    }

    [Test]
    public async Task GetFieldValueAsync_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "   ";

        _ = await Assert.ThrowsAsync<ArgumentException>("name", () => reader.GetFieldValueAsync<string>(name));
    }

    [Test]
    public async Task GetFieldValueAsync_String_WhenValidName_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        _ = reader.GetOrdinal(name).Returns(ordinal);
        _ = reader
            .GetFieldValueAsync<string>(ordinal, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(expectedValue));

        var result = await reader.GetFieldValueAsync<string>(name);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    public async Task GetFieldValueAsync_String_WithCancellationToken_WhenValidName_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        var cancellationToken = new CancellationToken();
        _ = reader.GetOrdinal(name).Returns(ordinal);
        _ = reader.GetFieldValueAsync<string>(ordinal, cancellationToken).Returns(Task.FromResult(expectedValue));

        var result = await reader.GetFieldValueAsync<string>(name, cancellationToken);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueAsyncStringData))]
    public async Task GetFieldValueAsync_String_Theory_Expected<T>(T expected, string columnName, T fieldValue)
    {
        var reader = Substitute.For<DbDataReader>();
        var ordinal = 0;
        _ = reader.GetOrdinal(columnName).Returns(ordinal);
        _ = reader.GetFieldValueAsync<T>(ordinal, Arg.Any<CancellationToken>()).Returns(Task.FromResult(fieldValue));

        var result = await reader.GetFieldValueAsync<T>(columnName);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(object?, string, object?)> GetFieldValueAsyncStringData =>
        [
            ("test", "stringColumn", "test"),
            (42, "intColumn", 42),
            (true, "boolColumn", true),
            (
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture),
                "dateColumn",
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture)
            ),
        ];

    [Test]
    public void GetFieldValueOrDefault_Int_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var index = 0;

        _ = Assert.Throws<ArgumentNullException>("reader", () => reader.GetFieldValueOrDefault<string>(index));
    }

    [Test]
    public void GetFieldValueOrDefault_Int_WhenColumnIsDBNull_ReturnsDefault()
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        _ = reader.IsDBNull(index).Returns(true);

        var result = reader.GetFieldValueOrDefault<string>(index);

        Assert.Null(result);
    }

    [Test]
    public async Task GetFieldValueOrDefault_Int_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        var expectedValue = "test";
        _ = reader.IsDBNull(index).Returns(false);
        _ = reader.GetFieldValue<string>(index).Returns(expectedValue);

        var result = reader.GetFieldValueOrDefault<string>(index);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueOrDefaultIntData))]
    public async Task GetFieldValueOrDefault_Int_Theory_Expected<T>(T expected, bool isDBNull, T fieldValue)
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        _ = reader.IsDBNull(index).Returns(isDBNull);
        if (!isDBNull)
        {
            _ = reader.GetFieldValue<T>(index).Returns(fieldValue);
        }

        var result = reader.GetFieldValueOrDefault<T>(index);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(object?, bool, object?)> GetFieldValueOrDefaultIntData =>
        [
            (null, true, "test"),
            ("test", false, "test"),
            (0, true, 42),
            (42, false, 42),
            (false, true, true),
            (true, false, true),
            (default(DateTime), true, DateTime.Now),
            (
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture),
                false,
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture)
            ),
        ];

    [Test]
    public void GetFieldValueOrDefault_String_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var name = "column";

        _ = Assert.Throws<ArgumentNullException>("reader", () => reader.GetFieldValueOrDefault<string>(name));
    }

    [Test]
    public void GetFieldValueOrDefault_String_WhenNameIsNull_ThrowsArgumentNullException()
    {
        var reader = Substitute.For<DbDataReader>();
        string name = null!;

        _ = Assert.Throws<ArgumentNullException>("name", () => reader.GetFieldValueOrDefault<string>(name));
    }

    [Test]
    public void GetFieldValueOrDefault_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = string.Empty;

        _ = Assert.Throws<ArgumentException>("name", () => reader.GetFieldValueOrDefault<string>(name));
    }

    [Test]
    public void GetFieldValueOrDefault_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "   ";

        _ = Assert.Throws<ArgumentException>("name", () => reader.GetFieldValueOrDefault<string>(name));
    }

    [Test]
    public void GetFieldValueOrDefault_String_WhenColumnIsDBNull_ReturnsDefault()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        _ = reader.GetOrdinal(name).Returns(ordinal);
        _ = reader.IsDBNull(ordinal).Returns(true);

        var result = reader.GetFieldValueOrDefault<string>(name);

        Assert.Null(result);
    }

    [Test]
    public async Task GetFieldValueOrDefault_String_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        _ = reader.GetOrdinal(name).Returns(ordinal);
        _ = reader.IsDBNull(ordinal).Returns(false);
        _ = reader.GetFieldValue<string>(ordinal).Returns(expectedValue);

        var result = reader.GetFieldValueOrDefault<string>(name);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueOrDefaultStringData))]
    public async Task GetFieldValueOrDefault_String_Theory_Expected<T>(
        T expected,
        string columnName,
        bool isDBNull,
        T fieldValue
    )
    {
        var reader = Substitute.For<DbDataReader>();
        var ordinal = 0;
        _ = reader.GetOrdinal(columnName).Returns(ordinal);
        _ = reader.IsDBNull(ordinal).Returns(isDBNull);
        if (!isDBNull)
        {
            _ = reader.GetFieldValue<T>(ordinal).Returns(fieldValue);
        }

        var result = reader.GetFieldValueOrDefault<T>(columnName);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(object?, string, bool, object?)> GetFieldValueOrDefaultStringData =>
        [
            (null, "stringColumn", true, "test"),
            ("test", "stringColumn", false, "test"),
            (0, "intColumn", true, 42),
            (42, "intColumn", false, 42),
            (false, "boolColumn", true, true),
            (true, "boolColumn", false, true),
        ];

    [Test]
    public async Task GetFieldValueOrDefaultAsync_Int_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var index = 0;

        _ = await Assert.ThrowsAsync<ArgumentNullException>(
            "reader",
            () => reader.GetFieldValueOrDefaultAsync<string>(index)
        );
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_Int_WhenColumnIsDBNull_ReturnsDefault()
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        _ = reader.IsDBNullAsync(index).Returns(Task.FromResult(true));

        var result = await reader.GetFieldValueOrDefaultAsync<string>(index);

        Assert.Null(result);
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_Int_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        var expectedValue = "test";
        _ = reader.IsDBNullAsync(index).Returns(Task.FromResult(false));
        _ = reader.GetFieldValueAsync<string>(index).Returns(Task.FromResult(expectedValue));

        var result = await reader.GetFieldValueOrDefaultAsync<string>(index);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_Int_WithCancellationToken_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        var expectedValue = "test";
        var cancellationToken = new CancellationToken();
        _ = reader.IsDBNullAsync(index, cancellationToken).Returns(Task.FromResult(false));
        _ = reader.GetFieldValueAsync<string>(index, cancellationToken).Returns(Task.FromResult(expectedValue));

        var result = await reader.GetFieldValueOrDefaultAsync<string>(index, cancellationToken: cancellationToken);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueOrDefaultAsyncIntData))]
    public async Task GetFieldValueOrDefaultAsync_Int_Theory_Expected<T>(T expected, bool isDBNull, T fieldValue)
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        _ = reader.IsDBNullAsync(index).Returns(Task.FromResult(isDBNull));
        if (!isDBNull)
        {
            _ = reader.GetFieldValueAsync<T>(index).Returns(Task.FromResult(fieldValue));
        }

        var result = await reader.GetFieldValueOrDefaultAsync<T>(index);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(object?, bool, object?)> GetFieldValueOrDefaultAsyncIntData =>
        [
            (null, true, "test"),
            ("test", false, "test"),
            (0, true, 42),
            (42, false, 42),
            (false, true, true),
            (true, false, true),
            (default(DateTime), true, DateTime.Now),
            (
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture),
                false,
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture)
            ),
        ];

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var name = "column";

        _ = await Assert.ThrowsAsync<ArgumentNullException>(
            "reader",
            () => reader.GetFieldValueOrDefaultAsync<string>(name)
        );
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WhenNameIsNull_ThrowsArgumentNullException()
    {
        var reader = Substitute.For<DbDataReader>();
        string name = null!;

        _ = await Assert.ThrowsAsync<ArgumentNullException>(
            "name",
            () => reader.GetFieldValueOrDefaultAsync<string>(name)
        );
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = string.Empty;

        _ = await Assert.ThrowsAsync<ArgumentException>("name", () => reader.GetFieldValueOrDefaultAsync<string>(name));
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "   ";

        _ = await Assert.ThrowsAsync<ArgumentException>("name", () => reader.GetFieldValueOrDefaultAsync<string>(name));
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WhenColumnIsDBNull_ReturnsDefault()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        _ = reader.GetOrdinal(name).Returns(ordinal);
        _ = reader.IsDBNullAsync(ordinal).Returns(Task.FromResult(true));

        var result = await reader.GetFieldValueOrDefaultAsync<string>(name);

        Assert.Null(result);
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        _ = reader.GetOrdinal(name).Returns(ordinal);
        _ = reader.IsDBNullAsync(ordinal).Returns(Task.FromResult(false));
        _ = reader.GetFieldValueAsync<string>(ordinal).Returns(Task.FromResult(expectedValue));

        var result = await reader.GetFieldValueOrDefaultAsync<string>(name);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WithCancellationToken_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        var cancellationToken = new CancellationToken();
        _ = reader.GetOrdinal(name).Returns(ordinal);
        _ = reader.IsDBNullAsync(ordinal, cancellationToken).Returns(Task.FromResult(false));
        _ = reader.GetFieldValueAsync<string>(ordinal, cancellationToken).Returns(Task.FromResult(expectedValue));

        var result = await reader.GetFieldValueOrDefaultAsync<string>(name, cancellationToken: cancellationToken);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueOrDefaultAsyncStringData))]
    public async Task GetFieldValueOrDefaultAsync_String_Theory_Expected<T>(
        T expected,
        string columnName,
        bool isDBNull,
        T fieldValue
    )
    {
        var reader = Substitute.For<DbDataReader>();
        var ordinal = 0;
        _ = reader.GetOrdinal(columnName).Returns(ordinal);
        _ = reader.IsDBNullAsync(ordinal).Returns(Task.FromResult(isDBNull));
        if (!isDBNull)
        {
            _ = reader.GetFieldValueAsync<T>(ordinal).Returns(Task.FromResult(fieldValue));
        }

        var result = await reader.GetFieldValueOrDefaultAsync<T>(columnName);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(object?, string, bool, object?)> GetFieldValueOrDefaultAsyncStringData =>
        [
            (null, "stringColumn", true, "test"),
            ("test", "stringColumn", false, "test"),
            (0, "intColumn", true, 42),
            (42, "intColumn", false, 42),
            (false, "boolColumn", true, true),
            (true, "boolColumn", false, true),
        ];
}
