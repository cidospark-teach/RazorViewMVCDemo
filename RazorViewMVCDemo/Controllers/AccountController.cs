using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RazorViewMVCDemo.Models;
using RazorViewMVCDemo.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RazorViewMVCDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userMgr;
        private readonly SignInManager<User> _signInMgr;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            IMapper mapper)
        {
            _userMgr = userManager;
            _signInMgr = signInManager;
            _mapper = mapper;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("Register");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // check if the email already exists
            var ExistingUser = await _userMgr.FindByEmailAsync(model.Email);
            if(ExistingUser != null)
            {
                ModelState.AddModelError("Invalid", "Email already exists");
                return View(model);
            }

            // map viewmodel to user model
            var user = _mapper.Map<User>(model);

            // add user
            // P@ssw0rd123
            user.UserName = model.Email;
            var res = await _userMgr.CreateAsync(user, model.Password);
            if (!res.Succeeded)
            {
                foreach(var err in res.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }

            // add role to user
            var roleRes = await _userMgr.AddToRoleAsync(user, "Regular");
            if(!roleRes.Succeeded)
            {
                foreach (var err in roleRes.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }

            // generate email confirmation token and send
            var token = await _userMgr.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action("ConfirmEmail", "Auth", new { token, email = model.Email });

            // send an email to the user using the email provided
            // TODO:



            return RedirectToAction("Index", "Home");
        }


    }
}
