using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SmartCarePatientPortal.Models;
using System.Diagnostics;

namespace SmartCarePatientPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            ViewBag.UserRole = user != null ? user.Role.ToString() : null;
            return View();
        }

        public IActionResult About()
        {
            var user = _userManager.GetUserAsync(User).Result;
            ViewBag.UserRole = user != null ? user.Role.ToString() : null;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
