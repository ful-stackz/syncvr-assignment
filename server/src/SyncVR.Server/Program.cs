using Microsoft.EntityFrameworkCore;
using SyncVR.Server;
using SyncVR.Server.Database;
using SyncVR.Server.Services;
using SyncVR.Server.Stores;

var builder = WebApplication.CreateBuilder(args);

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();
