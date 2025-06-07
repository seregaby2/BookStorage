BookStorage — Online Bookstore

**BookStorage** is a web application for managing an online bookstore with the ability to add (get, update, delete) authors, books, customers, and place orders.

---

## Features

-  CRUD operations for books and authors  
-  Customer management  
-  Order creation, update, and retrieval  
-  Data access via Dapper for high performance  
-  Object mapping using AutoMapper  
-  Clean architecture with clear layer separation and SOLID principles
-  Swagger documentation
-  Versioning my API
-  Rate limiting to protect your API from DOS & DDOS and attacks 

---

## Technologies

- [.NET 8]
- ASP.NET Core Web API  
- Dapper  
- AutoMapper  
- MediatR
- FluentValidation
- MS SQL LocalDB  
- Clean Architecture / N-Layer / SOLID Principles  

---

## Architecture Overview
BookStorage.sln
 - BookStorage.WebApi # `Web API (Controllers, DTOs, AutoMapper)`
 - BookStorage.Application # `Interfaces, business logic, services`
 - BookStorage.Infrastructure # `Dapper repositories and DB interaction`
 - BookStorage.Domain # `Entities and enums`

---

## How to Run

1. Open the solution in Visual Studio  
2. Set `BookStorage.WebApi` as the startup project  
3. Configure connection string in `appsettings.json`:
4. `"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BookStorage;Trusted_Connection=True;TrustServerCertificate=True;"
};`
5. Make sure the database exists and tables are created (example my scheme.sql `BookStorage.Infrastructure/Db/CreateSchemeFINAL.sql`);
6. Run the application via IIS Express (F5)
7. Open Swagger UI in browser at https://localhost:44370/swagger





