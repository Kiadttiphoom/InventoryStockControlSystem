# Inventory Stock Control System - Setup Guide

## Prerequisites
- .NET 10 SDK
- SQL Server 2008 or later
- Visual Studio Code or Visual Studio 2022

## Database Setup

### Step 1: Configure Connection String
Update `appsettings.json` with your SQL Server connection details:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=InventoryStockControlDb;Trusted_Connection=true;Encrypt=false;"
}
```

Replace `YOUR_SERVER_NAME` with:
- `.` for local default instance
- `localhost\SQLEXPRESS` for SQL Server Express
- Your actual server name for remote servers

### Step 2: Create EF Core Migration
Open Terminal and run:

```bash
dotnet ef migrations add InitialCreate
```

### Step 3: Update Database
Apply migrations to create the database schema:

```bash
dotnet ef database update
```

The database will be automatically seeded with:
- **Roles**: Admin, Manager, Staff
- **Admin User**: 
  - Email: `admin@inventory.com`
  - Password: `Admin@123456`
- **Sample Categories**: Electronics, Clothing, Books, Home & Garden, Sports & Outdoors
- **Sample Warehouses**: Main, Secondary, Distribution Center

## Running the Application

```bash
dotnet run
```

The application will start on:
- HTTPS: https://localhost:7001
- HTTP: http://localhost:5000

## Default Admin Credentials
- **Email**: admin@inventory.com
- **Password**: Admin@123456

> ⚠️ **Important**: Change the admin password after first login!

## Project Structure

```
├── Models/                    # Entity models
├── Data/                      # EF Core DbContext
├── Repositories/              # Generic repository pattern
├── Services/                  # Business logic layer
├── Controllers/               # MVC controllers
├── Views/                     # Razor views
├── ViewModels/                # View data transfer objects
├── Mappings/                  # AutoMapper profiles
├── Validators/                # FluentValidation rules
└── Properties/                # Project configuration
```

## Features Implemented

### Phase 1: Package Management
- ✅ Replaced SQLite with SQL Server
- ✅ Added Serilog for logging
- ✅ Configured AutoMapper
- ✅ Set up FluentValidation

### Phase 2: Core Models & Data Access
- ✅ ApplicationUser with extended properties
- ✅ Product, Category, Warehouse models
- ✅ StockMovement for inventory tracking
- ✅ AuditLog for change tracking
- ✅ ApplicationDbContext with relationships and indexes

### Phase 3: Repository & Services Layer
- ✅ Generic IRepository pattern
- ✅ ProductService with business logic
- ✅ StockMovementService for inventory management
- ✅ CategoryService and WarehouseService
- ✅ Dependency injection configured

### Phase 4: ViewModels, Mapping & Validation
- ✅ ViewModels for all entities
- ✅ AutoMapper configuration
- ✅ FluentValidation for input validation

### Phase 5: Controllers & Views
- ✅ DashboardController with analytics
- ✅ ProductsController with CRUD operations
- ✅ CategoriesController
- ✅ WarehousesController
- ✅ StockMovementsController
- ✅ Bootstrap 5 responsive views
- ✅ Sidebar navigation menu

### Phase 6: Migration & Seeding
- ✅ Initial database migration
- ✅ Role seeding (Admin, Manager, Staff)
- ✅ Admin user seeding
- ✅ Sample data initialization

## Main Routes

- `/Dashboard` - Dashboard with statistics
- `/Products` - Product management
- `/Categories` - Category management
- `/Warehouses` - Warehouse management
- `/StockMovements` - Stock movement recording and tracking

## Logging
Application logs are stored in the `logs/` directory with daily rotation.

## API Endpoints for Stock Movements

### Record Stock In
```
POST /StockMovements/Create
- movementType: "In"
- productId: [int]
- warehouseId: [int]
- quantity: [int]
- reference: [string]
- notes: [string]
```

### Record Stock Out
```
POST /StockMovements/Create
- movementType: "Out"
- productId: [int]
- warehouseId: [int]
- quantity: [int] (must be ≤ current stock)
- reference: [string]
- notes: [string]
```

## Troubleshooting

### Connection String Issues
- Verify SQL Server is running
- Test connection with SQL Server Management Studio
- Check Windows Authentication is enabled

### Migration Issues
```bash
# View pending migrations
dotnet ef migrations list

# Revert last migration
dotnet ef migrations remove

# Update to specific migration
dotnet ef database update [migration-name]
```

### Authentication Issues
- Clear browser cookies
- Check SQL Server for IdentityUser table
- Verify admin user exists in AspNetUsers table

## Future Enhancements
- Chart.js integration for dashboard analytics
- Export to Excel using ClosedXML
- Advanced reporting and filtering
- Multi-warehouse stock transfer
- Barcode scanning integration
- Email notifications for low stock
