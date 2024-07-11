using System.Security.Claims;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers;

public class AccountController : Controller
{
    readonly UserManager<AppUser> _userManager;
    readonly RoleManager<AppRole> _roleManager;
    readonly SignInManager<AppUser> _signInManager;
    readonly IEmailSender _emailSender;

    public AccountController(IEmailSender emailSender, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
    }

    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user == null)
            {
                ModelState.AddModelError("", "Email couldnt found");
                return View(model);
            }
            else
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "Please confirm your email");
                    return View(model);
                }
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user, model.Password!, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                    await _userManager.SetLockoutEndDateAsync(user, null);
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                    var timeLeft = lockoutDate.Value - DateTime.UtcNow;

                    ModelState.AddModelError("", "please try again after " + timeLeft);
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Your password is wrong");
                    return View(model);
                }
            }
        }
        else
            return View(model);
    }

    public IActionResult Create()
    {
        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateViewModel model)
    {
        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
        if (!ModelState.IsValid) return View(model);
        var identityUser = new AppUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName!,
        };
        IdentityResult result = await _userManager.CreateAsync(identityUser, model.Password!);
        if (result.Succeeded)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
            var url = Url.Action("ConfirmEmail", "Account", new
            {
                identityUser.Id,
                token,
            });
            await _emailSender.SendEmailAsync(identityUser.Email!, "Account confirmation", $"Please click the link below to confirm your email<br><a href='http://localhost:5263{url}'>Link</a>");
            return RedirectToAction("Index", "Home");
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

    public async Task<IActionResult> ConfirmEmail(string id, string token)
    {
        if (id == null || token == null)
        {
            TempData["message"] = "Invalid token";
            return View();
        }

        var user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                TempData["message"] = "Your account approved";
                return RedirectToAction("Login");
            }
        }

        TempData["message"] = "User did not found";
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return View();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var url = Url.Action("ResetPassword", "Account", new { user.Id, token });

        await _emailSender.SendEmailAsync(email, "Reset Passworrd", $"Click link below to reset your password <br> {url}");

        TempData["message"] = "Link has sent to your email.";
        return View();
    }

    public IActionResult ResetPassword(string id, string token)
    {
        if (id == null || token == null) return NotFound();
        return View(new ResetPasswordViewModel
        {
            Id = id,
            Token = token,
        });
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid) return NotFound();

        var user = await _userManager.FindByIdAsync(model.Id!);

        if (user == null) return NotFound();

        await _userManager.ResetPasswordAsync(user, model.Token!, model.NewPassword!);

        TempData["message"] = "Password changed successfully";
        return RedirectToAction("Login");
    }
}