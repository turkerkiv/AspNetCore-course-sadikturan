using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Components;

public class CategoriesListViewComponent : ViewComponent
{
    private readonly IStoreRepository _storeRepo;

    public CategoriesListViewComponent(IStoreRepository storeRepository)
    {
        _storeRepo = storeRepository;
    }

    public IViewComponentResult Invoke()
    {
        ViewBag.SelectedCategory = RouteData.Values["categoryUrl"];
        return View(_storeRepo
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