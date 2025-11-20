namespace NetEvolve.Extensions.Data.Tests.Integration;

using System.Data;
using System.Globalization;
using Microsoft.Data.Sqlite;

public sealed class IDataRecordExtensionsIntegrationTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private bool _disposed;

    public IDataRecordExtensionsIntegrationTests()
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
                NullableBoolean BOOLEAN,
                NullableByte INTEGER,
                NullableChar TEXT,
                NullableDateTime TEXT,
                NullableDecimal REAL,
                NullableDouble REAL,
                NullableFloat REAL,
                NullableGuid TEXT,
                NullableInt16 INTEGER,
                NullableInt32 INTEGER,
                NullableInt64 INTEGER,
                NullableString TEXT,
                ByteValue INTEGER,
                CharValue TEXT,
                DateTimeValue TEXT,
                DecimalValue REAL,
                DoubleValue REAL,
                FloatValue REAL,
                GuidValue TEXT,
                Int16Value INTEGER,
                Int32Value INTEGER,
                Int64Value INTEGER
            )
            """;
        _ = command.ExecuteNonQuery();
    }

    private void InsertTestData()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = """
            INSERT INTO TestData (
                Id, Name, Age, IsActive, Salary, BirthDate,
                NullableBoolean, NullableByte, NullableChar, NullableDateTime, 
                NullableDecimal, NullableDouble, NullableFloat, NullableGuid,
                NullableInt16, NullableInt32, NullableInt64, NullableString,
                ByteValue, CharValue, DateTimeValue, DecimalValue, DoubleValue,
                FloatValue, GuidValue, Int16Value, Int32Value, Int64Value
            )
            VALUES 
                (1, 'John Doe', 30, 1, 50000.50, '1993-01-15',
                 1, 255, 'A', '2023-01-15 10:30:00', 
                 12345.67, 123.456, 78.9, '550e8400-e29b-41d4-a716-446655440000',
                 32767, 2147483647, 9223372036854775807, 'Not Null',
                 128, 'B', '2023-01-16 11:00:00', 98765.43, 987.654,
                 321.0, '6ba7b810-9dad-11d1-80b4-00c04fd430c8', 16383, 1073741823, 4611686018427387903),
                (2, 'Jane Smith', 25, 0, 45000.75, '1998-06-20',
                 NULL, NULL, NULL, NULL,
                 NULL, NULL, NULL, NULL,
                 NULL, NULL, NULL, NULL,
                 64, 'C', '2023-02-15 12:30:00', 54321.98, 543.210,
                 123.4, '6ba7b811-9dad-11d1-80b4-00c04fd430c8', 8191, 536870911, 2305843009213693951),
                (3, 'Bob Johnson', 35, 1, 60000.00, '1988-12-05',
                 0, 127, 'Z', '2023-03-10 14:15:00',
                 99999.99, 999.999, 555.5, '6ba7b812-9dad-11d1-80b4-00c04fd430c8',
                 -32768, -2147483648, -9223372036854775808, 'Another String',
                 32, 'D', '2023-03-11 15:45:00', 11111.11, 111.111,
                 222.2, '6ba7b813-9dad-11d1-80b4-00c04fd430c8', -16384, -1073741824, -4611686018427387904)
            """;
        _ = command.ExecuteNonQuery();
    }

    [Test]
    public async Task GetNullableBoolean_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableBoolean(0);

        _ = await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task GetNullableBoolean_ByIndex_WithNullValue_ReturnsNull()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableBoolean(0);

        Assert.Null(result);
    }

    [Test]
    public async Task GetNullableBoolean_ByIndex_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableBoolean(0, true);

        _ = await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task GetNullableBoolean_ByName_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 3";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableBoolean("NullableBoolean");

        _ = await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task GetNullableBoolean_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableBoolean("NullableBoolean", false);

        _ = await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task GetNullableByte_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableByte FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableByte(0);

        _ = await Assert.That(result).IsEqualTo((byte)255);
    }

    [Test]
    public async Task GetNullableByte_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableByte FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableByte("NullableByte", (byte)100);

        _ = await Assert.That(result).IsEqualTo((byte)100);
    }

    [Test]
    public async Task GetNullableChar_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableChar FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableChar(0);

        _ = await Assert.That(result).IsEqualTo('A');
    }

    [Test]
    public async Task GetNullableChar_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableChar FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableChar("NullableChar", 'X');

        _ = await Assert.That(result).IsEqualTo('X');
    }

    [Test]
    public async Task GetNullableDateTime_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDateTime FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableDateTime(0);

        _ = await Assert.That(result).IsEqualTo(DateTime.Parse("2023-01-15 10:30:00", CultureInfo.InvariantCulture));
    }

    [Test]
    public async Task GetNullableDateTime_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDateTime FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var defaultDate = new DateTime(2023, 12, 25, 0, 0, 0, DateTimeKind.Utc);
        var result = reader.GetNullableDateTime("NullableDateTime", defaultDate);

        _ = await Assert.That(result).IsEqualTo(defaultDate);
    }

    [Test]
    public async Task GetNullableDecimal_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDecimal FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableDecimal(0);

        _ = await Assert.That(result).IsEqualTo(12345.67m);
    }

    [Test]
    public async Task GetNullableDecimal_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDecimal FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableDecimal("NullableDecimal", 999.99m);

        _ = await Assert.That(result).IsEqualTo(999.99m);
    }

    [Test]
    public async Task GetNullableDouble_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDouble FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableDouble(0);

        using (Assert.Multiple())
        {
            Assert.NotNull(result);
            _ = await Assert.That(Math.Abs(result.Value - 123.456) < 0.001).IsTrue();
        }
    }

    [Test]
    public async Task GetNullableDouble_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDouble FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableDouble("NullableDouble", 888.888);

        using (Assert.Multiple())
        {
            Assert.NotNull(result);
            _ = await Assert.That(Math.Abs(result.Value - 888.888) < 0.001).IsTrue();
        }
    }

    [Test]
    public async Task GetNullableFloat_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableFloat FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableFloat(0);

        using (Assert.Multiple())
        {
            Assert.NotNull(result);
            _ = await Assert.That(Math.Abs(result.Value - 78.9f) < 0.1f).IsTrue();
        }
    }

    [Test]
    public async Task GetNullableFloat_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableFloat FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableFloat("NullableFloat", 777.7f);

        using (Assert.Multiple())
        {
            Assert.NotNull(result);
            _ = await Assert.That(Math.Abs(result.Value - 777.7f) < 0.1f).IsTrue();
        }
    }

    [Test]
    public async Task GetNullableGuid_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableGuid FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableGuid(0);

        _ = await Assert.That(result).IsEqualTo(Guid.Parse("550e8400-e29b-41d4-a716-446655440000"));
    }

    [Test]
    public async Task GetNullableGuid_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableGuid FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var defaultGuid = Guid.Parse("00000000-0000-0000-0000-000000000001");
        var result = reader.GetNullableGuid("NullableGuid", defaultGuid);

        _ = await Assert.That(result).IsEqualTo(defaultGuid);
    }

    [Test]
    public async Task GetNullableInt16_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt16 FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableInt16(0);

        _ = await Assert.That(result).IsEqualTo((short)32767);
    }

    [Test]
    public async Task GetNullableInt16_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt16 FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableInt16("NullableInt16", (short)999);

        _ = await Assert.That(result).IsEqualTo((short)999);
    }

    [Test]
    public async Task GetNullableInt16_ByName_WithNegativeValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt16 FROM TestData WHERE Id = 3";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableInt16("NullableInt16");

        _ = await Assert.That(result).IsEqualTo((short)-32768);
    }

    [Test]
    public async Task GetNullableInt32_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt32 FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableInt32(0);

        _ = await Assert.That(result).IsEqualTo(2147483647);
    }

    [Test]
    public async Task GetNullableInt32_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt32 FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableInt32("NullableInt32", 12345);

        _ = await Assert.That(result).IsEqualTo(12345);
    }

    [Test]
    public async Task GetNullableInt32_ByName_WithNegativeValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt32 FROM TestData WHERE Id = 3";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableInt32("NullableInt32");

        _ = await Assert.That(result).IsEqualTo(-2147483648);
    }

    [Test]
    public async Task GetNullableInt64_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt64 FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableInt64(0);

        _ = await Assert.That(result).IsEqualTo(9223372036854775807L);
    }

    [Test]
    public async Task GetNullableInt64_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt64 FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableInt64("NullableInt64", 987654321L);

        _ = await Assert.That(result).IsEqualTo(987654321L);
    }

    [Test]
    public async Task GetNullableInt64_ByName_WithNegativeValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt64 FROM TestData WHERE Id = 3";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableInt64("NullableInt64");

        _ = await Assert.That(result).IsEqualTo(-9223372036854775808L);
    }

    [Test]
    public async Task GetNullableString_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableString FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableString(0);

        _ = await Assert.That(result).IsEqualTo("Not Null");
    }

    [Test]
    public async Task GetNullableString_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableString FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableString("NullableString", "Default String");

        _ = await Assert.That(result).IsEqualTo("Default String");
    }

    [Test]
    public async Task GetNullableString_ByName_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableString FROM TestData WHERE Id = 3";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableString("NullableString");

        _ = await Assert.That(result).IsEqualTo("Another String");
    }

    [Test]
    public async Task GetNullableValue_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT Name FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableValue(0);

        _ = await Assert.That(result).IsEqualTo("John Doe");
    }

    [Test]
    public async Task GetNullableValue_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableString FROM TestData WHERE Id = 2";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableValue("NullableString", "Default Object");

        _ = await Assert.That(result).IsEqualTo("Default Object");
    }

    [Test]
    public async Task GetNullableValue_ByName_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT Age FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();
        var result = reader.GetNullableValue("Age");

        _ = await Assert.That(result).IsEqualTo(30L); // SQLite returns INTEGER as long
    }

    [Test]
    public async Task GetNullableBoolean_WithNullRecord_ThrowsArgumentNullException()
    {
        IDataRecord record = null!;

        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableBoolean(0));

        _ = await Assert.That(exception.ParamName).IsEqualTo("record");
    }

    [Test]
    public async Task GetNullableBoolean_WithNullName_ThrowsArgumentNullException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var exception = Assert.Throws<ArgumentNullException>(() => reader.GetNullableBoolean(null!));

        _ = await Assert.That(exception.ParamName).IsEqualTo("name");
    }

    [Test]
    public async Task GetNullableBoolean_WithEmptyName_ThrowsArgumentException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var exception = Assert.Throws<ArgumentException>(() => reader.GetNullableBoolean(""));

        _ = await Assert.That(exception.ParamName).IsEqualTo("name");
    }

    [Test]
    public async Task GetNullableBoolean_WithInvalidIndex_ThrowsArgumentOutOfRangeException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        _ = Assert.Throws<ArgumentOutOfRangeException>(() => reader.GetNullableBoolean(10));
    }

    [Test]
    public async Task GetNullableBoolean_WithInvalidColumnName_ThrowsArgumentOutOfRangeException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 1";
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        _ = Assert.Throws<ArgumentOutOfRangeException>(() => reader.GetNullableBoolean("NonExistentColumn"));
    }

    [Test]
    public async Task GetNullableValues_MultipleColumns_ReturnsCorrectValues()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = """
            SELECT 
                NullableBoolean, NullableByte, NullableChar, NullableDateTime,
                NullableDecimal, NullableDouble, NullableFloat, NullableGuid,
                NullableInt16, NullableInt32, NullableInt64, NullableString
            FROM TestData WHERE Id = 1
            """;
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var boolean = reader.GetNullableBoolean(0);
        var byteValue = reader.GetNullableByte(1);
        var charValue = reader.GetNullableChar(2);
        var dateTime = reader.GetNullableDateTime(3);
        var decimalValue = reader.GetNullableDecimal(4);
        var doubleValue = reader.GetNullableDouble(5);
        var floatValue = reader.GetNullableFloat(6);
        var guidValue = reader.GetNullableGuid(7);
        var int16Value = reader.GetNullableInt16(8);
        var int32Value = reader.GetNullableInt32(9);
        var int64Value = reader.GetNullableInt64(10);
        var stringValue = reader.GetNullableString(11);

        using (Assert.Multiple())
        {
            _ = await Assert.That(boolean).IsTrue();
            _ = await Assert.That(byteValue).IsEqualTo((byte)255);
            _ = await Assert.That(charValue).IsEqualTo('A');
            _ = await Assert
                .That(dateTime)
                .IsEqualTo(DateTime.Parse("2023-01-15 10:30:00", CultureInfo.InvariantCulture));
            _ = await Assert.That(decimalValue).IsEqualTo(12345.67m);
            Assert.NotNull(doubleValue);
            _ = await Assert.That(Math.Abs(doubleValue.Value - 123.456) < 0.001).IsTrue();
            Assert.NotNull(floatValue);
            _ = await Assert.That(Math.Abs(floatValue.Value - 78.9f) < 0.1f).IsTrue();
            _ = await Assert.That(guidValue).IsEqualTo(Guid.Parse("550e8400-e29b-41d4-a716-446655440000"));
            _ = await Assert.That(int16Value).IsEqualTo((short)32767);
            _ = await Assert.That(int32Value).IsEqualTo(2147483647);
            _ = await Assert.That(int64Value).IsEqualTo(9223372036854775807L);
            _ = await Assert.That(stringValue).IsEqualTo("Not Null");
        }
    }

    [Test]
    public async Task GetNullableValues_AllNullColumns_ReturnsDefaultValues()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = """
            SELECT 
                NullableBoolean, NullableByte, NullableChar, NullableDateTime,
                NullableDecimal, NullableDouble, NullableFloat, NullableGuid,
                NullableInt16, NullableInt32, NullableInt64, NullableString
            FROM TestData WHERE Id = 2
            """;
        using var reader = await command.ExecuteReaderAsync();

        _ = await Assert.That(await reader.ReadAsync()).IsTrue();

        var boolean = reader.GetNullableBoolean("NullableBoolean", true);
        var byteValue = reader.GetNullableByte("NullableByte", (byte)100);
        var charValue = reader.GetNullableChar("NullableChar", 'X');
        var dateTime = reader.GetNullableDateTime(
            "NullableDateTime",
            new DateTime(2023, 12, 31, 0, 0, 0, DateTimeKind.Utc)
        );
        var decimalValue = reader.GetNullableDecimal("NullableDecimal", 999.99m);
        var doubleValue = reader.GetNullableDouble("NullableDouble", 888.888);
        var floatValue = reader.GetNullableFloat("NullableFloat", 777.7f);
        var guidValue = reader.GetNullableGuid("NullableGuid", Guid.Empty);
        var int16Value = reader.GetNullableInt16("NullableInt16", (short)123);
        var int32Value = reader.GetNullableInt32("NullableInt32", 456);
        var int64Value = reader.GetNullableInt64("NullableInt64", 789L);
        var stringValue = reader.GetNullableString("NullableString", "Default");

        using (Assert.Multiple())
        {
            _ = await Assert.That(boolean).IsTrue();
            _ = await Assert.That(byteValue).IsEqualTo((byte)100);
            _ = await Assert.That(charValue).IsEqualTo('X');
            _ = await Assert.That(dateTime).IsEqualTo(new DateTime(2023, 12, 31, 0, 0, 0, DateTimeKind.Utc));
            _ = await Assert.That(decimalValue).IsEqualTo(999.99m);
            Assert.NotNull(doubleValue);
            _ = await Assert.That(Math.Abs(doubleValue.Value - 888.888) < 0.001).IsTrue();
            Assert.NotNull(floatValue);
            _ = await Assert.That(Math.Abs(floatValue.Value - 777.7f) < 0.1f).IsTrue();
            _ = await Assert.That(guidValue).IsEqualTo(Guid.Empty);
            _ = await Assert.That(int16Value).IsEqualTo((short)123);
            _ = await Assert.That(int32Value).IsEqualTo(456);
            _ = await Assert.That(int64Value).IsEqualTo(789L);
            _ = await Assert.That(stringValue).IsEqualTo("Default");
        }
    }
}
