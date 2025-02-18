using BlazorCrudApp.Server.Database;
using BlazorCrudApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductDb"));

// Add Controllers
builder.Services.AddControllers();

// (Optional) Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// If dev environment, enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map the controllers
app.MapControllers();

// Optional: seed in-memory DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    // Add some initial data if you want
    if (!db.Products.Any())
    {
        db.Products.Add(new Product
        {
            Name = "Seeded Product",
            Description = "This is a seeded product",
            Price = 9.99m
        });
        db.SaveChanges();
    }
}

app.Run();
