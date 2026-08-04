namespace NetEvolve.Extensions.Data.Tests.Unit;

using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using global::TUnit.Mocks;
using global::TUnit.Mocks.Arguments;

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
        var reader = Mock.Of<DbDataReader>().Object;
        string name = null!;

        _ = Assert.Throws<ArgumentNullException>("name", () => reader.GetFieldValue<string>(name));
    }

    [Test]
    public void GetFieldValue_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Mock.Of<DbDataReader>().Object;
        var name = string.Empty;

        _ = Assert.Throws<ArgumentException>("name", () => reader.GetFieldValue<string>(name));
    }

    [Test]
    public void GetFieldValue_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Mock.Of<DbDataReader>().Object;
        var name = "   ";

        _ = Assert.Throws<ArgumentException>("name", () => reader.GetFieldValue<string>(name));
    }

    [Test]
    public async Task GetFieldValue_String_WhenValidName_ReturnsValue()
    {
        var mock = Mock.Of<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        _ = mock.GetOrdinal(name).Returns(ordinal);
        _ = mock.GetFieldValue<string>(ordinal).Returns(expectedValue);

        var result = mock.Object.GetFieldValue<string>(name);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueStringData))]
    public async Task GetFieldValue_String_Theory_Expected(object? expected, string columnName, object? fieldValue)
    {
        var mock = Mock.Of<DbDataReader>();
        var ordinal = 0;
        _ = mock.GetOrdinal(columnName).Returns(ordinal);
        _ = mock.GetFieldValue<object>(ordinal).Returns(fieldValue);

        var result = mock.Object.GetFieldValue<object>(columnName);

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
        var reader = Mock.Of<DbDataReader>().Object;
        string name = null!;

        _ = await Assert.ThrowsAsync<ArgumentNullException>("name", () => reader.GetFieldValueAsync<string>(name));
    }

    [Test]
    public async Task GetFieldValueAsync_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Mock.Of<DbDataReader>().Object;
        var name = string.Empty;

        _ = await Assert.ThrowsAsync<ArgumentException>("name", () => reader.GetFieldValueAsync<string>(name));
    }

    [Test]
    public async Task GetFieldValueAsync_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Mock.Of<DbDataReader>().Object;
        var name = "   ";

        _ = await Assert.ThrowsAsync<ArgumentException>("name", () => reader.GetFieldValueAsync<string>(name));
    }

    [Test]
    public async Task GetFieldValueAsync_String_WhenValidName_ReturnsValue()
    {
        var mock = Mock.Of<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        _ = mock.GetOrdinal(name).Returns(ordinal);
        _ = mock.GetFieldValueAsync<string>(ordinal, Arg.Any<CancellationToken>()).Returns(expectedValue);

        var result = await mock.Object.GetFieldValueAsync<string>(name).ConfigureAwait(false);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    public async Task GetFieldValueAsync_String_WithCancellationToken_WhenValidName_ReturnsValue()
    {
        var mock = Mock.Of<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        var cancellationToken = new CancellationToken();
        _ = mock.GetOrdinal(name).Returns(ordinal);
        _ = mock.GetFieldValueAsync<string>(ordinal, cancellationToken).Returns(expectedValue);

        var result = await mock.Object.GetFieldValueAsync<string>(name, cancellationToken).ConfigureAwait(false);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueAsyncStringData))]
    public async Task GetFieldValueAsync_String_Theory_Expected(object? expected, string columnName, object? fieldValue)
    {
        var mock = Mock.Of<DbDataReader>();
        var ordinal = 0;
        _ = mock.GetOrdinal(columnName).Returns(ordinal);
        _ = mock.GetFieldValueAsync<object>(ordinal, Arg.Any<CancellationToken>()).Returns(fieldValue);

        var result = await mock.Object.GetFieldValueAsync<object>(columnName).ConfigureAwait(false);

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
        var mock = Mock.Of<DbDataReader>();
        var index = 0;
        _ = mock.IsDBNull(index).Returns(true);

        var result = mock.Object.GetFieldValueOrDefault<string>(index);

        Assert.Null(result);
    }

    [Test]
    public async Task GetFieldValueOrDefault_Int_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var mock = Mock.Of<DbDataReader>();
        var index = 0;
        var expectedValue = "test";
        _ = mock.IsDBNull(index).Returns(false);
        _ = mock.GetFieldValue<string>(index).Returns(expectedValue);

        var result = mock.Object.GetFieldValueOrDefault<string>(index);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueOrDefaultIntData))]
    public async Task GetFieldValueOrDefault_Int_Theory_Expected(object? expected, bool isDBNull, object? fieldValue)
    {
        var mock = Mock.Of<DbDataReader>();
        var index = 0;
        _ = mock.IsDBNull(index).Returns(isDBNull);
        if (!isDBNull)
        {
            _ = mock.GetFieldValue<object>(index).Returns(fieldValue);
        }

        var result = mock.Object.GetFieldValueOrDefault<object>(index);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(object?, bool, object?)> GetFieldValueOrDefaultIntData =>
        [
            (null, true, "test"),
            ("test", false, "test"),
            (null, true, 42),
            (42, false, 42),
            (null, true, true),
            (true, false, true),
            (null, true, DateTime.Now),
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
        var reader = Mock.Of<DbDataReader>().Object;
        string name = null!;

        _ = Assert.Throws<ArgumentNullException>("name", () => reader.GetFieldValueOrDefault<string>(name));
    }

    [Test]
    public void GetFieldValueOrDefault_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Mock.Of<DbDataReader>().Object;
        var name = string.Empty;

        _ = Assert.Throws<ArgumentException>("name", () => reader.GetFieldValueOrDefault<string>(name));
    }

    [Test]
    public void GetFieldValueOrDefault_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Mock.Of<DbDataReader>().Object;
        var name = "   ";

        _ = Assert.Throws<ArgumentException>("name", () => reader.GetFieldValueOrDefault<string>(name));
    }

    [Test]
    public void GetFieldValueOrDefault_String_WhenColumnIsDBNull_ReturnsDefault()
    {
        var mock = Mock.Of<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        _ = mock.GetOrdinal(name).Returns(ordinal);
        _ = mock.IsDBNull(ordinal).Returns(true);

        var result = mock.Object.GetFieldValueOrDefault<string>(name);

        Assert.Null(result);
    }

    [Test]
    public async Task GetFieldValueOrDefault_String_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var mock = Mock.Of<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        _ = mock.GetOrdinal(name).Returns(ordinal);
        _ = mock.IsDBNull(ordinal).Returns(false);
        _ = mock.GetFieldValue<string>(ordinal).Returns(expectedValue);

        var result = mock.Object.GetFieldValueOrDefault<string>(name);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueOrDefaultStringData))]
    public async Task GetFieldValueOrDefault_String_Theory_Expected(
        object? expected,
        string columnName,
        bool isDBNull,
        object? fieldValue
    )
    {
        var mock = Mock.Of<DbDataReader>();
        var ordinal = 0;
        _ = mock.GetOrdinal(columnName).Returns(ordinal);
        _ = mock.IsDBNull(ordinal).Returns(isDBNull);
        if (!isDBNull)
        {
            _ = mock.GetFieldValue<object>(ordinal).Returns(fieldValue);
        }

        var result = mock.Object.GetFieldValueOrDefault<object>(columnName);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(object?, string, bool, object?)> GetFieldValueOrDefaultStringData =>
        [
            (null, "stringColumn", true, "test"),
            ("test", "stringColumn", false, "test"),
            (null, "intColumn", true, 42),
            (42, "intColumn", false, 42),
            (null, "boolColumn", true, true),
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
        var mock = Mock.Of<DbDataReader>();
        var index = 0;
        _ = mock.IsDBNullAsync(index, Arg.Any<CancellationToken>()).Returns(true);

        var result = await mock.Object.GetFieldValueOrDefaultAsync<string>(index).ConfigureAwait(false);

        Assert.Null(result);
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_Int_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var mock = Mock.Of<DbDataReader>();
        var index = 0;
        var expectedValue = "test";
        _ = mock.IsDBNullAsync(index, Arg.Any<CancellationToken>()).Returns(false);
        _ = mock.GetFieldValueAsync<string>(index, Arg.Any<CancellationToken>()).Returns(expectedValue);

        var result = await mock.Object.GetFieldValueOrDefaultAsync<string>(index).ConfigureAwait(false);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_Int_WithCancellationToken_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var mock = Mock.Of<DbDataReader>();
        var index = 0;
        var expectedValue = "test";
        var cancellationToken = new CancellationToken();
        _ = mock.IsDBNullAsync(index, cancellationToken).Returns(false);
        _ = mock.GetFieldValueAsync<string>(index, cancellationToken).Returns(expectedValue);

        var result = await mock
            .Object.GetFieldValueOrDefaultAsync<string>(index, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueOrDefaultAsyncIntData))]
    public async Task GetFieldValueOrDefaultAsync_Int_Theory_Expected(
        object? expected,
        bool isDBNull,
        object? fieldValue
    )
    {
        var mock = Mock.Of<DbDataReader>();
        var index = 0;
        _ = mock.IsDBNullAsync(index, Arg.Any<CancellationToken>()).Returns(isDBNull);
        if (!isDBNull)
        {
            _ = mock.GetFieldValueAsync<object>(index, Arg.Any<CancellationToken>()).Returns(fieldValue);
        }

        var result = await mock.Object.GetFieldValueOrDefaultAsync<object>(index).ConfigureAwait(false);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(object?, bool, object?)> GetFieldValueOrDefaultAsyncIntData =>
        [
            (null, true, "test"),
            ("test", false, "test"),
            (null, true, 42),
            (42, false, 42),
            (null, true, true),
            (true, false, true),
            (null, true, DateTime.Now),
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
        var reader = Mock.Of<DbDataReader>().Object;
        string name = null!;

        _ = await Assert.ThrowsAsync<ArgumentNullException>(
            "name",
            () => reader.GetFieldValueOrDefaultAsync<string>(name)
        );
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Mock.Of<DbDataReader>().Object;
        var name = string.Empty;

        _ = await Assert.ThrowsAsync<ArgumentException>("name", () => reader.GetFieldValueOrDefaultAsync<string>(name));
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Mock.Of<DbDataReader>().Object;
        var name = "   ";

        _ = await Assert.ThrowsAsync<ArgumentException>("name", () => reader.GetFieldValueOrDefaultAsync<string>(name));
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WhenColumnIsDBNull_ReturnsDefault()
    {
        var mock = Mock.Of<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        _ = mock.GetOrdinal(name).Returns(ordinal);
        _ = mock.IsDBNullAsync(ordinal, Arg.Any<CancellationToken>()).Returns(true);

        var result = await mock.Object.GetFieldValueOrDefaultAsync<string>(name).ConfigureAwait(false);

        Assert.Null(result);
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var mock = Mock.Of<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        _ = mock.GetOrdinal(name).Returns(ordinal);
        _ = mock.IsDBNullAsync(ordinal, Arg.Any<CancellationToken>()).Returns(false);
        _ = mock.GetFieldValueAsync<string>(ordinal, Arg.Any<CancellationToken>()).Returns(expectedValue);

        var result = await mock.Object.GetFieldValueOrDefaultAsync<string>(name).ConfigureAwait(false);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_String_WithCancellationToken_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var mock = Mock.Of<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        var cancellationToken = new CancellationToken();
        _ = mock.GetOrdinal(name).Returns(ordinal);
        _ = mock.IsDBNullAsync(ordinal, cancellationToken).Returns(false);
        _ = mock.GetFieldValueAsync<string>(ordinal, cancellationToken).Returns(expectedValue);

        var result = await mock
            .Object.GetFieldValueOrDefaultAsync<string>(name, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        _ = await Assert.That(result).IsEqualTo(expectedValue);
    }

    [Test]
    [MethodDataSource(nameof(GetFieldValueOrDefaultAsyncStringData))]
    public async Task GetFieldValueOrDefaultAsync_String_Theory_Expected(
        object? expected,
        string columnName,
        bool isDBNull,
        object? fieldValue
    )
    {
        var mock = Mock.Of<DbDataReader>();
        var ordinal = 0;
        _ = mock.GetOrdinal(columnName).Returns(ordinal);
        _ = mock.IsDBNullAsync(ordinal, Arg.Any<CancellationToken>()).Returns(isDBNull);
        if (!isDBNull)
        {
            _ = mock.GetFieldValueAsync<object>(ordinal, Arg.Any<CancellationToken>()).Returns(fieldValue);
        }

        var result = await mock.Object.GetFieldValueOrDefaultAsync<object>(columnName).ConfigureAwait(false);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(object?, string, bool, object?)> GetFieldValueOrDefaultAsyncStringData =>
        [
            (null, "stringColumn", true, "test"),
            ("test", "stringColumn", false, "test"),
            (null, "intColumn", true, 42),
            (42, "intColumn", false, 42),
            (null, "boolColumn", true, true),
            (true, "boolColumn", false, true),
        ];
}
