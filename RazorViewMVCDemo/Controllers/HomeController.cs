using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RazorViewMVCDemo.Models;
using RazorViewMVCDemo.ViewModels;

namespace RazorViewMVCDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userMgr;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, IMapper mapper)
        {
            _logger = logger;
            _userMgr = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var users = _userMgr.Users.Include(x => x.Photos).ToList() ;

            //var usersToDisplay = _mapper.Map<>();

            // map to viewmodel
            var usersToDisplay = new List<UsersToDisplayViewModel>();

            foreach(var user in users)
            {
                usersToDisplay.Add(new UsersToDisplayViewModel
                {
                    FullName = $"{user.LastName} {user.FirstName}",
                    ActiveStatus = user.IsActive? "Active" : "Not Active",
                    Email = user.Email,
                    Address = $"{user.Street}, {user.State}, {user.Country}",
                    Photo = user.Photos.FirstOrDefault().ToString()
                });
            }

            return View(usersToDisplay);
        }

        public IActionResult Privacy()
        {
            //ViewBag.HisTitle = "Home Page";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
