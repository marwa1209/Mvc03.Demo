using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.PL.Helper;
using Mvc03.Demo.PL.ViewModels.Employees;
using Mvc03.Demo.PL.ViewModels.Roles;
using Mvc03.Demo.PL.ViewModels.User;

namespace Mvc03.Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        public RoleManager<IdentityRole> _roleManager { get; }

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        #region Index
        public async Task<IActionResult> Index(string searchString)
        {
            var roles = Enumerable.Empty<RoleViewModel>();
            if (string.IsNullOrEmpty(searchString))
            {
                roles = await _roleManager.Roles.Select(u => new RoleViewModel()
                {
                    Id = u.Id,
                    RoleName = u.Name
                }
                 ).ToListAsync();
            }
            else
            {
                roles = await _roleManager.Roles.Where(u => u.Name
                .ToLower()
                .Contains(searchString.ToLower()))
                       .Select(u => new RoleViewModel()
                       {
                           Id = u.Id,
                           RoleName = u.Name

                       }).ToListAsync();
            }
            return View(roles);
        }
        #endregion
        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var roleFromDb = await _roleManager.FindByIdAsync(id);
            if (roleFromDb == null) return BadRequest();
            var role = new RoleViewModel
            {
                Id = roleFromDb.Id,
                RoleName = roleFromDb.Name

            };
            return View(viewName, role);
        }
        #endregion
        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = model.RoleName
                };

                await _roleManager.CreateAsync(role);
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }
        #endregion
        [HttpGet]
        #region Update
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string? id, RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var roleFromDb = await _roleManager.FindByIdAsync(id);
                if (roleFromDb == null) return BadRequest();
                roleFromDb.Name = model.RoleName;
                await _roleManager.UpdateAsync(roleFromDb);

                return RedirectToAction(nameof(Index));

            }

            return View(model);
        }

        #endregion
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null) return BadRequest();

            var roleFromDb = await _roleManager.FindByIdAsync(id);
            if (roleFromDb == null) return BadRequest();

            var result = await _roleManager.DeleteAsync(roleFromDb);
            if (result.Succeeded)
            {
                // Redirect to the Index action after successful deletion
                return RedirectToAction(nameof(Index));
            }

            // If something went wrong, you can handle errors here and show them in the Index view
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // Redirecting to Index view and passing current users list
            var roles = await _roleManager.Roles.ToListAsync();
            return View("Index", roles);
        }
        #endregion
    }
}
