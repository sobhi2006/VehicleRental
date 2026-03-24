# CarRental API

CarRental is an ASP.NET Core Web API built with Clean Architecture + CQRS (MediatR) for managing a full vehicle rental workflow.

It covers:
- master data (`Persons`, `Drivers`, `Vehicles`, `Classifications`, `Currencies`, `FeesBanks`, `Makes`)
- rental lifecycle (`BookingVehicles`, `ReturnVehicles`)
- operations (`DamageVehicles`, `MaintenanceVehicles`)
- finance (`Payments`, `Invoices`, `InvoiceLines`)
- safety/business controls (`BlockListCustomers`)
- image upload management for vehicles and damage reports.

## 1. Technology Stack

- .NET: `net10.0`
- ASP.NET Core Web API
- EF Core `9.0.0`
- MySQL provider: `Pomelo.EntityFrameworkCore.MySql 9.0.0`
- MediatR (CQRS)
- FluentValidation
- AutoMapper
- API Versioning (`Asp.Versioning.Mvc`)
- Swagger (Swashbuckle)
- xUnit + Moq + Coverlet (unit tests)

## 2. Solution Structure

```text
CarRental/
├── CarRental.slnx
├── docker-compose.yml
├── Dockerfile
├── README.md
├── src/
│   ├── CarRental.API/
│   │   ├── Controllers/
│   │   ├── Program.cs
│   │   ├── Request.http
│   │   └── wwwroot/Upload/
│   ├── CarRental.Application/
│   │   ├── Features/
│   │   ├── DTOs/
│   │   ├── Services/
│   │   └── Common/
│   ├── CarRental.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Interfaces/
│   │   └── Common/
│   └── CarRental.Infrastructure/
│       ├── Data/
│       ├── Repositories/
│       └── Migrations/
└── Tests/
      └── CarRental.Tests.Application.UnitTests/
```

## 3. Architecture (How It Works)

### Clean Architecture Layers

- `CarRental.API`: controllers + HTTP pipeline + Swagger + API versioning.
- `CarRental.Application`: commands, queries, validators, mapping profiles, business services.
- `CarRental.Domain`: entities, enums, interfaces, domain contracts.
- `CarRental.Infrastructure`: EF Core `DbContext`, configurations, repositories, migrations.

### Request Flow

1. Request enters a controller (for example `VehiclesController`).
2. Controller sends a MediatR command/query.
3. FluentValidation runs through the MediatR pipeline behavior.
4. Handler calls an Application service.
5. Service uses repositories + unit of work.
6. Result is mapped to DTO and returned.

### Patterns Used

- CQRS: split write (`Commands`) and read (`Queries`).
- Repository + Unit of Work.
- Result pattern for explicit success/failure.
- AutoMapper for entity/DTO mapping.

## 4. API Conventions

- Base route: `api/v{version}/[controller]`
- Current version: `v1`
- Update endpoints: `PUT` without id in route (id comes from body/form command).
- Pagination: `pageNumber`, `pageSize` query parameters.
- Root endpoint `/` returns discovered endpoints count and names.
- Swagger endpoint: `/swagger`

### File Upload Endpoints

These endpoints use `multipart/form-data`:
- `POST /api/v1/Vehicles`
- `PUT /api/v1/Vehicles`
- `POST /api/v1/DamageVehicles`
- `PUT /api/v1/DamageVehicles`

Image files are stored under `src/CarRental.API/wwwroot/Upload/`.

## 5. Main API Modules

Controllers currently available:
- `PersonsController`
- `DriversController`
- `MakesController`
- `ClassificationsController`
- `CurrenciesController`
- `VehiclesController`
- `MaintenanceVehiclesController`
- `BookingVehiclesController`
- `DamageVehiclesController`
- `FeesBanksController`
- `PaymentsController`
- `InvoicesController`
- `ReturnVehiclesController`
- `BlockListCustomersController`

## 6. Business Rules Implemented

### Payments and Invoices

- Payment status is derived by server logic (not supplied by request body).
- Invoice status and paid amount are synchronized from completed payments.
- Net completed payments are aggregated per booking.
- Booking status is synchronized from invoice state:
   - paid invoice -> booking `Completed`
   - unpaid invoice -> booking `Confirmed`
   - cancelled booking remains unchanged.

### Invoice Issuing Rules

- One active invoice per booking.
- Invoice lines are normalized (`LineTotal = Quantity * UnitPrice`).
- Invoice must contain at least one line.
- Invoice can be issued only after full payment coverage.

### Return Vehicle Rules

