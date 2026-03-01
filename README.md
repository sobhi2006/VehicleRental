# CarRental

ASP.NET Core Web API following Clean Architecture with MediatR CQRS pattern.

## Project Structure

```
CarRental/
├── src/
│   ├── CarRental.Domain/           # Entities, Interfaces, Value Objects
│   ├── CarRental.Application/      # Commands, Queries, DTOs, Validators (MediatR)
│   ├── CarRental.Infrastructure/   # EF Core, Repositories, DbContext
│   └── CarRental.API/              # Controllers, Program.cs
└── CarRental.slnx
```

## Architecture

This project uses **Clean Architecture** with **CQRS** pattern:

- **Commands**: Create, Update, Delete operations
- **Queries**: Read operations (GetById, GetAll)
- **MediatR**: Request/Response pipeline with validation behaviors
- **FluentValidation**: Request validation
- **Repository Pattern**: Data access abstraction
- **Result Pattern**: Explicit success/failure handling

## Getting Started

### Prerequisites
- .NET 10.0 SDK (preview)
- MySQL (or update connection string for your database)

### Running the Application

1. Update the connection string in `appsettings.json`
2. Run migrations:
   ```bash
   cd src/CarRental.API
   dotnet ef migrations add InitialCreate --project ../CarRental.Infrastructure
   dotnet ef database update
   ```
3. Run the application:
   ```bash
   dotnet run --project src/CarRental.API
   ```
4. Open https://localhost:5001/swagger


This generates:
- Domain Entity
- Repository Interface & Implementation
- DTOs (Create, Update, Read)
- Commands: Create, Update, Delete (with handlers)
- Queries: GetById, GetAll (with handlers)
- Validators for Commands
- API Controller
