using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Volunteer.Models;

namespace Volunteer.Controllers
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
            var test = new VolunteerUser();
            test.Id = Guid.NewGuid();
            test.Email = "vvindiuk@gmail.com";
            test.FirstName= "Test";



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