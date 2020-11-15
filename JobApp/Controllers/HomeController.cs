using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace JobApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LoginPublishers()
        {
            return RedirectToAction("Login", "publishers");
        }

        public IActionResult LoginAdmins()
        {
            return RedirectToAction("Login", "admins");
        }

        public IActionResult LoginSeekers()
        {
            return RedirectToAction("Login", "seekers");
        }

        public IActionResult NoPermission()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "home");
        }

        public IActionResult Error()
        {
            return View();
        }

        public ActionResult Video()
        {
            return View();
        }

        public ActionResult Articles()
        {
            return View();
        }
    }
}