- One return record per booking.
- Return date cannot be before booking pickup date.
- Optional damage record must belong to the same booking.
- `FeesBankIds` is accepted as a list and linked to return record.
- Return flow updates/creates invoice fee lines for return fees.

## 7. Configuration

### Connection Strings

`src/CarRental.API/appsettings.json`

```json
{
   "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;Database=CarRentalDb;User=root;Password=your_password;"
   }
}
```

`src/CarRental.API/appsettings.Development.json`

```json
{
   "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;Database=CarRentalDev;User=root;Password=sa123456sa;"
   }
}
```

## 8. Run Locally (Step by Step)

1. Go to solution root:

```bash
cd CarRental
```

2. Restore packages:

```bash
dotnet restore CarRental.slnx
```

3. Build:

```bash
dotnet build src/CarRental.API/CarRental.API.csproj
```

4. Make sure MySQL is running and connection string is correct.

5. Run the API:

```bash
dotnet run --project src/CarRental.API/CarRental.API.csproj
```

6. Open Swagger:

```text
http://localhost:5000/swagger
```

## 9. Database and Migrations

The API applies pending migrations automatically at startup in `Program.cs`:

```csharp
await db.Database.MigrateAsync();
```

Manual migration commands:

```bash
dotnet ef migrations add <MigrationName> --project src/CarRental.Infrastructure --startup-project src/CarRental.API
dotnet ef database update --project src/CarRental.Infrastructure --startup-project src/CarRental.API
```

## 10. Docker

### docker-compose services

- `db`: MySQL (`3306`)
- `api`: CarRental API (`5000`)

Start both:

```bash
docker compose up --build
```

API base URL in container mode:

```text
http://localhost:5000
```

## 11. Testing

Run unit tests:

```bash
dotnet test Tests/CarRental.Tests.Application.UnitTests/CarRental.Tests.Application.UnitTests.csproj
```

Current test focus includes person service and person validators.

## 12. Useful Commands

```bash
# Build API project
dotnet build src/CarRental.API/CarRental.API.csproj

# Publish API project
dotnet publish src/CarRental.API/CarRental.API.csproj

# Run with hot reload
dotnet watch run --project src/CarRental.API/CarRental.API.csproj
```

## 13. Request Examples

Use `src/CarRental.API/Request.http` as a quick API collection.

Important contract updates to keep in mind:
- `Payments` create/update requests should not include `status`.
- `Invoices` create/update requests should not include `status` or `paidAmount`.
- `ReturnVehicles` create/update now uses `feesBankIds` list (instead of single excess fee id).
- `Vehicles` and `DamageVehicles` create/update use `multipart/form-data` for images.

### Example: Create Payment

```http
POST /api/v1/Payments
Content-Type: application/json

{
   "bookingId": 1,
   "currencyId": 1,
   "amount": 100.50,
   "type": 0
}
```

### Example: Create Return Vehicle

```http
POST /api/v1/ReturnVehicles
Content-Type: application/json

{
   "bookingId": 1,
   "conditionNotes": "Returned in good condition",
   "actualReturnDate": "2026-03-06T18:00:00Z",
   "mileageAfter": 25000,
   "feesBankIds": [1, 2],
   "damageId": null
}
```

### Example: Create Vehicle With Image (multipart)

```bash
curl -X POST "http://localhost:5000/api/v1/Vehicles" \
   -F "makeId=1" \
   -F "modelYear=2024-01-01" \
   -F "vin=VIN123456" \
   -F "plateNumber=ABC-123" \
   -F "currentMileage=12000" \
   -F "classificationId=1" \
   -F "transmission=1" \
   -F "fuelType=1" \
   -F "doorsNumber=4" \
   -F "status=1" \
   -F "images=@/absolute/path/car.jpg"
```

## 14. Scripts in Repository Root

Outside the `CarRental/` folder there are two helper scripts:

- `CreateProject.sh`: generates a new clean architecture solution template.
- `AddEntity.sh`: scaffolds an entity with DTOs/commands/queries/validators/controller.

These scripts are utility generators and are not required for running the existing CarRental API.

## 15. Notes

- Enums are configured to store as strings in EF Core model configuration.
- API currently has no authentication/authorization flow beyond `UseAuthorization()` middleware registration.
- The codebase contains some historical naming typos in folder names (for example `ReturnVehciles` in some paths), while controller routes are exposed as `ReturnVehicles`.

---

If you want, the next step can be updating `src/CarRental.API/Request.http` so every example is fully aligned with the latest contracts and image upload behavior.
