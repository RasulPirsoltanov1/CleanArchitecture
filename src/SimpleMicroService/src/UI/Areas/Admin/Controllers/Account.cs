using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.Areas.Admin.Models;
using UI.Models;

namespace UI.Areas.Admin.Controllers;


[Area("admin")]
[AllowAnonymous]
public class Account : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public Account(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
    }

    public async Task<IActionResult> Login(string? ReturnUrl)
    {
        await _signInManager.SignOutAsync();
        ViewBag.ReturnUrl = ReturnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUser model, string? ReturnUrl)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return Redirect(ReturnUrl ?? @"\admin\");
                }
            }

            ModelState.AddModelError("UserName", "Invalid username or password");

        }

        return View(model);
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("login", "account");
    }
}
