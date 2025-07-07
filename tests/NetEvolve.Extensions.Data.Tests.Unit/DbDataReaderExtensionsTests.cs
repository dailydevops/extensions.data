namespace NetEvolve.Extensions.Data.Tests.Unit;

using System.Data.Common;
using System.Globalization;
using NSubstitute;

public class DbDataReaderExtensionsTests
{
    [Fact]
    public void GetFieldValue_String_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var name = "column";

        var exception = Assert.Throws<ArgumentNullException>(() => reader.GetFieldValue<string>(name));

        Assert.Equal("reader", exception.ParamName);
    }

    [Fact]
    public void GetFieldValue_String_WhenNameIsNull_ThrowsArgumentNullException()
    {
        var reader = Substitute.For<DbDataReader>();
        string name = null!;

        var exception = Assert.Throws<ArgumentNullException>(() => reader.GetFieldValue<string>(name));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetFieldValue_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = string.Empty;

        var exception = Assert.Throws<ArgumentException>(() => reader.GetFieldValue<string>(name));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetFieldValue_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "   ";

        var exception = Assert.Throws<ArgumentException>(() => reader.GetFieldValue<string>(name));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetFieldValue_String_WhenValidName_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        _ = reader.GetOrdinal(name).Returns(ordinal);
        _ = reader.GetFieldValue<string>(ordinal).Returns(expectedValue);

        var result = reader.GetFieldValue<string>(name);

        Assert.Equal(expectedValue, result);
    }

    [Theory]
    [MemberData(nameof(GetFieldValueStringData))]
    public void GetFieldValue_String_Theory_Expected<T>(T expected, string columnName, T fieldValue)
    {
        var reader = Substitute.For<DbDataReader>();
        var ordinal = 0;
        _ = reader.GetOrdinal(columnName).Returns(ordinal);
        _ = reader.GetFieldValue<T>(ordinal).Returns(fieldValue);

        var result = reader.GetFieldValue<T>(columnName);

        Assert.Equal(expected, result);
    }

    public static TheoryData<object?, string, object?> GetFieldValueStringData =>
        new TheoryData<object?, string, object?>
        {
            { "test", "stringColumn", "test" },
            { 42, "intColumn", 42 },
            { true, "boolColumn", true },
            {
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture),
                "dateColumn",
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture)
            },
        };

    [Fact]
    public async Task GetFieldValueAsync_String_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var name = "column";

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => reader.GetFieldValueAsync<string>(name));

        Assert.Equal("reader", exception.ParamName);
    }

    [Fact]
    public async Task GetFieldValueAsync_String_WhenNameIsNull_ThrowsArgumentNullException()
    {
        var reader = Substitute.For<DbDataReader>();
        string name = null!;

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => reader.GetFieldValueAsync<string>(name));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public async Task GetFieldValueAsync_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = string.Empty;

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => reader.GetFieldValueAsync<string>(name));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public async Task GetFieldValueAsync_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "   ";

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => reader.GetFieldValueAsync<string>(name));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
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

        Assert.Equal(expectedValue, result);
    }

    [Fact]
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

        Assert.Equal(expectedValue, result);
    }

    [Theory]
    [MemberData(nameof(GetFieldValueAsyncStringData))]
    public async Task GetFieldValueAsync_String_Theory_Expected<T>(T expected, string columnName, T fieldValue)
    {
        var reader = Substitute.For<DbDataReader>();
        var ordinal = 0;
        _ = reader.GetOrdinal(columnName).Returns(ordinal);
        _ = reader.GetFieldValueAsync<T>(ordinal, Arg.Any<CancellationToken>()).Returns(Task.FromResult(fieldValue));

        var result = await reader.GetFieldValueAsync<T>(columnName);

        Assert.Equal(expected, result);
    }

    public static TheoryData<object?, string, object?> GetFieldValueAsyncStringData =>
        new TheoryData<object?, string, object?>
        {
            { "test", "stringColumn", "test" },
            { 42, "intColumn", 42 },
            { true, "boolColumn", true },
            {
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture),
                "dateColumn",
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture)
            },
        };

    [Fact]
    public void GetFieldValueOrDefault_Int_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var index = 0;

        var exception = Assert.Throws<ArgumentNullException>(() => reader.GetFieldValueOrDefault<string>(index));

        Assert.Equal("reader", exception.ParamName);
    }

    [Fact]
    public void GetFieldValueOrDefault_Int_WhenColumnIsDBNull_ReturnsDefault()
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        _ = reader.IsDBNull(index).Returns(true);

        var result = reader.GetFieldValueOrDefault<string>(index);

        Assert.Null(result);
    }

    [Fact]
    public void GetFieldValueOrDefault_Int_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        var expectedValue = "test";
        _ = reader.IsDBNull(index).Returns(false);
        _ = reader.GetFieldValue<string>(index).Returns(expectedValue);

        var result = reader.GetFieldValueOrDefault<string>(index);

        Assert.Equal(expectedValue, result);
    }

    [Theory]
    [MemberData(nameof(GetFieldValueOrDefaultIntData))]
    public void GetFieldValueOrDefault_Int_Theory_Expected<T>(T expected, bool isDBNull, T fieldValue)
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        _ = reader.IsDBNull(index).Returns(isDBNull);
        if (!isDBNull)
        {
            _ = reader.GetFieldValue<T>(index).Returns(fieldValue);
        }

        var result = reader.GetFieldValueOrDefault<T>(index);

        Assert.Equal(expected, result);
    }

    public static TheoryData<object?, bool, object?> GetFieldValueOrDefaultIntData =>
        new TheoryData<object?, bool, object?>
        {
            { null, true, "test" },
            { "test", false, "test" },
            { 0, true, 42 },
            { 42, false, 42 },
            { false, true, true },
            { true, false, true },
            { default(DateTime), true, DateTime.Now },
            {
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture),
                false,
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture)
            },
        };

    [Fact]
    public void GetFieldValueOrDefault_String_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var name = "column";

        var exception = Assert.Throws<ArgumentNullException>(() => reader.GetFieldValueOrDefault<string>(name));

        Assert.Equal("reader", exception.ParamName);
    }

    [Fact]
    public void GetFieldValueOrDefault_String_WhenNameIsNull_ThrowsArgumentNullException()
    {
        var reader = Substitute.For<DbDataReader>();
        string name = null!;

        var exception = Assert.Throws<ArgumentNullException>(() => reader.GetFieldValueOrDefault<string>(name));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetFieldValueOrDefault_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = string.Empty;

        var exception = Assert.Throws<ArgumentException>(() => reader.GetFieldValueOrDefault<string>(name));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetFieldValueOrDefault_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "   ";

        var exception = Assert.Throws<ArgumentException>(() => reader.GetFieldValueOrDefault<string>(name));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
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

    [Fact]
    public void GetFieldValueOrDefault_String_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "column";
        var ordinal = 0;
        var expectedValue = "test";
        _ = reader.GetOrdinal(name).Returns(ordinal);
        _ = reader.IsDBNull(ordinal).Returns(false);
        _ = reader.GetFieldValue<string>(ordinal).Returns(expectedValue);

        var result = reader.GetFieldValueOrDefault<string>(name);

        Assert.Equal(expectedValue, result);
    }

    [Theory]
    [MemberData(nameof(GetFieldValueOrDefaultStringData))]
    public void GetFieldValueOrDefault_String_Theory_Expected<T>(
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

        Assert.Equal(expected, result);
    }

    public static TheoryData<object?, string, bool, object?> GetFieldValueOrDefaultStringData =>
        new TheoryData<object?, string, bool, object?>
        {
            { null, "stringColumn", true, "test" },
            { "test", "stringColumn", false, "test" },
            { 0, "intColumn", true, 42 },
            { 42, "intColumn", false, 42 },
            { false, "boolColumn", true, true },
            { true, "boolColumn", false, true },
        };

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_Int_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var index = 0;

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            reader.GetFieldValueOrDefaultAsync<string>(index)
        );

        Assert.Equal("reader", exception.ParamName);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_Int_WhenColumnIsDBNull_ReturnsDefault()
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        _ = reader.IsDBNullAsync(index).Returns(Task.FromResult(true));

        var result = await reader.GetFieldValueOrDefaultAsync<string>(index);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_Int_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        var expectedValue = "test";
        _ = reader.IsDBNullAsync(index).Returns(Task.FromResult(false));
        _ = reader.GetFieldValueAsync<string>(index).Returns(Task.FromResult(expectedValue));

        var result = await reader.GetFieldValueOrDefaultAsync<string>(index);

        Assert.Equal(expectedValue, result);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_Int_WithCancellationToken_WhenColumnIsNotDBNull_ReturnsValue()
    {
        var reader = Substitute.For<DbDataReader>();
        var index = 0;
        var expectedValue = "test";
        var cancellationToken = new CancellationToken();
        _ = reader.IsDBNullAsync(index, cancellationToken).Returns(Task.FromResult(false));
        _ = reader.GetFieldValueAsync<string>(index, cancellationToken).Returns(Task.FromResult(expectedValue));

        var result = await reader.GetFieldValueOrDefaultAsync<string>(index, cancellationToken: cancellationToken);

        Assert.Equal(expectedValue, result);
    }

    [Theory]
    [MemberData(nameof(GetFieldValueOrDefaultAsyncIntData))]
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

        Assert.Equal(expected, result);
    }

    public static TheoryData<object?, bool, object?> GetFieldValueOrDefaultAsyncIntData =>
        new TheoryData<object?, bool, object?>
        {
            { null, true, "test" },
            { "test", false, "test" },
            { 0, true, 42 },
            { 42, false, 42 },
            { false, true, true },
            { true, false, true },
            { default(DateTime), true, DateTime.Now },
            {
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture),
                false,
                DateTime.Parse("2023-01-01", CultureInfo.InvariantCulture)
            },
        };

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_String_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        DbDataReader reader = null!;
        var name = "column";

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            reader.GetFieldValueOrDefaultAsync<string>(name)
        );

        Assert.Equal("reader", exception.ParamName);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_String_WhenNameIsNull_ThrowsArgumentNullException()
    {
        var reader = Substitute.For<DbDataReader>();
        string name = null!;

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            reader.GetFieldValueOrDefaultAsync<string>(name)
        );

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_String_WhenNameIsEmpty_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = string.Empty;

        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            reader.GetFieldValueOrDefaultAsync<string>(name)
        );

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_String_WhenNameIsWhiteSpace_ThrowsArgumentException()
    {
        var reader = Substitute.For<DbDataReader>();
        var name = "   ";

        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            reader.GetFieldValueOrDefaultAsync<string>(name)
        );

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
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

    [Fact]
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

        Assert.Equal(expectedValue, result);
    }

    [Fact]
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

        Assert.Equal(expectedValue, result);
    }

    [Theory]
    [MemberData(nameof(GetFieldValueOrDefaultAsyncStringData))]
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

        Assert.Equal(expected, result);
    }

    public static TheoryData<object?, string, bool, object?> GetFieldValueOrDefaultAsyncStringData =>
        new TheoryData<object?, string, bool, object?>
        {
            { null, "stringColumn", true, "test" },
            { "test", "stringColumn", false, "test" },
            { 0, "intColumn", true, 42 },
            { 42, "intColumn", false, 42 },
            { false, "boolColumn", true, true },
            { true, "boolColumn", false, true },
        };
}
