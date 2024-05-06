# Inventory Management Application

## Description
This is a simple Inventory Management application designed in ASP.NET Core 6.0. The application provides basic functionalities such as creating, reading, updating, and deleting (CRUD operations) products in the inventory. The application uses Entity Framework Core for data access, PostgreSQL as the database, and AutoMapper for object-object mapping.

## Prerequisites

To run this application, you will need:

- .NET 6.0 SDK
- PostgreSQL

## Setup and Running

To download the project and run it in your local environment, please follow the steps below:

1. **Clone the repository**

   ```
   git clone https://dmuka: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@github.com/dmuka/AI_Course_InventorySystem.git
   ```

2. **Change to the directory that was just created**

   ```bash
   cd AI_Course_InventorySystem
   ```
   
3. **Install the necessary NuGet packages**

   Run `dotnet restore` command

4. **Add PostgreSQL database connection string**

   Open `appsettings.json` and replace `"InventoryDbContext"` value with your PostgreSQL connection string. The connection string generally looks like:
    
   ```
   Host=localhost;Database=InventoryDb;Username=postgres;Password=password
   ```

5. **Update database**

   You will need to run Entity Framework Core migrations to create your database schema. 

   Run `dotnet ef database update` command

6. **Run the app**

   To start the application, run this command:

   ```bash
   dotnet run
   ```

The application should now be up and running. If you navigate to `https://localhost:5001/api/products`, you should see a JSON response with the list of products in your inventory.
  
## Features
- **Product Management**: The main feature of this application is to manage the products in the inventory. It supports adding new products, updating existing products, retrieving the list and details of products, and removing products from the inventory.

Enjoy using the Inventory Management Application!
