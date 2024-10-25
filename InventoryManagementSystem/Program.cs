using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>(); // _InventoryRepository object -> Dependency Injection
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();  // _ProductsRepository object -> Dependency Injection
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>(); // _AssignmentRepository object -> Dependency Injection

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
