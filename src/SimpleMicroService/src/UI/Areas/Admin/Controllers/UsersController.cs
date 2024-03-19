using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI.Areas.Admin.Models;
using UI.Models;

namespace UI.Areas.Admin.Controllers;


[Area("admin")]
//[Authorize(Roles = "admin")]
public class UsersController : Controller
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly PasswordHasher<ApplicationUser> _passwordHasher;
    private readonly IPasswordValidator<ApplicationUser> _passwordValidator;

    public UsersController(UserManager<ApplicationUser> userManager, /*PasswordHasher<ApplicationUser> passwordHasher, */IPasswordValidator<ApplicationUser> passwordValidator)
    {
        this._userManager = userManager;
        //this._passwordHasher = passwordHasher;
        this._passwordValidator = passwordValidator;
    }

    public async Task<IActionResult> Index() => View(await _userManager.Users.ToListAsync());
    public IActionResult Create() => View();



    [HttpPost]
    public async Task<IActionResult> Create([Bind("UserName,Password,Email")] UserCreateDto model)
    {

        if (ModelState.IsValid)
        {
            var user = new ApplicationUser();
            user.UserName = model.UserName;
            user.Email = model.Email;

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
        }
        return View(model);
    }
}
