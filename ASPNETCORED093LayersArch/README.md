# E-Commerce System

A modern **3-layer architecture** e-commerce application built with **ASP.NET Core 9** and **Entity Framework Core**. This project demonstrates best practices for building scalable, maintainable web applications with clean separation of concerns.

## 🏗️ Project Architecture

The solution is organized into three main layers:

### 1. **EcommerceSystem.PL** (Presentation Layer)
- ASP.NET Core MVC with Razor Views
- Controllers for handling HTTP requests
- User interface and routing
- Dependency injection configuration

### 2. **Ecommerce.BLL** (Business Logic Layer)
- Managers/Services implementing core business logic
- `ProductManager` - Handles product operations
- `CategoryManager` - Handles category operations
- Business rules and validation
- Data transfer objects (ViewModels)

### 3. **Ecommerce.DAL** (Data Access Layer)
- Entity Framework Core DbContext
- Repository pattern for data access
- Unit of Work pattern for transaction management
- Database models and migrations
- Direct database operations

## 📋 Features

- **Product Management**
  - View all products with categories
  - Get product details by ID
  - Create new products
  - Update existing products
  - Delete products
  - Filter products by category

- **Category Management**
  - View all categories
  - Organize products by categories

- **Database Integration**
  - SQL Server support
  - Entity Framework Core ORM
  - Repository pattern for clean data access

## 🛠️ Technologies & Dependencies

- **.NET Framework**: .NET 9
- **Web Framework**: ASP.NET Core MVC
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Language**: C#
- **Architecture Pattern**: Repository + Unit of Work + Dependency Injection

## 🚀 Getting Started

### Prerequisites
- .NET 9 SDK or later
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or Visual Studio Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd ASPNETCORED093LayersArch
   ```

2. **Configure Database Connection**

   Update the connection string in `EcommerceSystem.PL/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=EcommerceDB;Trusted_Connection=true;TrustServerCertificate=true;"
     }
   }
   ```

3. **Restore NuGet Packages**
   ```bash
   dotnet restore
   ```

4. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run --project EcommerceSystem.PL
   ```

   The application will be available at `https://localhost:xxxx`

## 📁 Project Structure

```
ASPNETCORED093LayersArch/
├── EcommerceSystem.PL/                 # Presentation Layer (MVC)
│   ├── Controllers/
│   ├── Views/
│   ├── Models/
│   ├── Program.cs
│   ├── appsettings.json
│   └── appsettings.Development.json
│
├── Ecommerce.BLL/                      # Business Logic Layer
│   ├── Managers/
│   │   ├── ProductManager/
│   │   │   ├── ProductManager.cs
│   │   │   └── IProductManager.cs
│   │   └── CategoryManager/
│   │       ├── CategoryManager.cs
│   │       └── ICategoryManager.cs
│   └── ViewModels/
│
├── Ecommerce.DAL/                      # Data Access Layer
│   ├── Repositories/
│   │   ├── ProductRepository.cs
│   │   └── CategoryRepository.cs
│   ├── Models/
│   ├── AppDbContext.cs
│   └── UnitOfWork.cs
│
└── README.md                            # Project documentation
```

## 🔄 Dependency Injection Setup

The application uses ASP.NET Core's built-in dependency injection container. All services are registered in `Program.cs`:

```csharp
// Data Access Layer
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Business Logic
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<ICategoryManager, CategoryManager>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

## 💡 Core Classes

### ProductManager
Manages all product-related operations:
- `GetAll()` - Retrieve all products with categories
- `GetById(id)` - Get specific product details
- `Create(productCreateVM, imageName)` - Create new product
- `Update(productEditVM, imageName)` - Update product
- `Delete(id)` - Delete product
- `GetByCategory(categoryId)` - Filter by category

### CategoryManager
Manages category operations:
- `GetAllCategories()` - Retrieve all categories

## 📊 Database Schema

The application uses Entity Framework Core with the following main entities:
- **Product** - Product details (Name, Description, Price, Count, ExpiryDate, ImageURL)
- **Category** - Product categories

## 🔐 Security Considerations

- SQL Server connection strings are configured in environment-specific `appsettings` files
- Sensitive data should not be committed to version control
- Use User Secrets for development environment sensitive data

## 📝 Development Notes

- This project follows the **Repository Pattern** for data access
- The **Unit of Work Pattern** manages transactions across multiple repositories
- **Dependency Injection** is used throughout for loose coupling
- **ViewModels** are used for data transfer between layers

## 🧪 Testing

To test the application:
1. Ensure your database is properly configured
2. Run the application from Visual Studio or command line
3. Navigate to product and category management pages

## 🤝 Contributing

This is a training/learning project. For contributions or suggestions, please feel free to open an issue or pull request.

## 📄 License

This project is part of an ITI training program. Please check with your institution for usage guidelines.

## 👤 Author

Created as part of ASP.NET Core training (Day 9) at ITI.

## 📞 Support

For issues, questions, or improvements, please refer to the course materials or contact your instructor.

---

**Happy Coding! 🚀**
