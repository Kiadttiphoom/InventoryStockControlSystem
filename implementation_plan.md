# Inventory & Stock Control System Implementation Plan

This plan outlines the steps to build the full-stack Inventory & Stock Control System within your `InventoryStockControlSystem` project using ASP.NET Core MVC (.NET 10), Entity Framework Core (PostgreSQL), and Bootstrap 5.

## User Review Required

> [!IMPORTANT]
> The database will be switched from the default SQLite to **SQL Server (2008 compatible)** as requested.
> The project will use `.NET 10` based on your workspace setup, but the code architecture will follow the Clean Code / Repository pattern you provided.

## Open Questions

> [!WARNING]
> 1. **SQL Server Connection:** Once you have created the database using the provided schema, please provide the connection string (or Server, Database Name, User, Password) so I can configure `appsettings.json`.
> 2. **Authentication Flow:** Should I implement full custom login/register Razor views, or use the default Identity UI scaffolding for now?

## Proposed Changes

---

### Phase 1: Package Management & Configuration
*   **Remove** `Microsoft.EntityFrameworkCore.Sqlite`
*   **Add** Packages:
    *   `Microsoft.EntityFrameworkCore.SqlServer`
    *   `FluentValidation.AspNetCore`
    *   `Serilog.AspNetCore`
    *   `Serilog.Sinks.Console`
    *   `Serilog.Sinks.File`
    *   `AutoMapper.Extensions.Microsoft.DependencyInjection`
    *   `ClosedXML` (for Excel Export)
*   **Update** `Program.cs` and `appsettings.json` to register Serilog, SQL Server, AutoMapper, FluentValidation, and Dependency Injection for services/repositories.

---

### Phase 2: Core Models & Data Access Layer
Create the Entity models and `DbContext`.
#### [NEW] `Models/ApplicationUser.cs`
#### [NEW] `Models/Product.cs`
#### [NEW] `Models/Category.cs`
#### [NEW] `Models/Warehouse.cs`
#### [NEW] `Models/StockMovement.cs`
#### [NEW] `Models/AuditLog.cs`
#### [MODIFY] `Data/ApplicationDbContext.cs`
*   Configure Identity to use `ApplicationUser`.
*   Add `DbSet`s for all models.
*   Configure Fluent API (Indexes, Relationships).
*   Override `SaveChangesAsync` to automatically track changes and insert `AuditLog` records.

---

### Phase 3: Repositories & Services Layer (Clean Architecture)
Create generic repository and specific business services.
#### [NEW] `Repositories/IRepository.cs` & `Repositories/Repository.cs`
#### [NEW] `Services/IProductService.cs` & `Services/ProductService.cs`
#### [NEW] `Services/IStockMovementService.cs` & `Services/StockMovementService.cs`
*(Other services for Categories, Warehouses, etc.)*

---

### Phase 4: ViewModels, Mapping, & Validation
#### [NEW] `ViewModels/ProductViewModel.cs`
#### [NEW] `ViewModels/DashboardViewModel.cs`
#### [NEW] `Mappings/MappingProfile.cs` (AutoMapper)
#### [NEW] `Validators/ProductValidator.cs` (FluentValidation)

---

### Phase 5: Controllers & UI (Views)
Implement the MVC Controllers and Bootstrap 5 Views.
#### [NEW] `Controllers/DashboardController.cs`
#### [NEW] `Controllers/ProductsController.cs`
*(and Categories, Warehouses, StockMovements controllers)*
#### [MODIFY] `Views/Shared/_Layout.cshtml`
*   Integrate modern Bootstrap 5 Dashboard layout.
*   Add Sidebar menu (`_Sidebar.cshtml`).
#### [NEW] `Views/Dashboard/Index.cshtml` (Chart.js implementation)
#### [NEW] `Views/Products/Index.cshtml` (DataTables integration)
#### [NEW] `Views/Products/Create.cshtml`, `Edit.cshtml`, etc.

---

### Phase 6: Migration & Seeding
*   Create initial EF Core Migration (`InitialCreate`).
*   Create a Database Seeder to initialize `Roles` (Admin, Staff, Manager) and an initial Admin user.

## Verification Plan

### Automated/Code Verification
*   Compile project to ensure no build errors.
*   Verify DI container starts successfully.

### Manual Verification
*   User runs the application (`dotnet run`).
*   Verify SQL Server database is accessible.
*   Log in with the seeded Admin account.
*   Test full CRUD cycle on Products and Categories.
*   Perform a Stock Movement (In/Out) and check if Dashboard graphs and Audit Logs update correctly.
