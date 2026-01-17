using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.EntityFrameworkCore;

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

        // Register AppDbContext with SQL Server using connection string from configuration
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

         // Endpoint WITH dependency injection: IEmailSender is injected by ASP.NET Core's DI container.
        // The implementation (real or mock) is chosen based on the environment.
        // Modified register endpoint to add user to system
        app.MapPost("/register", async (string name, string email, IEmailSender emailSender, AppDbContext db) =>
        {
            var user = new User { Name = name, Email = email };
            db.Users.Add(user);
            await db.SaveChangesAsync();
            emailSender.SendMail(name);
            return Results.Created($"/users/{user.Id}", user);
        });
        
            // Endpoint to list all users
            app.MapGet("/users", async (AppDbContext db) =>
            {
                var users = await db.Users.ToListAsync();
                return Results.Ok(users);
            });

        // Endpoint to create a new study group
        app.MapPost("/studygroups", async (string topic, AppDbContext db) =>
        {
            var studyGroup = new StudyGroup { Topic = topic };
            db.StudyGroups.Add(studyGroup);
            await db.SaveChangesAsync();
            return Results.Created($"/studygroups/{studyGroup.Id}", studyGroup);
        });

        // Endpoint for a user to join a study group
        app.MapPost("/studygroups/{studyGroupId}/join/{userId}", async (int studyGroupId, int userId, AppDbContext db) =>
        {
            var studyGroup = await db.StudyGroups.Include(sg => sg.Users).FirstOrDefaultAsync(sg => sg.Id == studyGroupId);
            if (studyGroup == null)
                return Results.NotFound($"StudyGroup {studyGroupId} not found");

            var user = await db.Users.FindAsync(userId);
            if (user == null)
                return Results.NotFound($"User {userId} not found");

            if (!studyGroup.Users.Any(u => u.Id == userId))
            {
                studyGroup.Users.Add(user);
                await db.SaveChangesAsync();
            }
            return Results.Ok(studyGroup);
        });

        // Root endpoint returns a greeting message
        app.MapGet("/", () => "Welcome to ASP.Net Core");

        // Log the current ASP.NET Core environment to the console for debugging purposes
        Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {builder.Environment.EnvironmentName}");
        
        app.Run();
    }
}
