# RegisterDI

This project demonstrates the basics and power of Dependency Injection (DI) in ASP.NET Core.

## Purpose

The goal of this project is to show how ASP.NET Core's built-in DI container can be used to manage dependencies in a web application. It provides two endpoints to illustrate the difference between manual instantiation and dependency injection:

- `/register/{username}`: Creates an `EmailSender` manually inside the endpoint (no DI).
- `/register/DI/{username}`: Uses ASP.NET Core's DI to inject an `EmailSender` automatically.

## Features
- Minimal API setup with .NET 10
- Swagger/OpenAPI support for easy testing
- Example of registering and injecting services
- Clear code comments explaining DI concepts

## How to Run
1. Restore dependencies:
   ```sh
   dotnet restore
   ```
2. Build the project:
   ```sh
   dotnet build
   ```
3. Run the project:
   ```sh
   dotnet run
   ```
4. Open your browser to the Swagger UI (usually at `https://localhost:5001/swagger` or as shown in the console) to test the endpoints.

## Endpoints
- `GET /register/{username}`
  - Demonstrates manual instantiation (no DI)
- `GET /register/DI/{username}`
  - Demonstrates dependency injection

## Why Dependency Injection?
- Promotes loose coupling
- Makes code easier to test and maintain
- Enables swapping implementations (e.g., for testing or different environments)

## Requirements
- .NET 10 SDK or later

---

Feel free to use and modify this project to further explore DI in ASP.NET Core!