using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers;

public class UsersController : Controller
{
    readonly UserManager<IdentityUser> _userManager;

    public UsersController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View(_userManager.Users);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        if (_userManager.FindByNameAsync(model.UserName!) != null)
        {
            ModelState.AddModelError("", "Username is taken");
        }

        var identityUser = new IdentityUser
        {
            UserName = model.UserName,
            Email = model.Email,
        };

        IdentityResult result = await _userManager.CreateAsync(identityUser, model.Password!);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View(model);
        }
    }
}