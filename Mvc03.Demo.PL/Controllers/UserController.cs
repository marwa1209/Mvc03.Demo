using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.PL.Helper;
using Mvc03.Demo.PL.ViewModels.Employees;
using Mvc03.Demo.PL.ViewModels.User;

namespace Mvc03.Demo.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        #region Index
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString)
        {
            var users = Enumerable.Empty<UserViewModel>();
            if (string.IsNullOrEmpty(searchString))
            {
                users = await UserManager.Users.Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = UserManager.GetRolesAsync(u).Result
                }
                 ).ToListAsync();
            }
            else
            {
                users = await UserManager.Users.Where(u => u.Email
                .ToLower()
                .Contains(searchString.ToLower()))
                       .Select(u => new UserViewModel()
                       {
                           Id = u.Id,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Email = u.Email,
                           Roles = UserManager.GetRolesAsync(u).Result
                       }).ToListAsync();
            }
            return View(users);
        }
        #endregion
        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var userFromDb = await UserManager.FindByIdAsync(id);
            if (userFromDb == null) return BadRequest();
            var user = new UserViewModel
            {
                Id = id,
                FirstName = userFromDb.FirstName,
                LastName = userFromDb.LastName,
                Email = userFromDb.Email,
                Roles = UserManager.GetRolesAsync(userFromDb).Result
            };
            return View(viewName, user);
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
        public async Task<IActionResult> Update([FromRoute] string? id, UserViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var userFromDb = await UserManager.FindByIdAsync(id);
                if (userFromDb == null) return BadRequest();
                userFromDb.FirstName = model.FirstName;
                userFromDb.LastName = model.LastName;
                userFromDb.Email = model.Email;
                await UserManager.UpdateAsync(userFromDb);

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

            var userFromDb = await UserManager.FindByIdAsync(id);
            if (userFromDb == null) return BadRequest();

            var result = await UserManager.DeleteAsync(userFromDb);
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
            var users = await UserManager.Users.ToListAsync();
            return View("Index", users); // Keep the user list view if deletion fails
        }


        #endregion
    }
}
