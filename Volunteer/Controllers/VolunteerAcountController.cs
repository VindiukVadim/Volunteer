using Microsoft.AspNetCore.Mvc;
using Volunteer.Data;

namespace Volunteer.Controllers
{
    public class VolunteerAcountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VolunteerAcountController(ApplicationDbContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
