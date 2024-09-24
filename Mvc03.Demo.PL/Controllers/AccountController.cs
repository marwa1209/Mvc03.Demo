using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.PL.ViewModels.Auth;

namespace Mvc03.Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }
        //SignUp
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByNameAsync(model.UserName);
                    if (user == null)
                    {
                        user = await UserManager.FindByEmailAsync(model.Email);
                        if (user == null)
                        {
                            user = new ApplicationUser()
                            {
                                UserName = model.UserName,
                                Email = model.Email,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                IsAgree = model.IsAgree
                            };
                            var result = await UserManager.CreateAsync(user, model.Password);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("SignIn");
                            }
                            else
                            {
                                foreach (var item in result.Errors)
                                {
                                    ModelState.AddModelError(String.Empty, item.Description);
                                }
                            }
                        }
                        ModelState.AddModelError(String.Empty, "Email Is Already Exists !!");
                        return View(model);



                    }
                    ModelState.AddModelError(String.Empty, "UserName Is Already Exists !!");
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(String.Empty, ex.Message);

            }

            return View(model);
        }
        //SignIn
        public IActionResult SignIn()
        {
            return View();
        }

    }
}
