using AvServiceHR.infrastructure.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura il DbContext
builder.Services.AddDbContext<AdventureWorks2017Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksConnection")));

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
