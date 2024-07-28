using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:CustomConnection"], b => b.MigrationsAssembly("StoreApp.Web"));
});

builder.Services.AddScoped<IStoreRepository, EfStoreRepository>();

var app = builder.Build();

app.UseStaticFiles();

// products/samsung-s24 => phone details
app.MapControllerRoute("product_details", "products/{productUrl}", new { controller = "Home", action = "Details"});

// categories/phone => category
app.MapControllerRoute("category", "Categories/{categoryUrl?}", new { controller = "Home", action = "Index"});

app.MapDefaultControllerRoute();


app.Run();
