namespace NetEvolve.Extensions.Data.Tests.Integration;

using System.Data;
using Microsoft.Data.Sqlite;
using Xunit;

public sealed class DbDataReaderExtensionsIntegrationTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private bool _disposed;

    public DbDataReaderExtensionsIntegrationTests()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        CreateTestTable();
        InsertTestData();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _connection?.Dispose();
            _disposed = true;
        }
    }

    private void CreateTestTable()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE TestData (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL,
                Age INTEGER,
                IsActive BOOLEAN,
                Salary REAL,
                BirthDate TEXT,
                NullableInt INTEGER,
                NullableText TEXT,
                NullableBoolean BOOLEAN,
                NullableDouble REAL
            )
            """;
        _ = command.ExecuteNonQuery();
    }

    private void InsertTestData()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = """
            INSERT INTO TestData (Id, Name, Age, IsActive, Salary, BirthDate, NullableInt, NullableText, NullableBoolean, NullableDouble)
            VALUES 
                (1, 'John Doe', 30, 1, 50000.50, '1993-01-15', 100, 'Optional Text', 1, 123.45),
                (2, 'Jane Smith', 25, 0, 45000.75, '1998-06-20', NULL, NULL, NULL, NULL),
                (3, 'Bob Johnson', 35, 1, 60000.00, '1988-12-05', 200, 'Another Text', 0, 987.65)
            """;
        _ = command.ExecuteNonQuery();
    }

    [Fact]
    public void GetFieldValue_WithValidColumnName_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        var id = reader.GetFieldValue<int>("Id");
        var name = reader.GetFieldValue<string>("Name");
        var age = reader.GetFieldValue<int>("Age");
        var isActive = reader.GetFieldValue<bool>("IsActive");
        var salary = reader.GetFieldValue<double>("Salary");

        Assert.Equal(1, id);
        Assert.Equal("John Doe", name);
        Assert.Equal(30, age);
        Assert.True(isActive);
        Assert.Equal(50000.50, salary, 2);
    }

    [Fact]
    public void GetFieldValue_WithInvalidColumnName_ThrowsIndexOutOfRangeException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        _ = Assert.Throws<ArgumentOutOfRangeException>(() => reader.GetFieldValue<string>("NonExistentColumn"));
    }

    [Fact]
    public async Task GetFieldValueAsync_WithValidColumnName_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        Assert.True(await reader.ReadAsync());

        var id = await reader.GetFieldValueAsync<int>("Id");
        var name = await reader.GetFieldValueAsync<string>("Name");
        var age = await reader.GetFieldValueAsync<int>("Age");
        var isActive = await reader.GetFieldValueAsync<bool>("IsActive");
        var salary = await reader.GetFieldValueAsync<double>("Salary");

        Assert.Equal(2, id);
        Assert.Equal("Jane Smith", name);
        Assert.Equal(25, age);
        Assert.False(isActive);
        Assert.Equal(45000.75, salary, 2);
    }

    [Fact]
    public async Task GetFieldValueAsync_WithCancellationToken_ReturnsCorrectValue()
    {
        using var cts = new CancellationTokenSource();
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 3";
        using var reader = await command.ExecuteReaderAsync(cts.Token);

        Assert.True(await reader.ReadAsync(cts.Token));

        var id = await reader.GetFieldValueAsync<int>("Id", cts.Token);
        var name = await reader.GetFieldValueAsync<string>("Name", cts.Token);

        Assert.Equal(3, id);
        Assert.Equal("Bob Johnson", name);
    }

    [Fact]
    public void GetFieldValueOrDefault_WithNullValue_ReturnsDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 2"; // Has NULL values
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        var nullableInt = reader.GetFieldValueOrDefault<int?>("NullableInt");
        var nullableText = reader.GetFieldValueOrDefault<string>("NullableText");
        var nullableBoolean = reader.GetFieldValueOrDefault<bool?>("NullableBoolean");
        var nullableDouble = reader.GetFieldValueOrDefault<double?>("NullableDouble");

        Assert.Null(nullableInt);
        Assert.Null(nullableText);
        Assert.Null(nullableBoolean);
        Assert.Null(nullableDouble);
    }

    [Fact]
    public void GetFieldValueOrDefault_WithNullValue_ReturnsCustomDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 2"; // Has NULL values
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        var nullableInt = reader.GetFieldValueOrDefault("NullableInt", 999);
        var nullableText = reader.GetFieldValueOrDefault("NullableText", "Default Text");
        var nullableBoolean = reader.GetFieldValueOrDefault("NullableBoolean", true);
        var nullableDouble = reader.GetFieldValueOrDefault("NullableDouble", 999.99);

        Assert.Equal(999, nullableInt);
        Assert.Equal("Default Text", nullableText);
        Assert.True(nullableBoolean);
        Assert.Equal(999.99, nullableDouble, 2);
    }

    [Fact]
    public void GetFieldValueOrDefault_WithValidValue_ReturnsActualValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1"; // Has actual values
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        var nullableInt = reader.GetFieldValueOrDefault("NullableInt", 999);
        var nullableText = reader.GetFieldValueOrDefault("NullableText", "Default Text");
        var nullableBoolean = reader.GetFieldValueOrDefault("NullableBoolean", false);
        var nullableDouble = reader.GetFieldValueOrDefault("NullableDouble", 999.99);

        Assert.Equal(100, nullableInt);
        Assert.Equal("Optional Text", nullableText);
        Assert.True(nullableBoolean);
        Assert.Equal(123.45, nullableDouble, 2);
    }

    [Fact]
    public void GetFieldValueOrDefault_WithOrdinal_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt, NullableText FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        var nullableInt = reader.GetFieldValueOrDefault<int?>(0); // NullableInt ordinal
        var nullableText = reader.GetFieldValueOrDefault<string>(1); // NullableText ordinal

        Assert.Null(nullableInt);
        Assert.Null(nullableText);
    }

    [Fact]
    public void GetFieldValueOrDefault_WithOrdinal_ReturnsCustomDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt, NullableText FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        var nullableInt = reader.GetFieldValueOrDefault(0, 777); // NullableInt ordinal
        var nullableText = reader.GetFieldValueOrDefault(1, "Custom Default"); // NullableText ordinal

        Assert.Equal(777, nullableInt);
        Assert.Equal("Custom Default", nullableText);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_WithNullValue_ReturnsDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 2"; // Has NULL values
        using var reader = await command.ExecuteReaderAsync();

        Assert.True(await reader.ReadAsync());

        var nullableInt = await reader.GetFieldValueOrDefaultAsync<int?>("NullableInt");
        var nullableText = await reader.GetFieldValueOrDefaultAsync<string>("NullableText");
        var nullableBoolean = await reader.GetFieldValueOrDefaultAsync<bool?>("NullableBoolean");
        var nullableDouble = await reader.GetFieldValueOrDefaultAsync<double?>("NullableDouble");

        Assert.Null(nullableInt);
        Assert.Null(nullableText);
        Assert.Null(nullableBoolean);
        Assert.Null(nullableDouble);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_WithNullValue_ReturnsCustomDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 2"; // Has NULL values
        using var reader = await command.ExecuteReaderAsync();

        Assert.True(await reader.ReadAsync());

        var nullableInt = await reader.GetFieldValueOrDefaultAsync("NullableInt", 888);
        var nullableText = await reader.GetFieldValueOrDefaultAsync("NullableText", "Async Default");
        var nullableBoolean = await reader.GetFieldValueOrDefaultAsync("NullableBoolean", true);
        var nullableDouble = await reader.GetFieldValueOrDefaultAsync("NullableDouble", 888.88);

        Assert.Equal(888, nullableInt);
        Assert.Equal("Async Default", nullableText);
        Assert.True(nullableBoolean);
        Assert.Equal(888.88, nullableDouble, 2);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_WithValidValue_ReturnsActualValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 3"; // Has actual values
        using var reader = await command.ExecuteReaderAsync();

        Assert.True(await reader.ReadAsync());

        var nullableInt = await reader.GetFieldValueOrDefaultAsync("NullableInt", 888);
        var nullableText = await reader.GetFieldValueOrDefaultAsync("NullableText", "Async Default");
        var nullableBoolean = await reader.GetFieldValueOrDefaultAsync("NullableBoolean", true);
        var nullableDouble = await reader.GetFieldValueOrDefaultAsync("NullableDouble", 888.88);

        Assert.Equal(200, nullableInt);
        Assert.Equal("Another Text", nullableText);
        Assert.False(nullableBoolean);
        Assert.Equal(987.65, nullableDouble, 2);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_WithOrdinal_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt, NullableText FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        Assert.True(await reader.ReadAsync());

        var nullableInt = await reader.GetFieldValueOrDefaultAsync<int?>(0); // NullableInt ordinal
        var nullableText = await reader.GetFieldValueOrDefaultAsync<string>(1); // NullableText ordinal

        Assert.Null(nullableInt);
        Assert.Null(nullableText);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_WithOrdinal_ReturnsCustomDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt, NullableText FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        Assert.True(await reader.ReadAsync());

        var nullableInt = await reader.GetFieldValueOrDefaultAsync(0, 666); // NullableInt ordinal
        var nullableText = await reader.GetFieldValueOrDefaultAsync(1, "Async Ordinal Default"); // NullableText ordinal

        Assert.Equal(666, nullableInt);
        Assert.Equal("Async Ordinal Default", nullableText);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_WithCancellationToken_ReturnsCorrectValue()
    {
        using var cts = new CancellationTokenSource();
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync(cts.Token);

        Assert.True(await reader.ReadAsync(cts.Token));

        var nullableInt = await reader.GetFieldValueOrDefaultAsync("NullableInt", 555, cts.Token);
        var nullableText = await reader.GetFieldValueOrDefaultAsync("NullableText", "Token Default", cts.Token);

        Assert.Equal(100, nullableInt);
        Assert.Equal("Optional Text", nullableText);
    }

    [Fact]
    public async Task GetFieldValueOrDefaultAsync_WithOrdinalAndCancellationToken_ReturnsCorrectValue()
    {
        using var cts = new CancellationTokenSource();
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt, NullableText FROM TestData WHERE Id = 3";
        using var reader = await command.ExecuteReaderAsync(cts.Token);

        Assert.True(await reader.ReadAsync(cts.Token));

        var nullableInt = await reader.GetFieldValueOrDefaultAsync(0, 444, cts.Token); // NullableInt ordinal
        var nullableText = await reader.GetFieldValueOrDefaultAsync(1, "Ordinal Token Default", cts.Token); // NullableText ordinal

        Assert.Equal(200, nullableInt);
        Assert.Equal("Another Text", nullableText);
    }

    [Fact]
    public void GetFieldValue_WithMultipleDataTypes_ReturnsCorrectTypes()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, Age, IsActive, Salary, BirthDate FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        var id = reader.GetFieldValue<long>("Id"); // SQLite INTEGER as long
        var name = reader.GetFieldValue<string>("Name");
        var age = reader.GetFieldValue<long>("Age"); // SQLite INTEGER as long
        var isActive = reader.GetFieldValue<bool>("IsActive");
        var salary = reader.GetFieldValue<double>("Salary");
        var birthDate = reader.GetFieldValue<string>("BirthDate");

        Assert.Equal(1L, id);
        Assert.Equal("John Doe", name);
        Assert.Equal(30L, age);
        Assert.True(isActive);
        Assert.Equal(50000.50, salary, 2);
        Assert.Equal("1993-01-15", birthDate);
    }

    [Fact]
    public void GetFieldValueOrDefault_ExceptionHandling_ThrowsCorrectExceptions()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        // Test invalid column name
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => reader.GetFieldValueOrDefault<string>("InvalidColumn"));

        // Test invalid ordinal
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => reader.GetFieldValueOrDefault<string>(999));
    }
}
