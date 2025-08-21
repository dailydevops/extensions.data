# NetEvolve.Extensions.Data

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![NuGet](https://img.shields.io/nuget/v/NetEvolve.Extensions.Data.svg)](https://www.nuget.org/packages/NetEvolve.Extensions.Data/)

**NetEvolve.Extensions.Data** is a powerful .NET library that provides essential extension methods for working with data access components from the `System.Data` namespace. The library is designed to simplify data handling operations, reduce boilerplate code, and improve developer productivity when working with databases in .NET applications.

## 🎯 Overview

This package contains multiple extensions for native .NET components, especially for the `System.Data` namespace. The main goal is to provide a set of time-saving methods that simplify development in the context of data handling, with a focus on:

- **Null-safe data retrieval** from database readers and records
- **Type-safe generic methods** for accessing field values
- **Async/await support** for modern asynchronous programming patterns
- **Column existence checking** to avoid runtime exceptions
- **Default value handling** for nullable database fields

## 🚀 Features

### IDataReader Extensions
- **HasColumn()** - Check if a column exists in the data reader before accessing it
- Prevents `IndexOutOfRangeException` when working with dynamic queries

### IDataRecord Extensions
- **GetNullable\*()** methods for all primitive types (Boolean, Byte, Char, DateTime, Decimal, Double, Float, Guid, Int16, Int32, Int64, String)
- Safe retrieval of nullable values with configurable default values
- Support for both column index and column name access

### DbDataReader Extensions
- **GetFieldValue\<T\>()** by column name (generic type-safe access)
- **GetFieldValueAsync\<T\>()** for asynchronous operations
- **GetFieldValueOrDefault\<T\>()** with fallback values for null columns
- **GetFieldValueOrDefaultAsync\<T\>()** for async operations with defaults

## 📦 Installation

### .NET CLI

```bash
dotnet add package NetEvolve.Extensions.Data
```

### PackageReference

```xml
<PackageReference Include="NetEvolve.Extensions.Data" Version="1.0.0" />
```

## 🛠️ Requirements

- **.NET Standard 2.0** (compatible with .NET Framework 4.6.1+, .NET Core 2.0+, .NET 5+)
- **Multi-target support**: .NET 8, .NET 9

## 📖 Usage

All extension methods are available in the `NetEvolve.Extensions.Data` namespace.
```csharp
using NetEvolve.Extensions.Data;
```

### IDataReader Extensions

#### Check if Column Existsusing var command = connection.CreateCommand();
command.CommandText = "SELECT Id, Name, Email FROM Users";
using var reader = command.ExecuteReader();

while (reader.Read())
{
    // Safe column checking before access
    if (reader.HasColumn("Email"))
    {
        var email = reader.GetString("Email");
    }
    
    // Avoid runtime exceptions for optional columns
    var phoneExists = reader.HasColumn("Phone"); // Returns false if column doesn't exist
}
### IDataRecord Extensions

#### Null-Safe Value Retrievalusing var command = connection.CreateCommand();
command.CommandText = "SELECT Id, Name, Age, IsActive, Salary, CreatedDate FROM Users";
using var reader = command.ExecuteReader();

while (reader.Read())
{
    var id = reader.GetInt32("Id");                                    // Regular access for non-null columns
    var name = reader.GetString("Name");
    
    // Null-safe access with default values
    var age = reader.GetNullableInt32("Age");                         // Returns null if DBNull
    var ageWithDefault = reader.GetNullableInt32("Age", 0);           // Returns 0 if DBNull
    
    var isActive = reader.GetNullableBoolean("IsActive", false);      // Returns false if DBNull
    var salary = reader.GetNullableDecimal("Salary");                 // Returns null if DBNull
    var createdDate = reader.GetNullableDateTime("CreatedDate");      // Returns null if DBNull
    
    // Using column index instead of name
    var emailByIndex = reader.GetNullableString(3, "no-email@example.com");
}
#### Working with All Supported Types// Nullable primitive types with default values
var nullableBool = record.GetNullableBoolean("IsActive", false);
var nullableByte = record.GetNullableByte("StatusCode", 0);
var nullableChar = record.GetNullableChar("Grade", 'N');
var nullableDateTime = record.GetNullableDateTime("LastLogin", DateTime.Now);
var nullableDecimal = record.GetNullableDecimal("Price", 0.0m);
var nullableDouble = record.GetNullableDouble("Rating", 0.0);
var nullableFloat = record.GetNullableFloat("Score", 0.0f);
var nullableGuid = record.GetNullableGuid("UniqueId", Guid.Empty);
var nullableInt16 = record.GetNullableInt16("SmallNumber", 0);
var nullableInt32 = record.GetNullableInt32("Count", 0);
var nullableInt64 = record.GetNullableInt64("BigNumber", 0L);
var nullableString = record.GetNullableString("Description", "N/A");
var nullableValue = record.GetNullableValue("CustomColumn");
### DbDataReader Extensions

#### Generic Type-Safe Accessusing var command = connection.CreateCommand();
command.CommandText = "SELECT Id, Name, CreatedDate, IsActive FROM Users";
using var reader = command.ExecuteReader();

while (reader.Read())
{
    // Generic type-safe access by column name
    var id = reader.GetFieldValue<int>("Id");
    var name = reader.GetFieldValue<string>("Name");
    var createdDate = reader.GetFieldValue<DateTime>("CreatedDate");
    var isActive = reader.GetFieldValue<bool>("IsActive");
    
    // With default values for nullable columns
    var email = reader.GetFieldValueOrDefault<string>("Email", "no-email@example.com");
    var lastLogin = reader.GetFieldValueOrDefault<DateTime?>("LastLogin");
    
    // Using column ordinal
    var idByOrdinal = reader.GetFieldValueOrDefault<int>(0, -1);
}
#### Async Operationsusing var command = connection.CreateCommand();
command.CommandText = "SELECT Id, Name, Data FROM LargeTable";
using var reader = await command.ExecuteReaderAsync();

while (await reader.ReadAsync())
{
    // Async field value retrieval
    var id = await reader.GetFieldValueAsync<int>("Id");
    var name = await reader.GetFieldValueAsync<string>("Name");
    
    // Async with default values
    var data = await reader.GetFieldValueOrDefaultAsync<byte[]>("Data", Array.Empty<byte>());
    
    // With cancellation token
    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
    var largeField = await reader.GetFieldValueOrDefaultAsync<string>("LargeTextField", "", cts.Token);
}
## 🏗️ Real-World Example
public class UserService
{
    private readonly IDbConnection _connection;
    
    public UserService(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = new List<User>();
        
        using var command = _connection.CreateCommand();
        command.CommandText = """
            SELECT 
                Id, 
                FirstName, 
                LastName, 
                Email, 
                Phone, 
                IsActive, 
                CreatedDate, 
                LastLoginDate,
                ProfileImageData
            FROM Users 
            WHERE IsDeleted = 0
            """;
            
        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        
        while (await reader.ReadAsync(cancellationToken))
        {
            var user = new User
            {
                Id = reader.GetFieldValue<int>("Id"),
                FirstName = reader.GetFieldValue<string>("FirstName"),
                LastName = reader.GetFieldValueOrDefault<string>("LastName", ""),
                Email = reader.GetNullableString("Email"),
                Phone = reader.GetNullableString("Phone", "N/A"),
                IsActive = reader.GetNullableBoolean("IsActive", true),
                CreatedDate = reader.GetFieldValue<DateTime>("CreatedDate"),
                LastLoginDate = reader.GetNullableDateTime("LastLoginDate"),
                ProfileImage = await reader.GetFieldValueOrDefaultAsync<byte[]>("ProfileImageData", 
                    Array.Empty<byte>(), cancellationToken)
            };
            
            users.Add(user);
        }
        
        return users;
    }
}

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public byte[]? ProfileImage { get; set; }
}
## 📚 API Reference

