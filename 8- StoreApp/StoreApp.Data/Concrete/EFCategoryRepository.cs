using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;

namespace StoreApp.Data.Concrete;

public class EFCategoryRepository : ICategoryRepository
{
    StoreDbContext _dbContext;
    public IQueryable<Category> Categories => _dbContext.Categories;

    public EFCategoryRepository(StoreDbContext storeDbContext)
    {
        _dbContext = storeDbContext;
    }

}