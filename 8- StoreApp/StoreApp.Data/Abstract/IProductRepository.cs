using StoreApp.Data.Concrete;

namespace StoreApp.Data.Abstract;

public interface IProductRepository
{
    IQueryable<Product> Products { get; }

    void CreateProduct(Product entity);
    int GetProductCount(string category);
    IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize);
}