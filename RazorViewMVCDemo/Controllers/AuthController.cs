using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RazorViewMVCDemo.Models;
using RazorViewMVCDemo.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RazorViewMVCDemo.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userMgr;
        private readonly SignInManager<User> _signInMgr;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userMgr = userManager;
            _signInMgr = signInManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // get user by email
            var user = await _userMgr.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Invalid", "Invalid Credentials");
            }

            // Todo: If check for email confirmation, Phone number confirmation is require; then go ahead from here below

            var res = await _signInMgr.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!res.Succeeded)
            {
                ModelState.AddModelError("", "invalid credentials");
                return View(model);
            }


            ViewBag.IsLoggedOut = "false";
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public  IActionResult ConfirmEmail(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
            {
                ViewBag.ErrMsg = "Invalid email or token";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInMgr.SignOutAsync();
            ViewBag.IsLoggedOut = "true";
            return RedirectToAction("Login");
        }

    }
}


// Redirection in MVC
// TagHelpers
// Model Binding
// Passing date between controller and view
