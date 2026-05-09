# Premium Inventory & Stock Control System

A complete, full-stack, enterprise-grade Inventory and Stock Control System built with **ASP.NET Core MVC** and **Entity Framework Core**. The application features a modern, responsive, and premium UI design out-of-the-box.

![Dashboard Preview](https://via.placeholder.com/1200x600.png?text=Premium+Inventory+System+Dashboard)

## 🚀 Key Features

- **Premium UI/UX:** Custom dark-themed sidebar layout, gradient KPI cards, micro-animations, and modern typography (Google Inter font).
- **Dashboard & Analytics:** Real-time summary statistics and data visualization using **Chart.js** (Doughnut charts for stock movement breakdown).
- **Product Management:** Complete CRUD for Products with categorized grouping, Reorder Levels, and Unit Price tracking.
- **Warehouse Management:** Manage multiple stock locations and warehouses.
- **Stock Movements (In/Out/Adjust):** Accurately record stock receiving, dispatching, and manual adjustments with reference numbers and typed badges.
- **Audit Logging:** Automated tracking of all system modifications (Create, Update, Delete) at the entity level.
- **Authentication & Authorization:** Secure login system powered by ASP.NET Core Identity.
- **Interactive Data Tables:** Advanced table sorting, searching, and pagination powered by **DataTables.net**.

## 🛠️ Technology Stack

- **Backend:** C# / .NET 10 (or .NET 8)
- **Framework:** ASP.NET Core MVC
- **Database ORM:** Entity Framework Core (SQL Server)
- **Identity:** ASP.NET Core Identity
- **Frontend:** HTML5, CSS3 (Custom Variables), Bootstrap 5
- **Libraries:** Chart.js (Charts), DataTables (Tables), Bootstrap Icons
- **Mapping & Validation:** AutoMapper, FluentValidation

## 📋 Prerequisites

To run this project locally, ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download) (Version 8.0 or newer)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer Edition)
- IDE (Visual Studio 2022, VS Code, or Zyther IDE)

## ⚙️ Getting Started

Follow these steps to get your development environment running:

### 1. Clone the repository
```bash
git clone <repository-url>
cd InventoryStockControlSystem
```

### 2. Configure Database
Update the `DefaultConnection` string in `appsettings.Development.json` (or `appsettings.json`) to point to your SQL Server instance:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=inventory;User Id=sa;Password=1234;TrustServerCertificate=True"
}
```

### 3. Apply Migrations & Seed Database
The project includes a `DatabaseSeeder` which automatically runs migrations and seeds default data on startup. 

*Optional: If you want to manually update the database before running:*
```bash
dotnet ef database update
```

### 4. Run the Application
Start the application using the .NET CLI:
```bash
dotnet run
```
The application will be available at `http://localhost:5236` (or the port specified in your console output).

## 🔐 Default Credentials

Upon the first run, the system automatically seeds an Admin account. Use these credentials to log in:

- **Email:** `admin@inventory.com`
- **Password:** `Admin@123456`

## 📁 Project Structure

```
InventoryStockControlSystem/
├── Controllers/         # MVC Controllers (Products, Warehouses, etc.)
├── Models/              # Domain Entities (Product, Category, AuditLog, etc.)
├── ViewModels/          # DTOs and Form Models
├── Services/            # Business Logic & Database Seeder
├── Repositories/        # Generic Repository Pattern implementation
├── Data/                # ApplicationDbContext & Migrations
├── Mappings/            # AutoMapper Profiles
├── Validators/          # FluentValidation rules
├── Views/               # Razor Pages & UI Views
└── wwwroot/
    ├── css/site.css     # Premium UI Design System & Variables
    └── js/site.js       # Global scripts
```

## 📄 License

This project is licensed under the MIT License. You are free to modify and distribute it as needed.
