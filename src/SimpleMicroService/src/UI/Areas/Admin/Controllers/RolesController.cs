using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UI.Areas.Admin.Models;
using UI.Models;

namespace UI.Areas.Admin.Controllers;


[Area("admin")]
[Authorize]
public class RolesController : Controller
{

    #region Constructor
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    public RolesController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        this._roleManager = roleManager;
        this._userManager = userManager;
    }
    #endregion

    public async Task<IActionResult> Index() => View(await _roleManager.Roles.ToListAsync());
    public IActionResult Create() => View();


    #region Create POST
    [HttpPost]
    public async Task<IActionResult> Create(RoleCreateDto model)
    {

        if (ModelState.IsValid)
        {
            var role = new ApplicationRole();
            role.Name = model.Name.ToLower().Replace(" ", "");
            role.ExpireDate = model.ExpireDate;

            var result = await _roleManager.CreateAsync(role);
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
    #endregion



    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        ApplicationRole? role = await _roleManager.FindByIdAsync(id);
        var members = new List<ApplicationUser>();  // bu role sahip kullanıcıların listesi
        var nonMembers = new List<ApplicationUser>(); // bu role sahip olmayan kullanıcıların listesi

        foreach (var user in _userManager.Users)
        {
            //bool hasRole = await _userManager.IsInRoleAsync(user, role.Name);
            //if (hasRole)
            //{
            //    members.Add(user);
            //}
            //else
            //{
            //    nonMembers.Add(user);
            //}

            var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
            list.Add(user);
        }

        var vm = new RoleDetailsVM
        {
            Role = role,
            Members = members,
            NonMembers = nonMembers
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RoleEditDto model)
    {
        ApplicationRole role;
        IdentityResult result = new();
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                bool isInRole = await _userManager.IsInRoleAsync(user, model.RoleName);
                if (isInRole)
                {
                    result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                }
                else
                {
                    result = await _userManager.AddToRoleAsync(user, model.RoleName);
                }
            }
        }

        return Ok(new
        {
            StatusCode = result.Succeeded ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Messages = result.Errors.Select(e => new
            {
                e.Description
            })
        });
    }
}
