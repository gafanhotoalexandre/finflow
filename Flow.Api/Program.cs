using Flow.Api.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FinaDbContext>(options =>
    options.UseSqlServer(connectionString)
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
