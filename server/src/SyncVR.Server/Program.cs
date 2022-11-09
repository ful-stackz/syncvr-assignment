using Microsoft.EntityFrameworkCore;
using SyncVR.Server;
using SyncVR.Server.Database;
using SyncVR.Server.Services;
using SyncVR.Server.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add console logging
builder.Logging.AddConsole();

// Register database service
builder.Services.AddDbContext<FibonacciDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetValue<string>(ConfigKey.ConnectionString))
);

builder.Services.AddScoped<QueriesStore>();
builder.Services.AddScoped<FibonacciStore>();
builder.Services.AddScoped<Fibonacci>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
