using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;

namespace StoreApp.Data.Concrete;

public class EfStoreRepository : IStoreRepository
{
    StoreDbContext _dbContext;
    public IQueryable<Product> Products => _dbContext.Products;
    public IQueryable<Category> Categories => _dbContext.Categories;

    public EfStoreRepository(StoreDbContext storeDbContext)
    {
        _dbContext = storeDbContext;
    }

    public async void CreateProduct(Product entity)
    {
        _dbContext.Products.Add(entity);
        await _dbContext.SaveChangesAsync();
    }

    public int GetProductCount(string categoryUrl)
    {
        if (string.IsNullOrEmpty(categoryUrl)) return Products.Count();
        else return Products.Where(p => p.Categories.Any(c => c.Url == categoryUrl)).Count();
    }

    public IEnumerable<Product> GetProductsByCategory(string categoryUrl, int page, int pageSize)
    {
        var productsInCat = Products;

        if (!string.IsNullOrEmpty(categoryUrl))
        {
            productsInCat = Products
                                .Include(p => p.Categories)
                                .Where(p => p
                                            .Categories
                                            .Any(c => c.Url == categoryUrl));
        }

        productsInCat = productsInCat
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize);
        return productsInCat;
    }
}