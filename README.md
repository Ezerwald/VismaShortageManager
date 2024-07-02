# VismaShortageManager

## Overview

VismaShortageManager is a console application designed to help manage and track shortages in various categories and rooms. It provides functionality to add, list, and delete shortages, as well as apply filters to view specific subsets of shortages. The application stores shortages inforamtion in JSON file and retains data between restarts. The program utilizes dependency injection via `Microsoft.Extensions.DependencyInjection` for better modularity and testability. Comprehensive unit tests are provided to ensure the reliability of the application.

### Disclaimer
*This project is not affiliated with any company and any product. It is a test task created by a student.*

## Features

- **Add Shortages**: Add details about shortages including title, category, room, and priority.
- **List Shortages**: View a list of all shortages with options to filter by title, date range, category, and room.
- **Delete Shortages**: Remove shortages from the list.
- **Filter Shortages**: Apply multiple filters to narrow down the list of shortages.
- **User Management**: Set the current user specifying permission level for actions such as adding or deleting shortages.

## Programming Patterns Used

- **Command Pattern**:
Utilized to encapsulate actions as objects implementing `IConsoleCommand`. This pattern separates requesters from executors, enhancing flexibility.

- **Repository Pattern**:
Abstracts data access operations through `IShortageRepository`, separating business logic from data storage processes managed by `JsonFileHandler.cs`. Enhances maintainability and scalability by providing a clear abstraction layer for interacting with data storage.

## Dependency Injection

This project uses `Microsoft.Extensions.DependencyInjection` for managing dependencies. This allows for better separation of concerns and easier unit testing. The following services and commands are registered:

- **Services**:
  - `ShortageRepository`
  - `ShortageService`
  - `InputParser`

- **Commands**:
  - `ListShortagesCommand`
  - `AddShortageCommand`
  - `DeleteShortageCommand`

Dependency injection is set up in the `Program.cs` file:

```csharp
var serviceProvider = new ServiceCollection()
    .AddSingleton<IShortageRepository>(new ShortageRepository("shortages.json"))
    .AddTransient<IShortageService, ShortageService>()
    .AddTransient<IInputParser, InputParser>()
    .AddCommands() // Register all commands dynamically
    .AddTransient<ConsoleApp>()
    .BuildServiceProvider();
```

In the `ServiceCollectionExtensions.cs` file, the `AddCommands` method is implemented to register all classes that implement the `IConsoleCommand` interface:

```csharp
public static IServiceCollection AddCommands(this IServiceCollection services)
{
    var commandTypes = typeof(Program).Assembly.GetTypes()
        .Where(t => typeof(IConsoleCommand).IsAssignableFrom(t) && !t.IsInterface);

    foreach (var commandType in commandTypes)
    {
        services.AddTransient(commandType);
    }

    return services;
}
```

## Unit Testing

The project includes comprehensive unit tests to ensure the functionality and reliability of the application. The tests cover various components, including commands, services, and helpers. Tests are organized in the `VismaShortageManager.Test` project, which mirrors the structure of the main project.
To facilitate testing, the project makes extensive use of the Moq framework to create mocks for dependencies. This allows for isolation of the units under test, ensuring that the tests are focused on the functionality of the specific components without interference from external dependencies.

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

## Summary

This project is designed to manage shortages within a system using C# and .NET technologies. It utilizes the Command and Repository patterns to maintain separation of concerns and facilitate extensibility. The Command pattern encapsulates actions as objects through IConsoleCommand, allowing flexible command execution. The Repository pattern abstracts data access via IShortageRepository, ensuring clear separation between business logic and data storage. Dependency injection using Microsoft.Extensions.DependencyInjection enhances modularity and testability. Unit testing with xUnit and Moq validates functionality and ensures reliability.

