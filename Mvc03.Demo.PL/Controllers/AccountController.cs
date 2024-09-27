using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.PL.Helper;
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

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    //Create Token
                    var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                    //Create Reset Password URL
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email,token }, Request.Scheme);
                    //Create Email
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body =url
                    };
                    //Send Email
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(String.Empty, "Invalid Operation , Please Try Again !!");
            }
            return View(model);
        }

        #endregion
        public IActionResult CheckYourInbox() 
        {
            return View();
        }
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user = await UserManager.FindByEmailAsync(email);
                if (user != null) {
                    var result = await UserManager.ResetPasswordAsync(user,token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Reset Password attempt.");
                    }
                }
            }
            ModelState.AddModelError(String.Empty, "Invalid Operation , Please Try Again !!");

            return View();
        }
        public ActionResult AccessDenied()
        {
            return View();
        }


    }
}
