using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.PL.ViewModels.Employees;
using Mvc03.Demo.PL.ViewModels.User;

namespace Mvc03.Demo.PL.Controllers
{
    public class UserController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var users = Enumerable.Empty<UserViewModel>();
            if (string.IsNullOrEmpty(searchString))
            {
            }
            else
            {
            }
            return View();
        }
    }
}
