
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TournamentService.Application.Services;
using TournamentService.Application.Services.Abstractions;
using TournamentService.Application.Services.Mapping;
using TournamentService.Domain.Repositories.Abstractions.Repositories;
using TournamentService.Infrastructure.EntityFramework;
using TournamentService.Infrastructure.EntityFramework.RepositoriesEF;
using TournamentService.WebHost.Helpers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string for TournamentServiceDbContext is not configured.");
}

builder.Services.AddNpgsql<ApplicationDbContext>(connectionString, options =>
{
    options.MigrationsAssembly("TournamentService.Infrastructure.EntityFramework");

});

builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "TournamentService service API",
            Description = "API for creating, viewing, storing, modifying, and deleting tournament data."
        });
    });

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    {
        options.UseNpgsql(connectionString);
    });

builder.Services.AddAutoMapper(configuration => configuration.AddProfile<ApplicationProfile>());
builder.Services.AddScoped<IOrganizerRepository, OrganizerRepository>();
builder.Services.AddScoped<IOrganizerApplicationService, OrganizerApplicationService>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy
            .SetIsOriginAllowed(origin =>
            {
                if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                    return false;

                return uri.Host is "localhost" or "127.0.0.1";
            })
            .AllowAnyHeader()
            .AllowAnyMethod());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("Frontend");
app.UseAuthorization();


app.MapControllers();

app.MigrateDatabase<ApplicationDbContext>();

app.Run();
