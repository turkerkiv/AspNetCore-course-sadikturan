using StoreApp.Data.Concrete;

namespace StoreApp.Data.Abstract;

public interface ICategoryRepository
{
    IQueryable<Category> Categories { get; }
}