namespace NetEvolve.Extensions.Data.Tests.Integration;

using System.Data;
using Microsoft.Data.Sqlite;
using Xunit;

public sealed class IDataReaderExtensionsIntegrationTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private bool _disposed;

    public IDataReaderExtensionsIntegrationTests()
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
            CREATE TABLE TestTable (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL,
                Email TEXT,
                Age INTEGER,
                IsActive BOOLEAN,
                CreatedDate TEXT
            )
            """;
        _ = command.ExecuteNonQuery();
    }

    private void InsertTestData()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = """
            INSERT INTO TestTable (Id, Name, Email, Age, IsActive, CreatedDate)
            VALUES 
                (1, 'John Doe', 'john@example.com', 30, 1, '2023-01-15'),
                (2, 'Jane Smith', 'jane@example.com', 25, 0, '2023-02-20'),
                (3, 'Bob Johnson', NULL, 35, 1, '2023-03-10')
            """;
        _ = command.ExecuteNonQuery();
    }

    [Fact]
    public void HasColumn_ExistingColumns_ReturnsTrue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = command.ExecuteReader();

        var hasId = reader.HasColumn("Id");
        var hasName = reader.HasColumn("Name");
        var hasEmail = reader.HasColumn("Email");
        var hasAge = reader.HasColumn("Age");
        var hasIsActive = reader.HasColumn("IsActive");
        var hasCreatedDate = reader.HasColumn("CreatedDate");

        Assert.True(hasId);
        Assert.True(hasName);
        Assert.True(hasEmail);
        Assert.True(hasAge);
        Assert.True(hasIsActive);
        Assert.True(hasCreatedDate);
    }

    [Fact]
    public void HasColumn_NonExistingColumns_ReturnsFalse()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = command.ExecuteReader();

        var hasNonExistent = reader.HasColumn("NonExistentColumn");
        var hasWrongCase = reader.HasColumn("WRONGCASE");

        Assert.False(hasNonExistent);
        Assert.False(hasWrongCase);
    }

    [Fact]
    public void HasColumn_CaseInsensitiveMatching_ReturnsTrue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = command.ExecuteReader();

        var hasIdLowercase = reader.HasColumn("id");
        var hasNameUppercase = reader.HasColumn("NAME");
        var hasEmailMixedCase = reader.HasColumn("EmAiL");

        Assert.True(hasIdLowercase);
        Assert.True(hasNameUppercase);
        Assert.True(hasEmailMixedCase);
    }

    [Fact]
    public void HasColumn_SubsetOfSelectedColumns_ReturnsCorrectResults()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, Email FROM TestTable";
        using var reader = command.ExecuteReader();

        var hasId = reader.HasColumn("Id");
        var hasName = reader.HasColumn("Name");
        var hasEmail = reader.HasColumn("Email");
        var hasAge = reader.HasColumn("Age"); // Not selected
        var hasIsActive = reader.HasColumn("IsActive"); // Not selected

        Assert.True(hasId);
        Assert.True(hasName);
        Assert.True(hasEmail);
        Assert.False(hasAge);
        Assert.False(hasIsActive);
    }

    [Fact]
    public void HasColumn_AliasedColumns_ReturnsCorrectResults()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT Id AS UserId, Name AS FullName FROM TestTable";
        using var reader = command.ExecuteReader();

        var hasUserId = reader.HasColumn("UserId");
        var hasFullName = reader.HasColumn("FullName");
        var hasId = reader.HasColumn("Id"); // Original column name
        var hasName = reader.HasColumn("Name"); // Original column name

        Assert.True(hasUserId);
        Assert.True(hasFullName);
        Assert.False(hasId);
        Assert.False(hasName);
    }

    [Fact]
    public void HasColumn_ComputedColumns_ReturnsCorrectResults()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) AS TotalCount, MAX(Age) AS MaxAge FROM TestTable";
        using var reader = command.ExecuteReader();

        var hasTotalCount = reader.HasColumn("TotalCount");
        var hasMaxAge = reader.HasColumn("MaxAge");
        var hasId = reader.HasColumn("Id"); // Not in result set

        Assert.True(hasTotalCount);
        Assert.True(hasMaxAge);
        Assert.False(hasId);
    }

    [Theory]
    [InlineData("Id")]
    [InlineData("Name")]
    [InlineData("Email")]
    [InlineData("Age")]
    [InlineData("IsActive")]
    [InlineData("CreatedDate")]
    public void HasColumn_ValidColumnNames_ReturnsTrue(string columnName)
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = command.ExecuteReader();

        var hasColumn = reader.HasColumn(columnName);

        Assert.True(hasColumn);
    }

    [Theory]
    [InlineData("NonExistent")]
    [InlineData("WrongColumn")]
    [InlineData("InvalidName")]
    public void HasColumn_InvalidColumnNames_ReturnsFalse(string columnName)
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = command.ExecuteReader();

        var hasColumn = reader.HasColumn(columnName);

        Assert.False(hasColumn);
    }

    [Fact]
    public void HasColumn_NullReader_ThrowsArgumentNullException()
    {
        IDataReader reader = null!;

        var exception = Assert.Throws<ArgumentNullException>(() => reader.HasColumn("Id"));

        Assert.Equal("reader", exception.ParamName);
    }

    [Fact]
    public void HasColumn_NullColumnName_ThrowsArgumentNullException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = command.ExecuteReader();

        var exception = Assert.Throws<ArgumentNullException>(() => reader.HasColumn(null!));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void HasColumn_WhitespaceColumnName_ThrowsArgumentException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = command.ExecuteReader();

        var exception = Assert.Throws<ArgumentException>(() => reader.HasColumn("   "));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void HasColumn_DisposedReader_ThrowsInvalidOperationException()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        var reader = command.ExecuteReader();
        reader.Dispose();

        _ = Assert.Throws<InvalidOperationException>(() => reader.HasColumn("Id"));
    }
}
