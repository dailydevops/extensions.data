namespace NetEvolve.Extensions.Data.Tests.Unit;

using System.Data;
using NSubstitute;

public class IDataReaderExtensionsTests
{
    [Test]
    public void HasColumn_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataReader reader = null!;
        var name = "Id";

        // Act
        _ = Assert.Throws<ArgumentNullException>("reader", () => reader.HasColumn(name));
    }

    [Test]
    public void HasColumn_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var reader = Substitute.For<IDataReader>();
        string name = null!;

        // Act
        _ = Assert.Throws<ArgumentNullException>("name", () => reader.HasColumn(name));
    }

    [Test]
    public void HasColumn_WhenNameIsEmpty_ThrowsArgumentNullException()
    {
        // Arrange
        var reader = Substitute.For<IDataReader>();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>("name", () => reader.HasColumn(name));
    }

    [Test]
    [MethodDataSource(nameof(HasColumnData))]
    public async Task HasColumn_Theory_Expected(bool expected, string name)
    {
        // Arrange
        var reader = Substitute.For<IDataReader>();

        _ = reader.FieldCount.Returns(2);

        _ = reader.GetName(Arg.Is(0)).Returns("Id");
        _ = reader.GetName(Arg.Is(1)).Returns("Name");

        // Act
        var result = reader.HasColumn(name);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(bool, string)> HasColumnData => [(true, "Id"), (true, "namE"), (false, "Mail")];
}
