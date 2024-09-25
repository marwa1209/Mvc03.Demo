using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.PL.ViewModels.Auth;

namespace Mvc03.Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        #region SignUp
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
                                return RedirectToAction("SignIn", "Account");
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
        #endregion
        #region SignIn
        //SignIn
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var flag = await UserManager.CheckPasswordAsync(user, model.Password);
                        if (flag)
                        {
                            var result = await SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            }
                        }
                    }
                    ModelState.AddModelError(String.Empty, "Invalid Email !!");
                    return View(model);

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);

            }

            return View(model);
        } 
        #endregion

        public new async Task<IActionResult> SignOut()
        {
           await SignInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }


    }
}
