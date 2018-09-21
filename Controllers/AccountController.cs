using System;
using System.Threading.Tasks;
using app_test_jmeter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace app_test_jmeter.Controllers
{
    public class AccountController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController( UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login(string returnurl = null)
        {
            ViewData["returnurl"] = returnurl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginViewModel model, string returnurl = null)
        {

            ViewData["returnurl"] = returnurl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnurl) && returnurl.Trim().Length > 0)
                        return Redirect(returnurl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                { 
                    ViewData["Status"] = "Login Inválido!";
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}