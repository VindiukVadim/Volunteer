using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Volunteer.Data;
using Volunteer.Models;

namespace Volunteer.Controllers
{
    public class MilitaryUnitController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MilitaryUnitController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            TempData["Controller"] = "MilitaryUnit";
            TempData["Method"] = "RemoveMilitaryUnit";
            var administrator = User.Identity.Name;
            var adminID = _context.SoldierUsers.FirstOrDefault(u => u.UserName == administrator);
            var listUnit = _context.MilitaryUnits.Where(o => o.MainSoldierId == adminID.Id).ToList();
            return View(listUnit);
        }

        [HttpPost]
        public IActionResult RemoveMilitaryUnit(Guid id)
        {
            var militaryUnit = _context.MilitaryUnits.FirstOrDefault(t => t.Id == id);
            if (militaryUnit != null)
            {
                _context.MilitaryUnits.Remove(militaryUnit);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateMilitaryUnit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMilitaryUnit(MilitaryUnit militaryUnit)
        {
            // Create a new organization and assign an administrator
            var administrator = User.Identity.Name;
            var adminID = _context.SoldierUsers.FirstOrDefault(u => u.UserName == administrator);

            var newUnit = new MilitaryUnit();
            newUnit.Name = militaryUnit.Name;
            newUnit.Description = militaryUnit.Description;
            newUnit.Id = Guid.NewGuid();
            newUnit.MainSoldierId = adminID.Id;
            newUnit.SoldierUsers = new List<SoldierUser> { adminID };
            _context.MilitaryUnits.Add(newUnit);
            _context.SaveChanges();
            return RedirectToAction("Index", "MilitaryUnit");
        }

        public IActionResult EditMilitaryUnit(Guid id)
        {
            TempData["Id"] = id;
            var militaryUnit = _context.MilitaryUnits.FirstOrDefault(o => o.Id == id);
            ViewBag.ListOfSoldier = _context.SoldierUsers.ToList();
            ViewBag.CurrentSoldier = _context.SoldierUsers.Where(u => u.MilitaryUnit.Id == id).ToList();
            return View(militaryUnit);

        }
        [HttpPost]
        public IActionResult EditMilitaryUnit(MilitaryUnit militaryUnit)
        {
            var updateUnit = _context.MilitaryUnits.FirstOrDefault(o => o.Id == militaryUnit.Id);
            if (updateUnit != null)
            {
                updateUnit.Name = militaryUnit.Name;
                updateUnit.Description = militaryUnit.Description;
                _context.MilitaryUnits.Update(updateUnit);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult ViewAllInformationCompositionMilitaryUnut(Guid id)
        {
            TempData["ControllerAdd"] = "MilitaryUnit";
            TempData["MethodAdd"] = "AddSoldiersToMilitaryUnit";
            TempData["ControllerRemove"] = "MilitaryUnit";
            TempData["MethodRemove"] = "RemoveSoldiersFromMilitaryUnit";
            TempData["IdUnit"] = id;
            var militaryUnit = _context.MilitaryUnits.Include(o => o.SoldierUsers).FirstOrDefault(u => u.Id == id);
            ViewBag.MainSoldier = _context.SoldierUsers.FirstOrDefault(u => u.Id == militaryUnit.MainSoldierId);
            ViewBag.ListOfSoldier = _context.SoldierUsers.ToList();
            ViewBag.CurrentVolonteer = _context.SoldierUsers.Where(u => u.MilitaryUnitId == id).ToList();

            return View(militaryUnit);
        }

        public IActionResult AddSoldiersToMilitaryUnit(Guid militaryUnitId, List<Guid> addlistUserId)
        {
            var updateUnit = _context.MilitaryUnits.Include(o => o.SoldierUsers).FirstOrDefault(u => u.Id == militaryUnitId);
            var currentSoldier = updateUnit.SoldierUsers.ToList();
            var soldierUsers = new List<SoldierUser>();
            foreach (var item in addlistUserId)
            {
                var newUser = _context.SoldierUsers.FirstOrDefault(u => u.Id == item);
                soldierUsers.Add(newUser);
            }
            var soldierToAdd = soldierUsers.Except(currentSoldier).ToList();
            updateUnit.SoldierUsers.AddRange(soldierToAdd);
            _context.MilitaryUnits.Update(updateUnit);
            _context.SaveChanges();
            return RedirectToAction("ViewAllInformationCompositionMilitaryUnut", new RouteValueDictionary { { "Id", militaryUnitId } });

        }
        public IActionResult RemoveSoldiersFromMilitaryUnit(Guid militaryUnitId, List<Guid> addlistUserId)
        {
            var updateUnit = _context.MilitaryUnits.Include(o => o.SoldierUsers).FirstOrDefault(u => u.Id == militaryUnitId);
            var currentSoldier = updateUnit.SoldierUsers.ToList();
            var soldierUsers = new List<SoldierUser>();
            foreach (var item in addlistUserId)
            {
                var newUser = _context.SoldierUsers.FirstOrDefault(u => u.Id == item);
                soldierUsers.Add(newUser);
            }
            var soldierToAdd = currentSoldier.Except(soldierUsers).ToList();
            updateUnit.SoldierUsers.Clear();
            updateUnit.SoldierUsers.AddRange(soldierToAdd);
            _context.MilitaryUnits.Update(updateUnit);
            _context.SaveChanges();
            return RedirectToAction("ViewAllInformationCompositionMilitaryUnut", new RouteValueDictionary { { "Id", militaryUnitId } });
        }

    }
}
