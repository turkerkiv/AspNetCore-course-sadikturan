using Microsoft.EntityFrameworkCore;

namespace StoreApp.Data.Concrete;

public class StoreDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
        .HasMany(e => e.Categories)
        .WithMany(e => e.Products)
        .UsingEntity<ProductCategory>();

        modelBuilder.Entity<Category>()
            .HasIndex(u => u.Url)
            .IsUnique();

        modelBuilder.Entity<Product>().HasData(
            new List<Product>()
            {
                new(){ Id = 1, Name = "samsung s24", Price = 15000, Description = "a good phone", },
                new(){ Id = 2, Name = "samsung s25", Price = 16000, Description = "a good phone", },
                new(){ Id = 3, Name = "samsung s26", Price = 17000, Description = "a good phone", },
                new(){ Id = 4, Name = "samsung s27", Price = 18000, Description = "a good phone", },
                new(){ Id = 5, Name = "samsung s28", Price = 19000, Description = "a good phone", },
                new(){ Id = 6, Name = "samsung s29", Price = 20000, Description = "a good phone", },
                new(){ Id = 7, Name = "samsung s30", Price = 21000, Description = "a good phone", },
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new List<Category>()
            {
                new() { Id = 1, Name = "Phone", Url = "phone"},
                new() { Id = 2, Name = "Electronic", Url = "electronic"},
                new() { Id = 3, Name = "Laptop", Url = "laptop"},
            }
        );

        modelBuilder.Entity<ProductCategory>().HasData(
            new List<ProductCategory>()
            {
                new() {
                    ProductId = 1, CategoryId = 1
                },
                new() {ProductId = 2, CategoryId = 2},
                new() {ProductId = 3, CategoryId = 1},
                new() {ProductId = 4, CategoryId = 3},
            }
        );
    }
}