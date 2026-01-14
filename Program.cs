using Swashbuckle.AspNetCore.SwaggerGen;

namespace RegisterDI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        // Register IEmailSender for dependency injection.
        // Use MockEmailSender in Development, EmailSender in Production/other.
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddTransient<IEmailSender, MockEmailSender>();
        }
        else
        {
            builder.Services.AddTransient<IEmailSender, EmailSender>();
        }

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        // Endpoint WITHOUT dependency injection: EmailSender is created manually inside the endpoint.
        // This does NOT use ASP.NET Core's DI container, so you are responsible for managing the instance.
        app.MapGet("/register/{username}", (string username) =>
        {
            var emailSender = new EmailSender();
            emailSender.SendMail(username);
            return $"Welcome, {username}! An email has been sent WITHOUT dependency injection.";
        });

         // Endpoint WITH dependency injection: IEmailSender is injected by ASP.NET Core's DI container.
        // The implementation (real or mock) is chosen based on the environment.
        app.MapGet("/register/DI/{username}", (string username, IEmailSender emailSender) =>
        {
            return emailSender.SendMail(username);
        });

        // Root endpoint returns a greeting message
        app.MapGet("/", () => "Welcome to ASP.Net Core");

        // Log the current ASP.NET Core environment to the console for debugging purposes
        Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {builder.Environment.EnvironmentName}");
        
        app.Run();
    }
}
