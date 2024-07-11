using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Controllers;

[Authorize]
public class UsersController : Controller
{
    readonly UserManager<AppUser> _userManager;
    readonly RoleManager<AppRole> _roleManager;

    public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        return View(_userManager.Users);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == null) return NotFound();

        var user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {
            ViewBag.Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            return View(new EditViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                SelectedRoles = await _userManager.GetRolesAsync(user),
            });
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, EditViewModel model)
    {
        if (id != model.Id) return NotFound();

        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        user.Email = model.Email;
        user.FullName = model.FullName!;


        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
        {
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, model.Password);
        }

        if (result.Succeeded)
        {
            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            if (model.SelectedRoles != null)
                await _userManager.AddToRolesAsync(user, model.SelectedRoles);
            return RedirectToAction("Index");
        }
        else
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }

            return View(model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null) return NotFound();

        var user = await _userManager.FindByIdAsync(id);

        if (user == null) return NotFound();
        await _userManager.DeleteAsync(user);
        return RedirectToAction("Index");
    }
}