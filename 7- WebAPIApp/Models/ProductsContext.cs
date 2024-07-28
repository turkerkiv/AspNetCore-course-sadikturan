using _7__WebAPIApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApiApp.Models;

public class ProductsContext : IdentityDbContext<AppUser, AppRole, int>
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Iphone 14", Price = 60000, IsActive = true }
            );
        modelBuilder.Entity<Product>().HasData(
        new Product { Id = 2, Name = "Iphone 15", Price = 65000, IsActive = true }
        ); modelBuilder.Entity<Product>().HasData(
        new Product { Id = 3, Name = "Iphone 16", Price = 70000, IsActive = true }
        );
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 4, Name = "Iphone 17", Price = 75000, IsActive = false}
            );
    }

    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
    {
    }
}