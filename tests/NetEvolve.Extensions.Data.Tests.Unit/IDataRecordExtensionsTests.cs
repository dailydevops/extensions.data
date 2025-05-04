namespace NetEvolve.Extensions.Data.Tests.Unit;

using System.Data;
using NSubstitute;

public class IDataRecordExtensionsTests
{
    [Fact]
    public void GetNullableBooleanIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableBoolean(index)
        );

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableBooleanIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() =>
            record.GetNullableBoolean(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableBooleanIndexData))]
    public void GetNullableBooleanIndex_Theory_Expected(
        bool? expected,
        int index,
        bool? defaultValue
    )
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableBoolean(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableBooleanNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableBoolean(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableBooleanNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableBoolean(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableBooleanNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableBoolean(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableBooleanNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() =>
            record.GetNullableBoolean(name)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableBooleanNamedData))]
    public void GetNullableBooleanNamed_Theory_Expected(
        bool? expected,
        string name,
        bool? defaultValue
    )
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableBoolean(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<bool?, int, bool?> GetNullableBooleanIndexData =>
        new TheoryData<bool?, int, bool?>
        {
            { null, 0, null },
            { true, 1, null },
            { false, 2, null },
            { false, 0, false },
            { true, 1, false },
            { false, 2, false },
        };

    public static TheoryData<bool?, string, bool?> GetNullableBooleanNamedData =>
        new TheoryData<bool?, string, bool?>
        {
            { null, "Id", null },
            { true, "Name", null },
            { false, "IsActive", null },
            { false, "Id", false },
            { true, "Name", false },
            { false, "IsActive", false },
        };

    [Fact]
    public void GetNullableByteIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableByte(index));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableByteIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableByte(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableByteIndexData))]
    public void GetNullableByteIndex_Theory_Expected(byte? expected, int index, byte? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableByte(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableByteNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableByte(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableByteNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableByte(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableByteNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableByte(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableByteNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableByte(name));

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableByteNamedData))]
    public void GetNullableByteNamed_Theory_Expected(
        byte? expected,
        string name,
        byte? defaultValue
    )
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableByte(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<byte?, int, byte?> GetNullableByteIndexData =>
        new TheoryData<byte?, int, byte?>
        {
            { null, 0, null },
            { 0x01b, 1, null },
            { 0x00b, 2, null },
            { 0x00b, 0, 0x00b },
            { 0x01b, 1, 0x00b },
            { 0x00b, 2, 0x00b },
        };

    public static TheoryData<byte?, string, byte?> GetNullableByteNamedData =>
        new TheoryData<byte?, string, byte?>
        {
            { null, "Id", null },
            { 0x01b, "Name", null },
            { 0x00b, "IsActive", null },
            { 0x00b, "Id", 0x00b },
            { 0x01b, "Name", 0x00b },
            { 0x00b, "IsActive", 0x00b },
        };

    [Fact]
    public void GetNullableCharIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableChar(index));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableCharIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableChar(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableCharIndexData))]
    public void GetNullableCharIndex_Theory_Expected(char? expected, int index, char? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableChar(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableCharNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableChar(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableCharNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableChar(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableCharNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableChar(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableCharNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableChar(name));

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableCharNamedData))]
    public void GetNullableCharNamed_Theory_Expected(
        char? expected,
        string name,
        char? defaultValue
    )
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableChar(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<char?, int, char?> GetNullableCharIndexData =>
        new TheoryData<char?, int, char?>
        {
            { null, 0, null },
            { '\t', 1, null },
            { ' ', 2, null },
            { ' ', 0, ' ' },
            { '\t', 1, ' ' },
            { ' ', 2, ' ' },
        };

    public static TheoryData<char?, string, char?> GetNullableCharNamedData =>
        new TheoryData<char?, string, char?>
        {
            { null, "Id", null },
            { '\t', "Name", null },
            { ' ', "IsActive", null },
            { ' ', "Id", ' ' },
            { '\t', "Name", ' ' },
            { ' ', "IsActive", ' ' },
        };

    [Fact]
    public void GetNullableDateTimeIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableDateTime(index)
        );

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableDateTimeIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() =>
            record.GetNullableDateTime(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableDateTimeIndexData))]
    public void GetNullableDateTimeIndex_Theory_Expected(
        DateTime? expected,
        int index,
        DateTime? defaultValue
    )
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDateTime(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableDateTimeNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableDateTime(name)
        );

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableDateTimeNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableDateTime(name)
        );

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableDateTimeNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableDateTime(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableDateTimeNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() =>
            record.GetNullableDateTime(name)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableDateTimeNamedData))]
    public void GetNullableDateTimeNamed_Theory_Expected(
        DateTime? expected,
        string name,
        DateTime? defaultValue
    )
    // Arrange
    {
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDateTime(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<DateTime?, int, DateTime?> GetNullableDateTimeIndexData =>
        new TheoryData<DateTime?, int, DateTime?>
        {
            { null, 0, null },
            { DateTime.FromOADate(0), 1, null },
            { DateTime.MinValue, 2, null },
            { DateTime.MinValue, 0, DateTime.MinValue },
            { DateTime.FromOADate(0), 1, DateTime.MinValue },
            { DateTime.MinValue, 2, DateTime.MinValue },
        };

    public static TheoryData<DateTime?, string, DateTime?> GetNullableDateTimeNamedData =>
        new TheoryData<DateTime?, string, DateTime?>
        {
            { null, "Id", null },
            { DateTime.FromOADate(0), "Name", null },
            { DateTime.MinValue, "IsActive", null },
            { DateTime.MinValue, "Id", DateTime.MinValue },
            { DateTime.FromOADate(0), "Name", DateTime.MinValue },
            { DateTime.MinValue, "IsActive", DateTime.MinValue },
        };

    [Fact]
    public void GetNullableDecimalIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableDecimal(index)
        );

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableDecimalIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() =>
            record.GetNullableDecimal(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableDecimalIndexData))]
    public void GetNullableDecimalIndex_Theory_Expected(
        decimal? expected,
        int index,
        decimal? defaultValue
    )
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDecimal(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableDecimalNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableDecimal(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableDecimalNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableDecimal(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableDecimalNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableDecimal(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableDecimalNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() =>
            record.GetNullableDecimal(name)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableDecimalNamedData))]
    public void GetNullableDecimalNamed_Theory_Expected(
        decimal? expected,
        string name,
        decimal? defaultValue
    )
    // Arrange
    {
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDecimal(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<decimal?, int, decimal?> GetNullableDecimalIndexData =>
        new TheoryData<decimal?, int, decimal?>
        {
            { null, 0, null },
            { decimal.MaxValue, 1, null },
            { decimal.MinValue, 2, null },
            { decimal.MinValue, 0, decimal.MinValue },
            { decimal.MaxValue, 1, decimal.MinValue },
            { decimal.MinValue, 2, decimal.MinValue },
        };

    public static TheoryData<decimal?, string, decimal?> GetNullableDecimalNamedData =>
        new TheoryData<decimal?, string, decimal?>
        {
            { null, "Id", null },
            { decimal.MaxValue, "Name", null },
            { decimal.MinValue, "IsActive", null },
            { decimal.MinValue, "Id", decimal.MinValue },
            { decimal.MaxValue, "Name", decimal.MinValue },
            { decimal.MinValue, "IsActive", decimal.MinValue },
        };

    [Fact]
    public void GetNullableDoubleIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableDouble(index));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableDoubleIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() =>
            record.GetNullableDouble(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableDoubleIndexData))]
    public void GetNullableDoubleIndex_Theory_Expected(
        double? expected,
        int index,
        double? defaultValue
    )
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDouble(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableDoubleNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableDouble(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableDoubleNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableDouble(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableDoubleNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableDouble(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableDoubleNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableDouble(name)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableDoubleNamedData))]
    public void GetNullableDoubleNamed_Theory_Expected(
        double? expected,
        string name,
        double? defaultValue
    )
    // Arrange
    {
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDouble(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<double?, int, double?> GetNullableDoubleIndexData =>
        new TheoryData<double?, int, double?>
        {
            { null, 0, null },
            { double.MaxValue, 1, null },
            { double.MinValue, 2, null },
            { double.MinValue, 0, double.MinValue },
            { double.MaxValue, 1, double.MinValue },
            { double.MinValue, 2, double.MinValue },
        };

    public static TheoryData<double?, string, double?> GetNullableDoubleNamedData =>
        new TheoryData<double?, string, double?>
        {
            { null, "Id", null },
            { double.MaxValue, "Name", null },
            { double.MinValue, "IsActive", null },
            { double.MinValue, "Id", double.MinValue },
            { double.MaxValue, "Name", double.MinValue },
            { double.MinValue, "IsActive", double.MinValue },
        };

    [Fact]
    public void GetNullableFloatIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableFloat(index));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableFloatIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableFloat(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableFloatIndexData))]
    public void GetNullableFloatIndex_Theory_Expected(
        float? expected,
        int index,
        float? defaultValue
    )
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableFloat(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableFloatNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableFloat(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableFloatNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableFloat(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableFloatNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableFloat(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableFloatNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableFloat(name)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableFloatNamedData))]
    public void GetNullableFloatNamed_Theory_Expected(
        float? expected,
        string name,
        float? defaultValue
    )
    // Arrange
    {
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableFloat(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<float?, int, float?> GetNullableFloatIndexData =>
        new TheoryData<float?, int, float?>
        {
            { null, 0, null },
            { float.MaxValue, 1, null },
            { float.MinValue, 2, null },
            { float.MinValue, 0, float.MinValue },
            { float.MaxValue, 1, float.MinValue },
            { float.MinValue, 2, float.MinValue },
        };

    public static TheoryData<float?, string, float?> GetNullableFloatNamedData =>
        new TheoryData<float?, string, float?>
        {
            { null, "Id", null },
            { float.MaxValue, "Name", null },
            { float.MinValue, "IsActive", null },
            { float.MinValue, "Id", float.MinValue },
            { float.MaxValue, "Name", float.MinValue },
            { float.MinValue, "IsActive", float.MinValue },
        };

    [Fact]
    public void GetNullableGuidIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableGuid(index));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableGuidIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableGuid(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableGuidIndexData))]
    public void GetNullableGuidIndex_Theory_Expected(Guid? expected, int index, Guid? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableGuid(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableGuidNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableGuid(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableGuidNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableGuid(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableGuidNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableGuid(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableGuidNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableGuid(name));

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableGuidNamedData))]
    public void GetNullableGuidNamed_Theory_Expected(
        Guid? expected,
        string name,
        Guid? defaultValue
    )
    // Arrange
    {
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableGuid(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<Guid?, int, Guid?> GetNullableGuidIndexData =>
        new TheoryData<Guid?, int, Guid?>
        {
            { null, 0, null },
            { _guidMax, 1, null },
            { Guid.Empty, 2, null },
            { Guid.Empty, 0, Guid.Empty },
            { _guidMax, 1, Guid.Empty },
            { Guid.Empty, 2, Guid.Empty },
        };

    public static TheoryData<Guid?, string, Guid?> GetNullableGuidNamedData =>
        new TheoryData<Guid?, string, Guid?>
        {
            { null, "Id", null },
            { _guidMax, "Name", null },
            { Guid.Empty, "IsActive", null },
            { Guid.Empty, "Id", Guid.Empty },
            { _guidMax, "Name", Guid.Empty },
            { Guid.Empty, "IsActive", Guid.Empty },
        };

    [Fact]
    public void GetNullableInt16Index_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableInt16(index));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt16Index_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt16(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableInt16IndexData))]
    public void GetNullableInt16Index_Theory_Expected(
        short? expected,
        int index,
        short? defaultValue
    )
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt16(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableInt16Named_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableInt16(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt16Named_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableInt16(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt16Named_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableInt16(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt16Named_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt16(name)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableInt16NamedData))]
    public void GetNullableInt16Named_Theory_Expected(
        short? expected,
        string name,
        short? defaultValue
    )
    // Arrange
    {
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt16(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<short?, int, short?> GetNullableInt16IndexData =>
        new TheoryData<short?, int, short?>
        {
            { null, 0, null },
            { short.MaxValue, 1, null },
            { short.MinValue, 2, null },
            { short.MinValue, 0, short.MinValue },
            { short.MaxValue, 1, short.MinValue },
            { short.MinValue, 2, short.MinValue },
        };

    public static TheoryData<short?, string, short?> GetNullableInt16NamedData =>
        new TheoryData<short?, string, short?>
        {
            { null, "Id", null },
            { short.MaxValue, "Name", null },
            { short.MinValue, "IsActive", null },
            { short.MinValue, "Id", short.MinValue },
            { short.MaxValue, "Name", short.MinValue },
            { short.MinValue, "IsActive", short.MinValue },
        };

    [Fact]
    public void GetNullableInt32Index_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableInt32(index));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt32Index_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt32(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableInt32IndexData))]
    public void GetNullableInt32Index_Theory_Expected(int? expected, int index, int? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt32(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableInt32Named_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableInt32(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt32Named_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableInt32(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt32Named_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableInt32(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt32Named_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt32(name)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableInt32NamedData))]
    public void GetNullableInt32Named_Theory_Expected(int? expected, string name, int? defaultValue)
    // Arrange
    {
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt32(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<int?, int, int?> GetNullableInt32IndexData =>
        new TheoryData<int?, int, int?>
        {
            { null, 0, null },
            { int.MaxValue, 1, null },
            { int.MinValue, 2, null },
            { int.MinValue, 0, int.MinValue },
            { int.MaxValue, 1, int.MinValue },
            { int.MinValue, 2, int.MinValue },
        };

    public static TheoryData<int?, string, int?> GetNullableInt32NamedData =>
        new TheoryData<int?, string, int?>
        {
            { null, "Id", null },
            { int.MaxValue, "Name", null },
            { int.MinValue, "IsActive", null },
            { int.MinValue, "Id", int.MinValue },
            { int.MaxValue, "Name", int.MinValue },
            { int.MinValue, "IsActive", int.MinValue },
        };

    [Fact]
    public void GetNullableInt64Index_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableInt64(index));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt64Index_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt64(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableInt64IndexData))]
    public void GetNullableInt64Index_Theory_Expected(long? expected, int index, long? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt64(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableInt64Named_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableInt64(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt64Named_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableInt64(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt64Named_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableInt64(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableInt64Named_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt64(name)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableInt64NamedData))]
    public void GetNullableInt64Named_Theory_Expected(
        long? expected,
        string name,
        long? defaultValue
    )
    // Arrange
    {
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt64(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<long?, int, long?> GetNullableInt64IndexData =>
        new TheoryData<long?, int, long?>
        {
            { null, 0, null },
            { long.MaxValue, 1, null },
            { long.MinValue, 2, null },
            { long.MinValue, 0, long.MinValue },
            { long.MaxValue, 1, long.MinValue },
            { long.MinValue, 2, long.MinValue },
        };

    public static TheoryData<long?, string, long?> GetNullableInt64NamedData =>
        new TheoryData<long?, string, long?>
        {
            { null, "Id", null },
            { long.MaxValue, "Name", null },
            { long.MinValue, "IsActive", null },
            { long.MinValue, "Id", long.MinValue },
            { long.MaxValue, "Name", long.MinValue },
            { long.MinValue, "IsActive", long.MinValue },
        };

    [Fact]
    public void GetNullableStringIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableString(index));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableStringIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() =>
            record.GetNullableString(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableStringIndexData))]
    public void GetNullableStringIndex_Theory_Expected(
        string? expected,
        int index,
        string? defaultValue
    )
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableString(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableStringNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableString(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableStringNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableString(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableStringNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableString(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableStringNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableString(name)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableStringNamedData))]
    public void GetNullableStringNamed_Theory_Expected(
        string? expected,
        string name,
        string? defaultValue
    )
    // Arrange
    {
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableString(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<string?, int, string?> GetNullableStringIndexData =>
        new TheoryData<string?, int, string?>
        {
            { null, 0, null },
            { "Hello World!", 1, null },
            { string.Empty, 2, null },
            { string.Empty, 0, string.Empty },
            { "Hello World!", 1, string.Empty },
            { string.Empty, 2, string.Empty },
        };

    public static TheoryData<string?, string, string?> GetNullableStringNamedData =>
        new TheoryData<string?, string, string?>
        {
            { null, "Id", null },
            { "Hello World!", "Name", null },
            { string.Empty, "IsActive", null },
            { string.Empty, "Id", string.Empty },
            { "Hello World!", "Name", "Hello World!" },
            { string.Empty, "IsActive", string.Empty },
        };

    [Fact]
    public void GetNullableValueIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableValue(index));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableValueIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableValue(index)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableValueIndexData))]
    public void GetNullableValueIndex_Theory_Expected(
        object? expected,
        int index,
        object? defaultValue
    )
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableValue(index, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetNullableValueNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableValue(name));

        // Assert
        Assert.Equal("record", exception.ParamName);
    }

    [Fact]
    public void GetNullableValueNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => record.GetNullableValue(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableValueNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => record.GetNullableValue(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void GetNullableValueNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        var exception = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableValue(name)
        );

        // Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [MemberData(nameof(GetNullableValueNamedData))]
    public void GetNullableValueNamed_Theory_Expected(
        object? expected,
        string name,
        object? defaultValue
    )
    // Arrange
    {
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableValue(name, defaultValue);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<object?, int, object?> GetNullableValueIndexData =>
        new TheoryData<object?, int, object?>
        {
            { null, 0, null },
            { "Hello World!", 1, null },
            { string.Empty, 2, null },
            { string.Empty, 0, string.Empty },
            { "Hello World!", 1, string.Empty },
            { string.Empty, 2, string.Empty },
        };

    public static TheoryData<object?, string, object?> GetNullableValueNamedData =>
        new TheoryData<object?, string, object?>
        {
            { null, "Id", null },
            { "Hello World!", "Name", null },
            { string.Empty, "IsActive", null },
            { string.Empty, "Id", string.Empty },
            { "Hello World!", "Name", "Hello World!" },
            { string.Empty, "IsActive", string.Empty },
        };

    private static readonly Guid _guidMax = Guid.NewGuid();

    private static IDataRecord CreateTestRecord()
    {
        var record = Substitute.For<IDataRecord>();

        _ = record.GetOrdinal(Arg.Any<string>()).Returns(-1);
        _ = record.GetOrdinal(Arg.Is("Id")).Returns(0);
        _ = record.GetOrdinal(Arg.Is("Name")).Returns(1);
        _ = record.GetOrdinal(Arg.Is("IsActive")).Returns(2);

        _ = record.IsDBNull(Arg.Any<int>()).Returns(false);
        _ = record.IsDBNull(Arg.Is<int>(i => i < 0)).Throws<IndexOutOfRangeException>();
        _ = record.IsDBNull(Arg.Is(0)).Returns(true);

        _ = record.GetBoolean(Arg.Any<int>()).Returns(false);
        _ = record.GetBoolean(Arg.Is(1)).Returns(true);

        _ = record.GetByte(Arg.Any<int>()).Returns<byte>(0x00b);
        _ = record.GetByte(Arg.Is(1)).Returns<byte>(0x01b);

        _ = record.GetChar(Arg.Any<int>()).Returns(' ');
        _ = record.GetChar(Arg.Is(1)).Returns('\t');

        _ = record.GetDateTime(Arg.Any<int>()).Returns(DateTime.MinValue);
        _ = record.GetDateTime(Arg.Is(1)).Returns(DateTime.FromOADate(0));

        _ = record.GetDecimal(Arg.Any<int>()).Returns(decimal.MinValue);
        _ = record.GetDecimal(Arg.Is(1)).Returns(decimal.MaxValue);

        _ = record.GetDouble(Arg.Any<int>()).Returns(double.MinValue);
        _ = record.GetDouble(Arg.Is(1)).Returns(double.MaxValue);

        _ = record.GetFloat(Arg.Any<int>()).Returns(float.MinValue);
        _ = record.GetFloat(Arg.Is(1)).Returns(float.MaxValue);

        _ = record.GetGuid(Arg.Any<int>()).Returns(Guid.Empty);
        _ = record.GetGuid(Arg.Is(1)).Returns(_guidMax);

        _ = record.GetInt16(Arg.Any<int>()).Returns(short.MinValue);
        _ = record.GetInt16(Arg.Is(1)).Returns(short.MaxValue);

        _ = record.GetInt32(Arg.Any<int>()).Returns(int.MinValue);
        _ = record.GetInt32(Arg.Is(1)).Returns(int.MaxValue);

        _ = record.GetInt64(Arg.Any<int>()).Returns(long.MinValue);
        _ = record.GetInt64(Arg.Is(1)).Returns(long.MaxValue);

        _ = record.GetString(Arg.Any<int>()).Returns(string.Empty);
        _ = record.GetString(Arg.Is(1)).Returns("Hello World!");

        _ = record.GetValue(Arg.Any<int>()).Returns(string.Empty);
        _ = record.GetValue(Arg.Is(1)).Returns("Hello World!");

        return record;
    }
}
