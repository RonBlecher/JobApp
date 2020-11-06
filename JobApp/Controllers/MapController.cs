using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobApp.Controllers
{
    public class MapController : Controller
    {
        // GET: /<controller>/
        [Authorize(Roles = "Publisher")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Seeker")]
        public IActionResult Seeker()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
