using Core.Interfaces;
using ECommerceAPI.Middleware;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using StackExchange.Redis;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("Infrastructure")  // ✅ Set correct migrations assembly),
    );
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenricRepository<>), typeof(GenericRepository<>));
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var connString = builder.Configuration.GetConnectionString("Redis");
    if(connString == null) throw new Exception("Redis connection string is missing");
    var configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x=>x.AllowAnyHeader()
.AllowAnyMethod().WithOrigins("https://localhost:4200", "https://localhost:4200"));
app.MapControllers();
app.MapFallbackToController("Index","Fallback");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();

    try
    {
        await context.Database.MigrateAsync(); // ✅ Alternative fix
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database Migration Error: {ex.Message}");
    }
    try
    {
        await StoreContextSeed.SeedAsync(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Problem While seeding data",ex.Message.ToString());
    }

}
await app.RunAsync();
