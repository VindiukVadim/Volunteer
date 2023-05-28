using Microsoft.AspNetCore.Mvc;
using Volunteer.Data;
using Volunteer.Migrations;
using Volunteer.Models;

namespace Volunteer.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {


            return View();
        }
    }
}
