using System.Linq.Expressions;
using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class UsersController : Controller
{
    readonly IUserRepository _userRepo;
    public UsersController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public IActionResult Login()
    {
        if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Posts");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Posts");
        if (ModelState.IsValid)
        {
            var user = _userRepo.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                var userClaims = new List<Claim>();

                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                userClaims.Add(new Claim(ClaimTypes.Name, user.Username ?? ""));
                userClaims.Add(new Claim(ClaimTypes.GivenName, user.Name ?? ""));
                userClaims.Add(new Claim(ClaimTypes.UserData, user.Image ?? ""));

                if (user.Email == "tkvlcm@gmail.com")
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                }

                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProporties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                     new ClaimsPrincipal(claimsIdentity),
                     authProporties
                     );

                return RedirectToAction("Index", "Posts");
            }
            else
            {
                ModelState.AddModelError("", "Password or username is incorrect");
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        if (!User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Posts");
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Posts");
    }

    public IActionResult Register()
    {
        if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Posts");
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel registerViewModel)
    {
        if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Posts");
        if (!ModelState.IsValid)
        {
            return View(registerViewModel);
        }

        if (_userRepo.Users.Any(u => u.Username == registerViewModel.Username || u.Email == registerViewModel.Email))
        {
            ModelState.AddModelError("","Username or email is taken");
            return View(registerViewModel);
        }
        _userRepo.CreateUser(new Entity.User
        {
            Name = registerViewModel.Name,
            Username = registerViewModel.Username,
            Password = registerViewModel.Password,
            Email = registerViewModel.Email,
            Image = "1.jpg",
        });
        return RedirectToAction("Login");
    }

}