namespace NetEvolve.Extensions.Data.Tests.Integration;

using System.Data;
using System.Globalization;
using Microsoft.Data.Sqlite;
using Xunit;

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

    #region GetNullableBoolean Tests

    [Fact]
    public void GetNullableBoolean_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableBoolean(0);

        Assert.True(result);
    }

    [Fact]
    public void GetNullableBoolean_ByIndex_WithNullValue_ReturnsNull()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableBoolean(0);

        Assert.Null(result);
    }

    [Fact]
    public void GetNullableBoolean_ByIndex_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableBoolean(0, true);

        Assert.True(result);
    }

    [Fact]
    public void GetNullableBoolean_ByName_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 3";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableBoolean("NullableBoolean");

        Assert.False(result);
    }

    [Fact]
    public void GetNullableBoolean_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableBoolean("NullableBoolean", false);

        Assert.False(result);
    }

    #endregion

    #region GetNullableByte Tests

    [Fact]
    public void GetNullableByte_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableByte FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableByte(0);

        Assert.Equal((byte)255, result);
    }

    [Fact]
    public void GetNullableByte_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableByte FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableByte("NullableByte", (byte)100);

        Assert.Equal((byte)100, result);
    }

    #endregion

    #region GetNullableChar Tests

    [Fact]
    public void GetNullableChar_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableChar FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableChar(0);

        Assert.Equal('A', result);
    }

    [Fact]
    public void GetNullableChar_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableChar FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableChar("NullableChar", 'X');

        Assert.Equal('X', result);
    }

    #endregion

    #region GetNullableDateTime Tests

    [Fact]
    public void GetNullableDateTime_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDateTime FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableDateTime(0);

        Assert.Equal(DateTime.Parse("2023-01-15 10:30:00", CultureInfo.InvariantCulture), result);
    }

    [Fact]
    public void GetNullableDateTime_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDateTime FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var defaultDate = new DateTime(2023, 12, 25, 0, 0, 0, DateTimeKind.Utc);
        var result = reader.GetNullableDateTime("NullableDateTime", defaultDate);

        Assert.Equal(defaultDate, result);
    }

    #endregion

    #region GetNullableDecimal Tests

    [Fact]
    public void GetNullableDecimal_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDecimal FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableDecimal(0);

        Assert.Equal(12345.67m, result);
    }

    [Fact]
    public void GetNullableDecimal_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDecimal FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableDecimal("NullableDecimal", 999.99m);

        Assert.Equal(999.99m, result);
    }

    #endregion

    #region GetNullableDouble Tests

    [Fact]
    public void GetNullableDouble_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDouble FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableDouble(0);

        _ = Assert.NotNull(result);
        Assert.True(Math.Abs(result.Value - 123.456) < 0.001);
    }

    [Fact]
    public void GetNullableDouble_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableDouble FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableDouble("NullableDouble", 888.888);

        _ = Assert.NotNull(result);
        Assert.True(Math.Abs(result.Value - 888.888) < 0.001);
    }

    #endregion

    #region GetNullableFloat Tests

    [Fact]
    public void GetNullableFloat_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableFloat FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableFloat(0);

        _ = Assert.NotNull(result);
        Assert.True(Math.Abs(result.Value - 78.9f) < 0.1f);
    }

    [Fact]
    public void GetNullableFloat_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableFloat FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableFloat("NullableFloat", 777.7f);

        _ = Assert.NotNull(result);
        Assert.True(Math.Abs(result.Value - 777.7f) < 0.1f);
    }

    #endregion

    #region GetNullableGuid Tests

    [Fact]
    public void GetNullableGuid_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableGuid FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableGuid(0);

        Assert.Equal(Guid.Parse("550e8400-e29b-41d4-a716-446655440000"), result);
    }

    [Fact]
    public void GetNullableGuid_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableGuid FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var defaultGuid = Guid.Parse("00000000-0000-0000-0000-000000000001");
        var result = reader.GetNullableGuid("NullableGuid", defaultGuid);

        Assert.Equal(defaultGuid, result);
    }

    #endregion

    #region GetNullableInt16 Tests

    [Fact]
    public void GetNullableInt16_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt16 FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableInt16(0);

        Assert.Equal((short)32767, result);
    }

    [Fact]
    public void GetNullableInt16_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt16 FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableInt16("NullableInt16", (short)999);

        Assert.Equal((short)999, result);
    }

    [Fact]
    public void GetNullableInt16_ByName_WithNegativeValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt16 FROM TestData WHERE Id = 3";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableInt16("NullableInt16");

        Assert.Equal((short)-32768, result);
    }

    #endregion

    #region GetNullableInt32 Tests

    [Fact]
    public void GetNullableInt32_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt32 FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableInt32(0);

        Assert.Equal(2147483647, result);
    }

    [Fact]
    public void GetNullableInt32_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt32 FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableInt32("NullableInt32", 12345);

        Assert.Equal(12345, result);
    }

    [Fact]
    public void GetNullableInt32_ByName_WithNegativeValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt32 FROM TestData WHERE Id = 3";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableInt32("NullableInt32");

        Assert.Equal(-2147483648, result);
    }

    #endregion

    #region GetNullableInt64 Tests

    [Fact]
    public void GetNullableInt64_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt64 FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableInt64(0);

        Assert.Equal(9223372036854775807L, result);
    }

    [Fact]
    public void GetNullableInt64_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt64 FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableInt64("NullableInt64", 987654321L);

        Assert.Equal(987654321L, result);
    }

    [Fact]
    public void GetNullableInt64_ByName_WithNegativeValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableInt64 FROM TestData WHERE Id = 3";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableInt64("NullableInt64");

        Assert.Equal(-9223372036854775808L, result);
    }

    #endregion

    #region GetNullableString Tests

    [Fact]
    public void GetNullableString_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableString FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableString(0);

        Assert.Equal("Not Null", result);
    }

    [Fact]
    public void GetNullableString_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableString FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableString("NullableString", "Default String");

        Assert.Equal("Default String", result);
    }

    [Fact]
    public void GetNullableString_ByName_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableString FROM TestData WHERE Id = 3";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableString("NullableString");

        Assert.Equal("Another String", result);
    }

    #endregion

    #region GetNullableValue Tests

    [Fact]
    public void GetNullableValue_ByIndex_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT Name FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableValue(0);

        Assert.Equal("John Doe", result);
    }

    [Fact]
    public void GetNullableValue_ByName_WithNullValue_ReturnsDefaultValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableString FROM TestData WHERE Id = 2";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableValue("NullableString", "Default Object");

        Assert.Equal("Default Object", result);
    }

    [Fact]
    public void GetNullableValue_ByName_WithValidValue_ReturnsCorrectValue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT Age FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());
        var result = reader.GetNullableValue("Age");

        Assert.Equal(30L, result); // SQLite returns INTEGER as long
    }

    #endregion

    #region Error Handling Tests

    [Fact]
    public void GetNullableBoolean_WithNullRecord_ThrowsArgumentNullException()
    {
        IDataRecord record = null!;

        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableBoolean(0));

        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableBoolean_WithNullName_ThrowsArgumentNullException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        var exception = Assert.Throws<ArgumentNullException>(() => reader.GetNullableBoolean(null!));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableBoolean_WithEmptyName_ThrowsArgumentException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        var exception = Assert.Throws<ArgumentException>(() => reader.GetNullableBoolean(""));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableBoolean_WithInvalidIndex_ThrowsArgumentOutOfRangeException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        _ = Assert.Throws<ArgumentOutOfRangeException>(() => reader.GetNullableBoolean(10));
    }

    [Fact]
    public void GetNullableBoolean_WithInvalidColumnName_ThrowsArgumentOutOfRangeException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT NullableBoolean FROM TestData WHERE Id = 1";
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

        _ = Assert.Throws<ArgumentOutOfRangeException>(() => reader.GetNullableBoolean("NonExistentColumn"));
    }

    #endregion

    #region Multiple Column Tests

    [Fact]
    public void GetNullableValues_MultipleColumns_ReturnsCorrectValues()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = """
            SELECT 
                NullableBoolean, NullableByte, NullableChar, NullableDateTime,
                NullableDecimal, NullableDouble, NullableFloat, NullableGuid,
                NullableInt16, NullableInt32, NullableInt64, NullableString
            FROM TestData WHERE Id = 1
            """;
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

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

        Assert.True(boolean);
        Assert.Equal((byte)255, byteValue);
        Assert.Equal('A', charValue);
        Assert.Equal(DateTime.Parse("2023-01-15 10:30:00", CultureInfo.InvariantCulture), dateTime);
        Assert.Equal(12345.67m, decimalValue);
        _ = Assert.NotNull(doubleValue);
        Assert.True(Math.Abs(doubleValue.Value - 123.456) < 0.001);
        _ = Assert.NotNull(floatValue);
        Assert.True(Math.Abs(floatValue.Value - 78.9f) < 0.1f);
        Assert.Equal(Guid.Parse("550e8400-e29b-41d4-a716-446655440000"), guidValue);
        Assert.Equal((short)32767, int16Value);
        Assert.Equal(2147483647, int32Value);
        Assert.Equal(9223372036854775807L, int64Value);
        Assert.Equal("Not Null", stringValue);
    }

    [Fact]
    public void GetNullableValues_AllNullColumns_ReturnsDefaultValues()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = """
            SELECT 
                NullableBoolean, NullableByte, NullableChar, NullableDateTime,
                NullableDecimal, NullableDouble, NullableFloat, NullableGuid,
                NullableInt16, NullableInt32, NullableInt64, NullableString
            FROM TestData WHERE Id = 2
            """;
        using var reader = command.ExecuteReader();

        Assert.True(reader.Read());

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

        Assert.True(boolean);
        Assert.Equal((byte)100, byteValue);
        Assert.Equal('X', charValue);
        Assert.Equal(new DateTime(2023, 12, 31, 0, 0, 0, DateTimeKind.Utc), dateTime);
        Assert.Equal(999.99m, decimalValue);
        _ = Assert.NotNull(doubleValue);
        Assert.True(Math.Abs(doubleValue.Value - 888.888) < 0.001);
        _ = Assert.NotNull(floatValue);
        Assert.True(Math.Abs(floatValue.Value - 777.7f) < 0.1f);
        Assert.Equal(Guid.Empty, guidValue);
        Assert.Equal((short)123, int16Value);
        Assert.Equal(456, int32Value);
        Assert.Equal(789L, int64Value);
        Assert.Equal("Default", stringValue);
    }

    #endregion
}
