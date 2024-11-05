namespace NetEvolve.Extensions.Data.Tests.Unit;

using System.Data;
using NSubstitute;

public class IDataReaderExtensionsTests
{
    [Fact]
    public void HasColumn_WhenReaderIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IDataReader reader = null!;
        var name = "Id";

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => reader.HasColumn(name));

        // Assert
        Assert.Equal("reader", exception.ParamName);
    }

    [Fact]
    public void HasColumn_WhenNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var reader = Substitute.For<IDataReader>();
        string name = null!;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => reader.HasColumn(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void HasColumn_WhenNameIsEmpty_ThrowsArgumentNullException()
    {
        // Arrange
        var reader = Substitute.For<IDataReader>();
        var name = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => reader.HasColumn(name));

        // Assert
        Assert.Equal("name", exception.ParamName);
    }

    [Theory]
    [MemberData(nameof(HasColumnData))]
    public void HasColumn_Theory_Expected(bool expected, string name)
    {
        // Arrange
        var reader = Substitute.For<IDataReader>();

        _ = reader.FieldCount.Returns(2);

        _ = reader.GetName(Arg.Is(0)).Returns("Id");
        _ = reader.GetName(Arg.Is(1)).Returns("Name");

        // Act
        var result = reader.HasColumn(name);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<bool, string> HasColumnData =>
        new TheoryData<bool, string>
        {
            { true, "Id" },
            { true, "namE" },
            { false, "Mail" },
        };
}
