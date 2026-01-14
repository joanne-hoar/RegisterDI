# RegisterDI

This project demonstrates the basics and power of Dependency Injection (DI) in ASP.NET Core.

**Author:** Joanne

**Acknowledgements:**
This project was developed, documented, and troubleshooted with the assistance of GitHub Copilot (GPT-4.1 AI).

## Purpose

The goal of this project is to show how ASP.NET Core's built-in DI container can be used to manage dependencies in a web application. It provides two endpoints to illustrate the difference between manual instantiation and dependency injection:

- `/register/{username}`: Creates an `EmailSender` manually inside the endpoint (no DI).
- `/register/DI/{username}`: Uses ASP.NET Core's DI to inject an `EmailSender` automatically.

### Why Dependency Injection?
- Promotes loose coupling
- Makes code easier to test and maintain
- Enables swapping implementations (e.g., for testing or different environments)

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
4. To force a specific profile (e.g., https, Production):
   ```
   dotnet run --launch-profile Production
   ```

This will use the URLs defined in `launchSettings.json`.

## Endpoints
- `GET /register/{username}`
  - Demonstrates manual instantiation (no DI)
- `GET /register/DI/{username}`
  - Demonstrates dependency injection

Open your browser to the Swagger UI to test the endpoints.

> **Swagger UI Access:**
>
> After running the project, you can access the Swagger UI at:
>
> - [https://localhost:7165/swagger](https://localhost:7165/swagger)
> - [http://localhost:5224/swagger](http://localhost:5224/swagger)
>
> The exact URL may be shown in the console output when you start the application. Use the Swagger UI to easily explore and test all available endpoints.

## How Dependency Injection is Demonstrated

This project now uses an `IEmailSender` interface with two implementations:
- `EmailSender`: The real implementation, used in Production.
- `MockEmailSender`: A mock implementation, used in Development.

The registration in `Program.cs` automatically selects which implementation to inject based on the environment:
- In Development, requests to `/register/DI/{username}` will return a mock message.
- In Production, the same endpoint will return a real message.

### To test the behavior:
- Run the project as usual (`dotnet run`).
- Use the Swagger UI or your browser to call:
  - `/register/{username}`: Always uses manual instantiation (no DI).
  - `/register/DI/{username}`: Uses DI and will show different results depending on the environment.

You can change the environment by setting the `ASPNETCORE_ENVIRONMENT` variable before running.

To test the application's behavior using different profiles or environment variables, you can use the `launchSettings.json` file located in the `Properties` folder. This file allows you to configure how the application is launched, including environment variables, command-line arguments, and profiles for development and production.

Observe the difference in the response from `/register/DI/{username}` to see DI in action!

---

Feel free to use and modify this project to further explore DI in ASP.NET Core!