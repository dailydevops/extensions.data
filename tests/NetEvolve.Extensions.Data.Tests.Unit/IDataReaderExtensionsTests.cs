namespace NetEvolve.Extensions.Data.Tests.Unit;

using System.Data;
using global::TUnit.Core.Executors;

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
        var reader = IDataReader.Mock();
        string name = null!;

        // Act
        _ = Assert.Throws<ArgumentNullException>("name", () => reader.HasColumn(name));
    }

    [Test]
    public void HasColumn_WhenNameIsEmpty_ThrowsArgumentNullException()
    {
        // Arrange
        var reader = IDataReader.Mock();
        var name = string.Empty;

        // Act
        _ = Assert.Throws<ArgumentException>("name", () => reader.HasColumn(name));
    }

    [Test]
    [MethodDataSource(nameof(HasColumnData))]
    public async Task HasColumn_Theory_Expected(bool expected, string name)
    {
        // Arrange
        var reader = IDataReader.Mock();

        _ = reader.FieldCount.Returns(2);

        _ = reader.GetName(0).Returns("Id");
        _ = reader.GetName(1).Returns("Name");

        // Act
        var result = reader.HasColumn(name);

        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static IEnumerable<(bool, string)> HasColumnData => [(true, "Id"), (true, "namE"), (false, "Mail")];

    /// <summary>
    /// Under the Turkish culture, "i" and "I" do not fold to each other the way they do everywhere
    /// else - the dotted/dotless "I" problem - so a comparison that quietly used the current or
    /// invariant culture instead of <see cref="StringComparison.OrdinalIgnoreCase"/> would answer this
    /// differently depending on the thread's culture. <c>HasColumn</c> must not.
    /// </summary>
    [Test]
    [Culture("tr-TR")]
    public async Task HasColumn_UnderTurkishCulture_StillComparesOrdinally()
    {
        // Arrange
        var reader = IDataReader.Mock();

        _ = reader.FieldCount.Returns(1);
        _ = reader.GetName(0).Returns("ID");

        // Act
        var result = reader.HasColumn("id");

        // Assert
        _ = await Assert.That(result).IsTrue();
    }

    /// <summary>
    /// The Unicode Roman numeral "Ⅰ" (U+2160) carries a compatibility decomposition to the plain
    /// letter "I", which the linguistic comparers behind <see cref="StringComparison.CurrentCultureIgnoreCase"/>
    /// and <see cref="StringComparison.InvariantCultureIgnoreCase"/> fold on - so they treat "Ⅰ" and
    /// "I" as the same letter regardless of thread culture. <see cref="StringComparison.Ordinal"/>
    /// compares raw code points and never applies that folding, so <c>HasColumn</c> must not confuse a
    /// column named "I" with a lookup for "Ⅰ".
    /// </summary>
    [Test]
    public async Task HasColumn_NameIsAUnicodeRomanNumeral_DoesNotMatchThePlainLetterColumn()
    {
        // Arrange
        var reader = IDataReader.Mock();

        _ = reader.FieldCount.Returns(1);
        _ = reader.GetName(0).Returns("I");

        // Act
        var result = reader.HasColumn("Ⅰ");

        // Assert
        _ = await Assert.That(result).IsFalse();
    }
}
