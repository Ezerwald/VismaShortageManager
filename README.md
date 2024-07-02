# VismaShortageManager

## Overview

VismaShortageManager is a console application designed to help manage and track shortages in various categories and rooms. It provides functionality to add, list, and delete shortages, as well as apply filters to view specific subsets of shortages. The application utilizes dependency injection via `Microsoft.Extensions.DependencyInjection` for better modularity and testability. Comprehensive unit tests are provided to ensure the reliability of the application.

## Features

- **Add Shortages**: Add details about shortages including title, category, room, and priority.
- **List Shortages**: View a list of all shortages with options to filter by title, date range, category, and room.
- **Delete Shortages**: Remove shortages from the list.
- **Filter Shortages**: Apply multiple filters to narrow down the list of shortages.
- **User Management**: Set the current user for actions such as adding or deleting shortages.

## Dependency Injection

This project uses `Microsoft.Extensions.DependencyInjection` for managing dependencies. This allows for better separation of concerns and easier unit testing. The following services and commands are registered:

- **Services**:
  - `IShortageService`
  - `InputParser`

- **Commands**:
  - `ListShortagesCommand`
  - `AddShortageCommand`
  - `DeleteShortageCommand`

Dependency injection is set up in the `Program.cs` file:

```csharp
var serviceProvider = new ServiceCollection()
    .AddSingleton<IShortageService, ShortageService>()
    .AddSingleton<IInputParser, InputParser>()
    .AddTransient<ListShortagesCommand>()
    .AddTransient<AddShortageCommand>()
    .AddTransient<DeleteShortageCommand>()
    .BuildServiceProvider();
```

## Unit Testing

The project includes comprehensive unit tests to ensure the functionality and reliability of the application. The tests cover various components, including commands, services, and helpers. Tests are organized in the `MyApp.Test` project, which mirrors the structure of the main project.

### Test Collections

Test collections are used to ensure that certain tests run sequentially to avoid conflicts and ensure proper test isolation. The following test collections are defined:

- `AddShortageCommandTests`
- `DeleteShortageCommandTests`
- `ListShortagesCommandTests`
- `DataTests`
- `InputParserTests`
- `MenuHelperTests`
- `UIHelperTests`
- `ServicesTests`

Each test class is assigned to a collection to ensure sequential execution:

```csharp
[Collection("AddShortageCommandTests")]
public class AddShortageCommandTests
{
    // Test methods
}

[Collection("DeleteShortageCommandTests")]
public class DeleteShortageCommandTests
{
    // Test methods
}

// Other test classes...
```

## Disclaimer

This project is not affiliated with any company and any product. It is a test task created by a student.

