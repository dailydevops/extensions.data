namespace NetEvolve.Extensions.Data.Tests.Integration;

using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.Sqlite;

[SuppressMessage("Performance", "CA1849:Call async methods when in an async method", Justification = "As designed.")]
[SuppressMessage("Major Code Smell", "S6966:Awaitable method should be used", Justification = "As designed.")]
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

    [Test]
    public async Task GetFieldValue_WithValidColumnName_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        var hasValue = await reader.ReadAsync();
        _ = await Assert.That(hasValue).IsTrue();

        var id = reader.GetFieldValue<int>("Id");
        var name = reader.GetFieldValue<string>("Name");
        var age = reader.GetFieldValue<int>("Age");
        var isActive = reader.GetFieldValue<bool>("IsActive");
        var salary = reader.GetFieldValue<double>("Salary");

        using (Assert.Multiple())
        {
            _ = await Assert.That(id).IsEqualTo(1);
            _ = await Assert.That(name).IsEqualTo("John Doe");
            _ = await Assert.That(age).IsEqualTo(30);
            _ = await Assert.That(isActive).IsTrue();
            _ = await Assert.That(salary).IsEqualTo(50000.50);
        }
    }

    [Test]
    public async Task GetFieldValue_WithInvalidColumnName_ThrowsIndexOutOfRangeException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        var hasValue = await reader.ReadAsync();
        _ = await Assert.That(hasValue).IsTrue();

        _ = Assert.Throws<ArgumentOutOfRangeException>(() => reader.GetFieldValue<string>("NonExistentColumn"));
    }

    [Test]
    public async Task GetFieldValueAsync_WithValidColumnName_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var id = await reader.GetFieldValueAsync<int>("Id");
        var name = await reader.GetFieldValueAsync<string>("Name");
        var age = await reader.GetFieldValueAsync<int>("Age");
        var isActive = await reader.GetFieldValueAsync<bool>("IsActive");
        var salary = await reader.GetFieldValueAsync<double>("Salary");

        using (Assert.Multiple())
        {
            _ = await Assert.That(id).IsEqualTo(2);
            _ = await Assert.That(name).IsEqualTo("Jane Smith");
            _ = await Assert.That(age).IsEqualTo(25);
            _ = await Assert.That(isActive).IsFalse();
            _ = await Assert.That(salary).IsEqualTo(45000.75);
        }
    }

    [Test]
    public async Task GetFieldValueAsync_WithCancellationToken_ReturnsCorrectValue()
    {
        using var cts = new CancellationTokenSource();
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 3";
        using var reader = await command.ExecuteReaderAsync(cts.Token);

        _ = await Assert.That(await reader.ReadAsync(cts.Token)).IsTrue();

        var id = await reader.GetFieldValueAsync<int>("Id", cts.Token);
        var name = await reader.GetFieldValueAsync<string>("Name", cts.Token);

        using (Assert.Multiple())
        {
            _ = await Assert.That(id).IsEqualTo(3);
            _ = await Assert.That(name).IsEqualTo("Bob Johnson");
        }
    }

    [Test]
    public async Task GetFieldValueOrDefault_WithNullValue_ReturnsDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 2"; // Has NULL values
        using var reader = await command.ExecuteReaderAsync();

        var hasValue = await reader.ReadAsync();
        _ = await Assert.That(hasValue).IsTrue();

        var nullableInt = reader.GetFieldValueOrDefault<int?>("NullableInt");
        var nullableText = reader.GetFieldValueOrDefault<string>("NullableText");
        var nullableBoolean = reader.GetFieldValueOrDefault<bool?>("NullableBoolean");
        var nullableDouble = reader.GetFieldValueOrDefault<double?>("NullableDouble");

        using (Assert.Multiple())
        {
            Assert.Null(nullableInt);
            Assert.Null(nullableText);
            Assert.Null(nullableBoolean);
            Assert.Null(nullableDouble);
        }
    }

    [Test]
    public async Task GetFieldValueOrDefault_WithNullValue_ReturnsCustomDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 2"; // Has NULL values
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var nullableInt = reader.GetFieldValueOrDefault("NullableInt", 999);
        var nullableText = reader.GetFieldValueOrDefault("NullableText", "Default Text");
        var nullableBoolean = reader.GetFieldValueOrDefault("NullableBoolean", true);
        var nullableDouble = reader.GetFieldValueOrDefault("NullableDouble", 999.99);

        using (Assert.Multiple())
        {
            _ = await Assert.That(nullableInt).IsEqualTo(999);
            _ = await Assert.That(nullableText).IsEqualTo("Default Text");
            _ = await Assert.That(nullableBoolean).IsTrue();
            _ = await Assert.That(nullableDouble).IsEqualTo(999.99);
        }
    }

    [Test]
    public async Task GetFieldValueOrDefault_WithValidValue_ReturnsActualValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1"; // Has actual values
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var nullableInt = reader.GetFieldValueOrDefault("NullableInt", 999);
        var nullableText = reader.GetFieldValueOrDefault("NullableText", "Default Text");
        var nullableBoolean = reader.GetFieldValueOrDefault("NullableBoolean", false);
        var nullableDouble = reader.GetFieldValueOrDefault("NullableDouble", 999.99);

        using (Assert.Multiple())
        {
            _ = await Assert.That(nullableInt).IsEqualTo(100);
            _ = await Assert.That(nullableText).IsEqualTo("Optional Text");
            _ = await Assert.That(nullableBoolean).IsTrue();
            _ = await Assert.That(nullableDouble).IsEqualTo(123.45);
        }
    }

    [Test]
    public async Task GetFieldValueOrDefault_WithOrdinal_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt, NullableText FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var nullableInt = reader.GetFieldValueOrDefault<int?>(0); // NullableInt ordinal
        var nullableText = reader.GetFieldValueOrDefault<string>(1); // NullableText ordinal

        Assert.Null(nullableInt);
        Assert.Null(nullableText);
    }

    [Test]
    public async Task GetFieldValueOrDefault_WithOrdinal_ReturnsCustomDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt, NullableText FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var nullableInt = reader.GetFieldValueOrDefault(0, 777); // NullableInt ordinal
        var nullableText = reader.GetFieldValueOrDefault(1, "Custom Default"); // NullableText ordinal

        using (Assert.Multiple())
        {
            _ = await Assert.That(nullableInt).IsEqualTo(777);
            _ = await Assert.That(nullableText).IsEqualTo("Custom Default");
        }
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_WithNullValue_ReturnsDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 2"; // Has NULL values
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var nullableInt = await reader.GetFieldValueOrDefaultAsync<int?>("NullableInt");
        var nullableText = await reader.GetFieldValueOrDefaultAsync<string>("NullableText");
        var nullableBoolean = await reader.GetFieldValueOrDefaultAsync<bool?>("NullableBoolean");
        var nullableDouble = await reader.GetFieldValueOrDefaultAsync<double?>("NullableDouble");

        Assert.Null(nullableInt);
        Assert.Null(nullableText);
        Assert.Null(nullableBoolean);
        Assert.Null(nullableDouble);
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_WithNullValue_ReturnsCustomDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 2"; // Has NULL values
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var nullableInt = await reader.GetFieldValueOrDefaultAsync("NullableInt", 888);
        var nullableText = await reader.GetFieldValueOrDefaultAsync("NullableText", "Async Default");
        var nullableBoolean = await reader.GetFieldValueOrDefaultAsync("NullableBoolean", true);
        var nullableDouble = await reader.GetFieldValueOrDefaultAsync("NullableDouble", 888.88);

        using (Assert.Multiple())
        {
            _ = await Assert.That(nullableInt).IsEqualTo(888);
            _ = await Assert.That(nullableText).IsEqualTo("Async Default");
            _ = await Assert.That(nullableBoolean).IsTrue();
            _ = await Assert.That(nullableDouble).IsEqualTo(888.88);
        }
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_WithValidValue_ReturnsActualValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 3"; // Has actual values
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var nullableInt = await reader.GetFieldValueOrDefaultAsync("NullableInt", 888);
        var nullableText = await reader.GetFieldValueOrDefaultAsync("NullableText", "Async Default");
        var nullableBoolean = await reader.GetFieldValueOrDefaultAsync("NullableBoolean", true);
        var nullableDouble = await reader.GetFieldValueOrDefaultAsync("NullableDouble", 888.88);

        using (Assert.Multiple())
        {
            _ = await Assert.That(nullableInt).IsEqualTo(200);
            _ = await Assert.That(nullableText).IsEqualTo("Another Text");
            _ = await Assert.That(nullableBoolean).IsFalse();
            _ = await Assert.That(nullableDouble).IsEqualTo(987.65);
        }
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_WithOrdinal_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt, NullableText FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var nullableInt = await reader.GetFieldValueOrDefaultAsync<int?>(0); // NullableInt ordinal
        var nullableText = await reader.GetFieldValueOrDefaultAsync<string>(1); // NullableText ordinal

        Assert.Null(nullableInt);
        Assert.Null(nullableText);
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_WithOrdinal_ReturnsCustomDefault()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt, NullableText FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var nullableInt = await reader.GetFieldValueOrDefaultAsync(0, 666); // NullableInt ordinal
        var nullableText = await reader.GetFieldValueOrDefaultAsync(1, "Async Ordinal Default"); // NullableText ordinal

        using (Assert.Multiple())
        {
            _ = await Assert.That(nullableInt).IsEqualTo(666);
            _ = await Assert.That(nullableText).IsEqualTo("Async Ordinal Default");
        }
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_WithCancellationToken_ReturnsCorrectValue()
    {
        using var cts = new CancellationTokenSource();
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync(cts.Token);

        _ = await Assert.That(await reader.ReadAsync(cts.Token)).IsTrue();

        var nullableInt = await reader.GetFieldValueOrDefaultAsync("NullableInt", 555, cts.Token);
        var nullableText = await reader.GetFieldValueOrDefaultAsync("NullableText", "Token Default", cts.Token);

        using (Assert.Multiple())
        {
            _ = await Assert.That(nullableInt).IsEqualTo(100);
            _ = await Assert.That(nullableText).IsEqualTo("Optional Text");
        }
    }

    [Test]
    public async Task GetFieldValueOrDefaultAsync_WithOrdinalAndCancellationToken_ReturnsCorrectValue()
    {
        using var cts = new CancellationTokenSource();
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt, NullableText FROM TestData WHERE Id = 3";
        using var reader = await command.ExecuteReaderAsync(cts.Token);

        _ = await Assert.That(await reader.ReadAsync(cts.Token)).IsTrue();

        var nullableInt = await reader.GetFieldValueOrDefaultAsync(0, 444, cts.Token); // NullableInt ordinal
        var nullableText = await reader.GetFieldValueOrDefaultAsync(1, "Ordinal Token Default", cts.Token); // NullableText ordinal

        using (Assert.Multiple())
        {
            _ = await Assert.That(nullableInt).IsEqualTo(200);
            _ = await Assert.That(nullableText).IsEqualTo("Another Text");
        }
    }

    [Test]
    public async Task GetFieldValue_WithMultipleDataTypes_ReturnsCorrectTypes()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, Age, IsActive, Salary, BirthDate FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var id = reader.GetFieldValue<long>("Id"); // SQLite INTEGER as long
        var name = reader.GetFieldValue<string>("Name");
        var age = reader.GetFieldValue<long>("Age"); // SQLite INTEGER as long
        var isActive = reader.GetFieldValue<bool>("IsActive");
        var salary = reader.GetFieldValue<double>("Salary");
        var birthDate = reader.GetFieldValue<string>("BirthDate");

        using (Assert.Multiple())
        {
            _ = await Assert.That(id).IsEqualTo(1L);
            _ = await Assert.That(name).IsEqualTo("John Doe");
            _ = await Assert.That(age).IsEqualTo(30L);
            _ = await Assert.That(isActive).IsTrue();
            _ = await Assert.That(salary).IsEqualTo(50000.50);
            _ = await Assert.That(birthDate).IsEqualTo("1993-01-15");
        }
    }

    [Test]
    public async Task GetFieldValueOrDefault_ExceptionHandling_ThrowsCorrectExceptions()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        // Test invalid column name
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => reader.GetFieldValueOrDefault<string>("InvalidColumn"));

        // Test invalid ordinal
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => reader.GetFieldValueOrDefault<string>(999));
    }
}
