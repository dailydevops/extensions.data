namespace NetEvolve.Extensions.Data.Tests.Unit;

using System.Data;
using NSubstitute;

public class IDataRecordExtensionsTests
{
    [Test]
    public void GetNullableBooleanIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableBoolean(index));
    }

    [Test]
    public void GetNullableBooleanIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableBoolean(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableBooleanIndexData))]
    public async Task GetNullableBooleanIndex_Theory_Expected(bool? expected, int index, bool? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableBoolean(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableBooleanNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableBoolean(name));
    }

    [Test]
    public void GetNullableBooleanNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableBoolean(name));
    }

    [Test]
    public void GetNullableBooleanNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableBoolean(name));
    }

    [Test]
    public void GetNullableBooleanNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableBoolean(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableBooleanNamedData))]
    public async Task GetNullableBooleanNamed_Theory_Expected(bool? expected, string name, bool? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableBoolean(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(bool?, int, bool?)> GetNullableBooleanIndexData =>
        [(null, 0, null), (true, 1, null), (false, 2, null), (false, 0, false), (true, 1, false), (false, 2, false)];

    public static IEnumerable<(bool?, string, bool?)> GetNullableBooleanNamedData =>
        [
            (null, "Id", null),
            (true, "Name", null),
            (false, "IsActive", null),
            (false, "Id", false),
            (true, "Name", false),
            (false, "IsActive", false),
        ];

    [Test]
    public void GetNullableByteIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableByte(index));
    }

    [Test]
    public void GetNullableByteIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableByte(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableByteIndexData))]
    public async Task GetNullableByteIndex_Theory_Expected(byte? expected, int index, byte? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableByte(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableByteNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableByte(name));
    }

    [Test]
    public void GetNullableByteNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableByte(name));
    }

    [Test]
    public void GetNullableByteNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableByte(name));
    }

    [Test]
    public void GetNullableByteNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableByte(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableByteNamedData))]
    public async Task GetNullableByteNamed_Theory_Expected(byte? expected, string name, byte? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableByte(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(byte?, int, byte?)> GetNullableByteIndexData =>
        [(null, 0, null), (0x01b, 1, null), (0x00b, 2, null), (0x00b, 0, 0x00b), (0x01b, 1, 0x00b), (0x00b, 2, 0x00b)];

    public static IEnumerable<(byte?, string, byte?)> GetNullableByteNamedData =>
        [
            (null, "Id", null),
            (0x01b, "Name", null),
            (0x00b, "IsActive", null),
            (0x00b, "Id", 0x00b),
            (0x01b, "Name", 0x00b),
            (0x00b, "IsActive", 0x00b),
        ];

    [Test]
    public void GetNullableCharIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableChar(index));
    }

    [Test]
    public void GetNullableCharIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableChar(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableCharIndexData))]
    public async Task GetNullableCharIndex_Theory_Expected(char? expected, int index, char? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableChar(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableCharNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableChar(name));
    }

    [Test]
    public void GetNullableCharNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableChar(name));
    }

    [Test]
    public void GetNullableCharNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableChar(name));
    }

    [Test]
    public void GetNullableCharNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableChar(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableCharNamedData))]
    public async Task GetNullableCharNamed_Theory_Expected(char? expected, string name, char? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableChar(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(char?, int, char?)> GetNullableCharIndexData =>
        [(null, 0, null), ('\t', 1, null), (' ', 2, null), (' ', 0, ' '), ('\t', 1, ' '), (' ', 2, ' ')];

    public static IEnumerable<(char?, string, char?)> GetNullableCharNamedData =>
        [
            (null, "Id", null),
            ('\t', "Name", null),
            (' ', "IsActive", null),
            (' ', "Id", ' '),
            ('\t', "Name", ' '),
            (' ', "IsActive", ' '),
        ];

    [Test]
    public void GetNullableDateTimeIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableDateTime(index));
    }

    [Test]
    public void GetNullableDateTimeIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableDateTime(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableDateTimeIndexData))]
    public async Task GetNullableDateTimeIndex_Theory_Expected(DateTime? expected, int index, DateTime? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDateTime(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableDateTimeNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableDateTime(name));
    }

    [Test]
    public void GetNullableDateTimeNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableDateTime(name));
    }

    [Test]
    public void GetNullableDateTimeNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableDateTime(name));
    }

    [Test]
    public void GetNullableDateTimeNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableDateTime(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableDateTimeNamedData))]
    public async Task GetNullableDateTimeNamed_Theory_Expected(DateTime? expected, string name, DateTime? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDateTime(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(DateTime?, int, DateTime?)> GetNullableDateTimeIndexData =>
        [
            (null, 0, null),
            (DateTime.FromOADate(0), 1, null),
            (DateTime.MinValue, 2, null),
            (DateTime.MinValue, 0, DateTime.MinValue),
            (DateTime.FromOADate(0), 1, DateTime.MinValue),
            (DateTime.MinValue, 2, DateTime.MinValue),
        ];

    public static IEnumerable<(DateTime?, string, DateTime?)> GetNullableDateTimeNamedData =>
        [
            (null, "Id", null),
            (DateTime.FromOADate(0), "Name", null),
            (DateTime.MinValue, "IsActive", null),
            (DateTime.MinValue, "Id", DateTime.MinValue),
            (DateTime.FromOADate(0), "Name", DateTime.MinValue),
            (DateTime.MinValue, "IsActive", DateTime.MinValue),
        ];

    [Test]
    public void GetNullableDecimalIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableDecimal(index));
    }

    [Test]
    public void GetNullableDecimalIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableDecimal(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableDecimalIndexData))]
    public async Task GetNullableDecimalIndex_Theory_Expected(decimal? expected, int index, decimal? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDecimal(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableDecimalNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableDecimal(name));
    }

    [Test]
    public void GetNullableDecimalNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableDecimal(name));
    }

    [Test]
    public void GetNullableDecimalNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableDecimal(name));
    }

    [Test]
    public void GetNullableDecimalNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableDecimal(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableDecimalNamedData))]
    public async Task GetNullableDecimalNamed_Theory_Expected(decimal? expected, string name, decimal? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDecimal(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(decimal?, int, decimal?)> GetNullableDecimalIndexData =>
        [
            (null, 0, null),
            (decimal.MaxValue, 1, null),
            (decimal.MinValue, 2, null),
            (decimal.MinValue, 0, decimal.MinValue),
            (decimal.MaxValue, 1, decimal.MinValue),
            (decimal.MinValue, 2, decimal.MinValue),
        ];

    public static IEnumerable<(decimal?, string, decimal?)> GetNullableDecimalNamedData =>
        [
            (null, "Id", null),
            (decimal.MaxValue, "Name", null),
            (decimal.MinValue, "IsActive", null),
            (decimal.MinValue, "Id", decimal.MinValue),
            (decimal.MaxValue, "Name", decimal.MinValue),
            (decimal.MinValue, "IsActive", decimal.MinValue),
        ];

    [Test]
    public void GetNullableDoubleIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableDouble(index));
    }

    [Test]
    public void GetNullableDoubleIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableDouble(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableDoubleIndexData))]
    public async Task GetNullableDoubleIndex_Theory_Expected(double? expected, int index, double? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDouble(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableDoubleNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableDouble(name));
    }

    [Test]
    public void GetNullableDoubleNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableDouble(name));
    }

    [Test]
    public void GetNullableDoubleNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableDouble(name));
    }

    [Test]
    public void GetNullableDoubleNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableDouble(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableDoubleNamedData))]
    public async Task GetNullableDoubleNamed_Theory_Expected(double? expected, string name, double? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableDouble(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(double?, int, double?)> GetNullableDoubleIndexData =>
        [
            (null, 0, null),
            (double.MaxValue, 1, null),
            (double.MinValue, 2, null),
            (double.MinValue, 0, double.MinValue),
            (double.MaxValue, 1, double.MinValue),
            (double.MinValue, 2, double.MinValue),
        ];

    public static IEnumerable<(double?, string, double?)> GetNullableDoubleNamedData =>
        [
            (null, "Id", null),
            (double.MaxValue, "Name", null),
            (double.MinValue, "IsActive", null),
            (double.MinValue, "Id", double.MinValue),
            (double.MaxValue, "Name", double.MinValue),
            (double.MinValue, "IsActive", double.MinValue),
        ];

    [Test]
    public void GetNullableFloatIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableFloat(index));
    }

    [Test]
    public void GetNullableFloatIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableFloat(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableFloatIndexData))]
    public async Task GetNullableFloatIndex_Theory_Expected(float? expected, int index, float? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableFloat(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableFloatNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableFloat(name));
    }

    [Test]
    public void GetNullableFloatNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableFloat(name));
    }

    [Test]
    public void GetNullableFloatNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act & Assert
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableFloat(name));
    }

    [Test]
    public void GetNullableFloatNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableFloat(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableFloatNamedData))]
    public async Task GetNullableFloatNamed_Theory_Expected(float? expected, string name, float? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableFloat(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(float?, int, float?)> GetNullableFloatIndexData =>
        [
            (null, 0, null),
            (float.MaxValue, 1, null),
            (float.MinValue, 2, null),
            (float.MinValue, 0, float.MinValue),
            (float.MaxValue, 1, float.MinValue),
            (float.MinValue, 2, float.MinValue),
        ];

    public static IEnumerable<(float?, string, float?)> GetNullableFloatNamedData =>
        [
            (null, "Id", null),
            (float.MaxValue, "Name", null),
            (float.MinValue, "IsActive", null),
            (float.MinValue, "Id", float.MinValue),
            (float.MaxValue, "Name", float.MinValue),
            (float.MinValue, "IsActive", float.MinValue),
        ];

    [Test]
    public void GetNullableGuidIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableGuid(index));
    }

    [Test]
    public void GetNullableGuidIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableGuid(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableGuidIndexData))]
    public async Task GetNullableGuidIndex_Theory_Expected(Guid? expected, int index, Guid? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableGuid(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableGuidNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableGuid(name));
    }

    [Test]
    public void GetNullableGuidNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableGuid(name));
    }

    [Test]
    public void GetNullableGuidNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act & Assert
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableGuid(name));
    }

    [Test]
    public void GetNullableGuidNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableGuid(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableGuidNamedData))]
    public async Task GetNullableGuidNamed_Theory_Expected(Guid? expected, string name, Guid? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableGuid(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(Guid?, int, Guid?)> GetNullableGuidIndexData =>
        [
            (null, 0, null),
            (_guidMax, 1, null),
            (Guid.Empty, 2, null),
            (Guid.Empty, 0, Guid.Empty),
            (_guidMax, 1, Guid.Empty),
            (Guid.Empty, 2, Guid.Empty),
        ];

    public static IEnumerable<(Guid?, string, Guid?)> GetNullableGuidNamedData =>
        [
            (null, "Id", null),
            (_guidMax, "Name", null),
            (Guid.Empty, "IsActive", null),
            (Guid.Empty, "Id", Guid.Empty),
            (_guidMax, "Name", Guid.Empty),
            (Guid.Empty, "IsActive", Guid.Empty),
        ];

    [Test]
    public void GetNullableInt16Index_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableInt16(index));
    }

    [Test]
    public void GetNullableInt16Index_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt16(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableInt16IndexData))]
    public async Task GetNullableInt16Index_Theory_Expected(short? expected, int index, short? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt16(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableInt16Named_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableInt16(name));
    }

    [Test]
    public void GetNullableInt16Named_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableInt16(name));
    }

    [Test]
    public void GetNullableInt16Named_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act & Assert
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableInt16(name));
    }

    [Test]
    public void GetNullableInt16Named_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt16(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableInt16NamedData))]
    public async Task GetNullableInt16Named_Theory_Expected(short? expected, string name, short? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt16(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(short?, int, short?)> GetNullableInt16IndexData =>
        [
            (null, 0, null),
            (short.MaxValue, 1, null),
            (short.MinValue, 2, null),
            (short.MinValue, 0, short.MinValue),
            (short.MaxValue, 1, short.MinValue),
            (short.MinValue, 2, short.MinValue),
        ];

    public static IEnumerable<(short?, string, short?)> GetNullableInt16NamedData =>
        [
            (null, "Id", null),
            (short.MaxValue, "Name", null),
            (short.MinValue, "IsActive", null),
            (short.MinValue, "Id", short.MinValue),
            (short.MaxValue, "Name", short.MinValue),
            (short.MinValue, "IsActive", short.MinValue),
        ];

    [Test]
    public void GetNullableInt32Index_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableInt32(index));
    }

    [Test]
    public void GetNullableInt32Index_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt32(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableInt32IndexData))]
    public async Task GetNullableInt32Index_Theory_Expected(int? expected, int index, int? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt32(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableInt32Named_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableInt32(name));
    }

    [Test]
    public void GetNullableInt32Named_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableInt32(name));
    }

    [Test]
    public void GetNullableInt32Named_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act & Assert
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableInt32(name));
    }

    [Test]
    public void GetNullableInt32Named_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt32(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableInt32NamedData))]
    public async Task GetNullableInt32Named_Theory_Expected(int? expected, string name, int? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt32(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(int?, int, int?)> GetNullableInt32IndexData =>
        [
            (null, 0, null),
            (int.MaxValue, 1, null),
            (int.MinValue, 2, null),
            (int.MinValue, 0, int.MinValue),
            (int.MaxValue, 1, int.MinValue),
            (int.MinValue, 2, int.MinValue),
        ];

    public static IEnumerable<(int?, string, int?)> GetNullableInt32NamedData =>
        [
            (null, "Id", null),
            (int.MaxValue, "Name", null),
            (int.MinValue, "IsActive", null),
            (int.MinValue, "Id", int.MinValue),
            (int.MaxValue, "Name", int.MinValue),
            (int.MinValue, "IsActive", int.MinValue),
        ];

    [Test]
    public void GetNullableInt64Index_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableInt64(index));
    }

    [Test]
    public void GetNullableInt64Index_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt64(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableInt64IndexData))]
    public async Task GetNullableInt64Index_Theory_Expected(long? expected, int index, long? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt64(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableInt64Named_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableInt64(name));
    }

    [Test]
    public void GetNullableInt64Named_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableInt64(name));
    }

    [Test]
    public void GetNullableInt64Named_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act & Assert
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableInt64(name));
    }

    [Test]
    public void GetNullableInt64Named_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableInt64(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableInt64NamedData))]
    public async Task GetNullableInt64Named_Theory_Expected(long? expected, string name, long? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableInt64(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(long?, int, long?)> GetNullableInt64IndexData =>
        [
            (null, 0, null),
            (long.MaxValue, 1, null),
            (long.MinValue, 2, null),
            (long.MinValue, 0, long.MinValue),
            (long.MaxValue, 1, long.MinValue),
            (long.MinValue, 2, long.MinValue),
        ];

    public static IEnumerable<(long?, string, long?)> GetNullableInt64NamedData =>
        [
            (null, "Id", null),
            (long.MaxValue, "Name", null),
            (long.MinValue, "IsActive", null),
            (long.MinValue, "Id", long.MinValue),
            (long.MaxValue, "Name", long.MinValue),
            (long.MinValue, "IsActive", long.MinValue),
        ];

    [Test]
    public void GetNullableStringIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableString(index));
    }

    [Test]
    public void GetNullableStringIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableString(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableStringIndexData))]
    public async Task GetNullableStringIndex_Theory_Expected(string? expected, int index, string? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableString(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableStringNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableString(name));
    }

    [Test]
    public void GetNullableStringNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableString(name));
    }

    [Test]
    public void GetNullableStringNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act & Assert
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableString(name));
    }

    [Test]
    public void GetNullableStringNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableString(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableStringNamedData))]
    public async Task GetNullableStringNamed_Theory_Expected(string? expected, string name, string? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableString(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(string?, int, string?)> GetNullableStringIndexData =>
        [
            (null, 0, null),
            ("Hello World!", 1, null),
            (string.Empty, 2, null),
            (string.Empty, 0, string.Empty),
            ("Hello World!", 1, string.Empty),
            (string.Empty, 2, string.Empty),
        ];

    public static IEnumerable<(string?, string, string?)> GetNullableStringNamedData =>
        [
            (null, "Id", null),
            ("Hello World!", "Name", null),
            (string.Empty, "IsActive", null),
            (string.Empty, "Id", string.Empty),
            ("Hello World!", "Name", "Hello World!"),
            (string.Empty, "IsActive", string.Empty),
        ];

    [Test]
    public void GetNullableValueIndex_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var index = 0;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableValue(index));
    }

    [Test]
    public void GetNullableValueIndex_WhenIndexIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var index = -1;

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableValue(index));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableValueIndexData))]
    public async Task GetNullableValueIndex_Theory_Expected(object? expected, int index, object? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableValue(index, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public void GetNullableValueNamed_WhenRecordIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataRecord record = null!;
        var name = "name";

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("record", () => record.GetNullableValue(name));
    }

    [Test]
    public void GetNullableValueNamed_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var record = CreateTestRecord();
        string name = null!;

        // Act & Assert
        _ = Assert.Throws<ArgumentNullException>("name", () => record.GetNullableValue(name));
    }

    [Test]
    public void GetNullableValueNamed_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = string.Empty;

        // Act & Assert
        _ = Assert.Throws<ArgumentException>("name", () => record.GetNullableValue(name));
    }

    [Test]
    public void GetNullableValueNamed_WhenNameIsOrdinalNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var record = CreateTestRecord();
        var name = "Mail";

        // Act & Assert
        _ = Assert.Throws<IndexOutOfRangeException>(() => record.GetNullableValue(name));
    }

    [Test]
    [MethodDataSource(nameof(GetNullableValueNamedData))]
    public async Task GetNullableValueNamed_Theory_Expected(object? expected, string name, object? defaultValue)
    {
        // Arrange
        var record = CreateTestRecord();

        // Act
        var result = record.GetNullableValue(name, defaultValue);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(object?, int, object?)> GetNullableValueIndexData =>
        [
            (null, 0, null),
            ("Hello World!", 1, null),
            (string.Empty, 2, null),
            (string.Empty, 0, string.Empty),
            ("Hello World!", 1, string.Empty),
            (string.Empty, 2, string.Empty),
        ];

    public static IEnumerable<(object?, string, object?)> GetNullableValueNamedData =>
        [
            (null, "Id", null),
            ("Hello World!", "Name", null),
            (string.Empty, "IsActive", null),
            (string.Empty, "Id", string.Empty),
            ("Hello World!", "Name", "Hello World!"),
            (string.Empty, "IsActive", string.Empty),
        ];

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