### IDataReaderExtensions

| Method | Description |
|--------|-------------|
| `HasColumn(string name)` | Determines whether the data reader contains a column with the specified name |

### IDataRecordExtensions

| Method | Description |
|--------|-------------|
| `GetNullableBoolean(int i, bool? defaultValue = null)` | Retrieves a nullable Boolean from column index |
| `GetNullableBoolean(string name, bool? defaultValue = null)` | Retrieves a nullable Boolean from column name |
| `GetNullableByte(int i, byte? defaultValue = null)` | Retrieves a nullable Byte from column index |
| `GetNullableByte(string name, byte? defaultValue = null)` | Retrieves a nullable Byte from column name |
| `GetNullableChar(int i, char? defaultValue = null)` | Retrieves a nullable Char from column index |
| `GetNullableChar(string name, char? defaultValue = null)` | Retrieves a nullable Char from column name |
| `GetNullableDateTime(int i, DateTime? defaultValue = null)` | Retrieves a nullable DateTime from column index |
| `GetNullableDateTime(string name, DateTime? defaultValue = null)` | Retrieves a nullable DateTime from column name |
| `GetNullableDecimal(int i, decimal? defaultValue = null)` | Retrieves a nullable Decimal from column index |
| `GetNullableDecimal(string name, decimal? defaultValue = null)` | Retrieves a nullable Decimal from column name |
| `GetNullableDouble(int i, double? defaultValue = null)` | Retrieves a nullable Double from column index |
| `GetNullableDouble(string name, double? defaultValue = null)` | Retrieves a nullable Double from column name |
| `GetNullableFloat(int i, float? defaultValue = null)` | Retrieves a nullable Float from column index |
| `GetNullableFloat(string name, float? defaultValue = null)` | Retrieves a nullable Float from column name |
| `GetNullableGuid(int i, Guid? defaultValue = null)` | Retrieves a nullable Guid from column index |
| `GetNullableGuid(string name, Guid? defaultValue = null)` | Retrieves a nullable Guid from column name |
| `GetNullableInt16(int i, short? defaultValue = null)` | Retrieves a nullable Int16 from column index |
| `GetNullableInt16(string name, short? defaultValue = null)` | Retrieves a nullable Int16 from column name |
| `GetNullableInt32(int i, int? defaultValue = null)` | Retrieves a nullable Int32 from column index |
| `GetNullableInt32(string name, int? defaultValue = null)` | Retrieves a nullable Int32 from column name |
| `GetNullableInt64(int i, long? defaultValue = null)` | Retrieves a nullable Int64 from column index |
| `GetNullableInt64(string name, long? defaultValue = null)` | Retrieves a nullable Int64 from column name |
| `GetNullableString(int i, string? defaultValue = null)` | Retrieves a nullable String from column index |
| `GetNullableString(string name, string? defaultValue = null)` | Retrieves a nullable String from column name |
| `GetNullableValue(int i, object? defaultValue = null)` | Retrieves a nullable Object from column index |
| `GetNullableValue(string name, object? defaultValue = null)` | Retrieves a nullable Object from column name |

