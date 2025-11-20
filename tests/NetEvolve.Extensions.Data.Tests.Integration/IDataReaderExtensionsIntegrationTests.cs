namespace NetEvolve.Extensions.Data.Tests.Integration;

using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

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

    [Test]
    public async Task HasColumn_ExistingColumns_ReturnsTrue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = await command.ExecuteReaderAsync();

        var hasId = reader.HasColumn("Id");
        var hasName = reader.HasColumn("Name");
        var hasEmail = reader.HasColumn("Email");
        var hasAge = reader.HasColumn("Age");
        var hasIsActive = reader.HasColumn("IsActive");
        var hasCreatedDate = reader.HasColumn("CreatedDate");

        using (Assert.Multiple())
        {
            _ = await Assert.That(hasId).IsTrue();
            _ = await Assert.That(hasName).IsTrue();
            _ = await Assert.That(hasEmail).IsTrue();
            _ = await Assert.That(hasEmail).IsTrue();
            _ = await Assert.That(hasAge).IsTrue();
            _ = await Assert.That(hasIsActive).IsTrue();
            _ = await Assert.That(hasCreatedDate).IsTrue();
        }
    }

    [Test]
    public async Task HasColumn_NonExistingColumns_ReturnsFalse()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = await command.ExecuteReaderAsync();

        var hasNonExistent = reader.HasColumn("NonExistentColumn");
        var hasWrongCase = reader.HasColumn("WRONGCASE");

        using (Assert.Multiple())
        {
            _ = await Assert.That(hasNonExistent).IsFalse();
            _ = await Assert.That(hasWrongCase).IsFalse();
        }
    }

    [Test]
    public async Task HasColumn_CaseInsensitiveMatching_ReturnsTrue()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = await command.ExecuteReaderAsync();

        var hasIdLowercase = reader.HasColumn("id");
        var hasNameUppercase = reader.HasColumn("NAME");
        var hasEmailMixedCase = reader.HasColumn("EmAiL");

        using (Assert.Multiple())
        {
            _ = await Assert.That(hasIdLowercase).IsTrue();
            _ = await Assert.That(hasNameUppercase).IsTrue();
            _ = await Assert.That(hasEmailMixedCase).IsTrue();
        }
    }

    [Test]
    public async Task HasColumn_SubsetOfSelectedColumns_ReturnsCorrectResults()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, Email FROM TestTable";
        using var reader = await command.ExecuteReaderAsync();

        var hasId = reader.HasColumn("Id");
        var hasName = reader.HasColumn("Name");
        var hasEmail = reader.HasColumn("Email");
        var hasAge = reader.HasColumn("Age"); // Not selected
        var hasIsActive = reader.HasColumn("IsActive"); // Not selected

        using (Assert.Multiple())
        {
            _ = await Assert.That(hasId).IsTrue();
            _ = await Assert.That(hasName).IsTrue();
            _ = await Assert.That(hasEmail).IsTrue();
            _ = await Assert.That(hasAge).IsFalse();
            _ = await Assert.That(hasIsActive).IsFalse();
        }
    }

    [Test]
    public async Task HasColumn_AliasedColumns_ReturnsCorrectResults()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT Id AS UserId, Name AS FullName FROM TestTable";
        using var reader = await command.ExecuteReaderAsync();

        var hasUserId = reader.HasColumn("UserId");
        var hasFullName = reader.HasColumn("FullName");
        var hasId = reader.HasColumn("Id"); // Original column name
        var hasName = reader.HasColumn("Name"); // Original column name

        using (Assert.Multiple())
        {
            _ = await Assert.That(hasUserId).IsTrue();
            _ = await Assert.That(hasFullName).IsTrue();
            _ = await Assert.That(hasId).IsFalse();
            _ = await Assert.That(hasName).IsFalse();
        }
    }

    [Test]
    public async Task HasColumn_ComputedColumns_ReturnsCorrectResults()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) AS TotalCount, MAX(Age) AS MaxAge FROM TestTable";
        using var reader = await command.ExecuteReaderAsync();

        var hasTotalCount = reader.HasColumn("TotalCount");
        var hasMaxAge = reader.HasColumn("MaxAge");
        var hasId = reader.HasColumn("Id"); // Not in result set

        using (Assert.Multiple())
        {
            _ = await Assert.That(hasTotalCount).IsTrue();
            _ = await Assert.That(hasMaxAge).IsTrue();
            _ = await Assert.That(hasId).IsFalse();
        }
    }

    [Test]
    [Arguments("Id")]
    [Arguments("Name")]
    [Arguments("Email")]
    [Arguments("Age")]
    [Arguments("IsActive")]
    [Arguments("CreatedDate")]
    public async Task HasColumn_ValidColumnNames_ReturnsTrue(string columnName)
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = await command.ExecuteReaderAsync();

        var hasColumn = reader.HasColumn(columnName);

        _ = await Assert.That(hasColumn).IsTrue();
    }

    [Test]
    [Arguments("NonExistent")]
    [Arguments("WrongColumn")]
    [Arguments("InvalidName")]
    public async Task HasColumn_InvalidColumnNames_ReturnsFalse(string columnName)
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = await command.ExecuteReaderAsync();

        var hasColumn = reader.HasColumn(columnName);

        _ = await Assert.That(hasColumn).IsFalse();
    }

    [Test]
    public void HasColumn_NullReader_ThrowsArgumentNullException()
    {
        IDataReader reader = null!;

        _ = Assert.Throws<ArgumentNullException>("reader", () => reader.HasColumn("Id"));
    }

    [Test]
    public void HasColumn_NullColumnName_ThrowsArgumentNullException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = command.ExecuteReader();

        _ = Assert.Throws<ArgumentNullException>("name", () => reader.HasColumn(null!));
    }

    [Test]
    public void HasColumn_WhitespaceColumnName_ThrowsArgumentException()
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        using var reader = command.ExecuteReader();

        _ = Assert.Throws<ArgumentException>("name", () => reader.HasColumn("   "));
    }

    [Test]
    public void HasColumn_DisposedReader_ThrowsInvalidOperationException()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM TestTable";
        var reader = command.ExecuteReader();
        reader.Dispose();

        _ = Assert.Throws<InvalidOperationException>(() => reader.HasColumn("Id"));
    }
}
