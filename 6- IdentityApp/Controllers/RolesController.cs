using IdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers;

[Authorize]
public class RolesController : Controller
{
    readonly RoleManager<AppRole> _roleManager;
    public RolesController(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        return View(_roleManager.Roles.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AppRole model)
    {
        if (ModelState.IsValid)
        {
            var result = await _roleManager.CreateAsync(model);

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
            {
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
        }
        return View(model);
    }
}