using Microsoft.EntityFrameworkCore;
using MultiTenancy.Extensions;
using MultiTenancy.Middleware;
using MultiTenancy.Models;
using MultiTenancy.Services;
using MultiTenancy.Services.ProductService;
using MultiTenancy.Services.TenantService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<TenantDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAndMigrateTenantDatabases(builder.Configuration);


builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ITenantService, TenantService>();


//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<TenantResolver>();

app.MapControllers();

app.Run();
