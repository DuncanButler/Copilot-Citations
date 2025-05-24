# Project Citations

This document lists all external sources used in this project.

## Sources

### Microsoft Graph API
**Author:** Microsoft Corporation
**URL:** [https://docs.microsoft.com/en-us/graph/api/overview](https://docs.microsoft.com/en-us/graph/api/overview)
**License:** MIT

Microsoft Graph REST API documentation used for authentication flow implementation

### Repository Pattern with Entity Framework Core
**Author:** Microsoft Documentation Team
**URL:** [https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
**License:** MIT

Implementation pattern for data access layer using Repository pattern with EF Core

### Stack Overflow: Best Practices for Async/Await in C#
**Author:** User: StephenCleary
**URL:** [https://stackoverflow.com/questions/38017016/](https://stackoverflow.com/questions/38017016/)
**License:** CC BY-SA 4.0

Approach for handling asynchronous operations with proper exception handling

## Usage by File

### ..\..\..\..\..\..\..\project\Services\UserService.cs
- Uses: Microsoft Graph API (lines 45-120)
  - Note: Authentication flow implementation based on Microsoft Graph API examples
- Uses: Microsoft Graph API (lines 45-120)
  - Note: Authentication flow implementation based on Microsoft Graph API examples

### ..\..\..\..\..\..\..\project\Data\UserRepository.cs
- Uses: Repository Pattern with Entity Framework Core (lines 15-85)
  - Note: Repository implementation based on Microsoft documentation
- Uses: Repository Pattern with Entity Framework Core (lines 15-85)
  - Note: Repository implementation based on Microsoft documentation

### ..\..\..\..\..\..\..\project\Helpers\AsyncHelper.cs
- Uses: Stack Overflow: Best Practices for Async/Await in C# (lines 25-50)
  - Note: Async execution pattern adapted from Stack Overflow answer
- Uses: Stack Overflow: Best Practices for Async/Await in C# (lines 25-50)
  - Note: Async execution pattern adapted from Stack Overflow answer
