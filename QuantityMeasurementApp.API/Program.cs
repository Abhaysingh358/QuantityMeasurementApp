using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Business.Services;
using QuantityMeasurementApp.Repositories.Implementations;
using QuantityMeasurementApp.Repositories.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementEfRepository>();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Global Exception Middleware
app.UseMiddleware<QuantityMeasurementApp.API.Middlewares.GlobalExceptionHandler>();

// Request Logging
app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();