### DbDataReaderExtensions

| Method | Description |
|--------|-------------|
| `GetFieldValue<T>(string name)` | Gets the value of the specified column as the requested type |
| `GetFieldValueAsync<T>(string name, CancellationToken cancellationToken = default)` | Asynchronously gets the value of the specified column as the requested type |
| `GetFieldValueOrDefault<T>(int ordinal, T defaultValue = default!)` | Gets the value of the specified column or the default value if null |
| `GetFieldValueOrDefault<T>(string name, T defaultValue = default!)` | Gets the value of the specified column or the default value if null |
| `GetFieldValueOrDefaultAsync<T>(int ordinal, T defaultValue = default!, CancellationToken cancellationToken = default)` | Asynchronously gets the value of the specified column or the default value if null |
| `GetFieldValueOrDefaultAsync<T>(string name, T defaultValue = default!, CancellationToken cancellationToken = default)` | Asynchronously gets the value of the specified column or the default value if null |

## 🤝 Contributing

Contributions are welcome! Please feel free to submit issues, feature requests, or pull requests.

## 🔗 Related Packages

This package is part of the **NetEvolve** ecosystem of .NET extensions and utilities. Check out other packages in the NetEvolve family for additional functionality.

---

**Made with ❤️ by the NetEvolve Team**
