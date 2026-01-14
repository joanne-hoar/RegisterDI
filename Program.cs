

using Swashbuckle.AspNetCore.SwaggerGen;

namespace RegisterDI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        // Register EmailSender for dependency injection. 
        // AddTransient creates a new instance each time it's requested.
        builder.Services.AddTransient<EmailSender>();

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
            return emailSender.SendMail(username);
        });

        // Endpoint WITH dependency injection: EmailSender is injected by ASP.NET Core's DI container.
        // The framework automatically provides an EmailSender instance when this endpoint is called.
        app.MapGet("/register/DI/{username}", (string username, EmailSender emailSender) =>
        {
            return emailSender.SendMail(username);
        });

        // Root endpoint returns a greeting message
        app.MapGet("/", () => "Welcome to ASP.Net Core");

        app.Run();
    }
}
