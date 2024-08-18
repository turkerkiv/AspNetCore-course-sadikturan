using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;

public class HomeController : Controller
{
    public int _pageSize = 1;
    readonly IProductRepository _storeRepo;
    readonly IMapper _mapper;

    public HomeController(IProductRepository storeRepository, IMapper mapper)
    {
        _storeRepo = storeRepository;
        _mapper = mapper;
    }

    public IActionResult Index(string categoryUrl, int page = 1)
    {
        var finalProducts = _storeRepo
                                    .GetProductsByCategory(categoryUrl, page, _pageSize)
                                    .Select(p => _mapper.Map<ProductViewModel>(p)
                                         );

        return View(new ProductListViewModel
        {
            Products = finalProducts,
            PageInfo = new PageInfo
            {
                ItemsPerPage = _pageSize,
                CurrentPage = page,
                TotalItems = _storeRepo.GetProductCount(categoryUrl),
            }
        });
    }
}