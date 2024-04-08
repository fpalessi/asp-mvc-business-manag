using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SandraConfecciones.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SandraConfecciones.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string userEmail = "";

            if (claimUser.Identity.IsAuthenticated)
            {
                userEmail = claimUser.Claims.Where(c => c.Type == ClaimTypes.Email)
                    .Select(c => c.Value).SingleOrDefault();
            }

            ViewData["userEmail"] = userEmail;
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
    }
}
