using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Components;

public class CategoriesListViewComponent : ViewComponent
{
    private readonly IProductRepository _proRepo;
    private readonly ICategoryRepository _catRepo;

    public CategoriesListViewComponent(IProductRepository proRepo, ICategoryRepository catRepo)
    {
        _proRepo = proRepo;
        _catRepo = catRepo;
    }

    public IViewComponentResult Invoke()
    {
        ViewBag.SelectedCategory = RouteData.Values["categoryUrl"];
        return View(_catRepo
                    .Categories
                    .Select(c => new CategoryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Url = c.Url,
                    })
                    .ToList()
                    );
    }
}