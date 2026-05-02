using System.Diagnostics;
using AuthProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuthProject.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // Any logged-in user:
        [Authorize]
        public IActionResult Dashboard() => View();

        // Admin only:
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel() => View();

        // Admin OR Editor:
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult EditContent() => View();

        // Public (no login needed):
        [AllowAnonymous]
        public IActionResult About() => View();
    }
}
