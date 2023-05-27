using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Volunteer.Data;
using Volunteer.Models;

namespace Volunteer.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizationController(ApplicationDbContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            TempData["Controller"] = "Organization";
            TempData["Method"] = "RemoveOrganization";
            var administrator = User.Identity.Name;
            var adminID = _context.VolunteerUsers.FirstOrDefault(u => u.UserName == administrator);
            var listOrg = _context.Organizations.Where(o=>o.MainVolunteerId == adminID.Id).ToList();
            return View(listOrg);
        }

        public ActionResult CreateOrganization()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrganization(Organization organization)
        {
            // Create a new organization and assign an administrator
            var administrator = User.Identity.Name;
            var adminID = _context.VolunteerUsers.FirstOrDefault(u => u.UserName == administrator);

            var newOrg = new Organization();
            newOrg.Name = organization.Name;
            newOrg.Description= organization.Description;
            newOrg.Id = Guid.NewGuid();
            newOrg.MainVolunteerId = adminID.Id;
            _context.Organizations.Add(newOrg);
            _context.SaveChanges();
            return RedirectToAction("Index", "Organization");
        }

        [HttpPost]
        public IActionResult RemoveOrganization(Guid id)
        {
            var organization = _context.Organizations.FirstOrDefault(t => t.Id == id);
            if (organization != null)
            {
                _context.Organizations.Remove(organization);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditOrganization(Guid id)
        {
            TempData["Controller"] = "Organization";
            TempData["Method"] = "EditOrganization";
            var organization = _context.Organizations.FirstOrDefault(o => o.Id == id);
            ViewBag.ListOfVolunteer = _context.VolunteerUsers.ToList();
            return View(organization);

        }
        [HttpPost]
        public IActionResult EditOrganization(Organization organization, List<Guid> addlistUserId)
        {
            var updateOrg = _context.Organizations.FirstOrDefault(o => o.Id == organization.Id);
            if (updateOrg != null)
            {
                updateOrg.Name = organization.Name;
                updateOrg.Description = organization.Description;
                _context.Organizations.Update(updateOrg);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult ViewAllInformationCompositionOrganization(Guid id)
        {
            TempData["ControllerAdd"] = "Organization";
            TempData["MethodAdd"] = "AddVolunteersToOrganization";
            TempData["ControllerRemove"] = "Organization";
            TempData["MethodRemove"] = "RemoveVolunteersFromOrganization";
            TempData["IdOrg"] = id;
            var organization = _context.Organizations.Include(o => o.VolunteerUsers).FirstOrDefault(u => u.Id == id);
            ViewBag.MainUser = _context.VolunteerUsers.FirstOrDefault(u => u.Id == organization.MainVolunteerId);
            ViewBag.ListOfUser = _context.VolunteerUsers.ToList();
            ViewBag.CurrentUser = _context.VolunteerUsers.Where(u => u.OrganizationId == id).ToList();

            return View(organization);
        }

        public IActionResult AddVolunteersToOrganization(Guid organizationId, List<Guid> addlistUserId)
        {
            var updateOrg = _context.Organizations.Include(o => o.VolunteerUsers).FirstOrDefault(u => u.Id == organizationId);
            var currentVolunteers = updateOrg.VolunteerUsers.ToList();
            var volunteerUsers = new List<VolunteerUser>();
            foreach (var item in addlistUserId)
            {
                var newUser = _context.VolunteerUsers.FirstOrDefault(u => u.Id == item);
                volunteerUsers.Add(newUser);
            }
            var volunteersToAdd = volunteerUsers.Except(currentVolunteers).ToList();
            updateOrg.VolunteerUsers.AddRange(volunteersToAdd);
            _context.Organizations.Update(updateOrg);
            _context.SaveChanges();
            return RedirectToAction("ViewAllInformationCompositionOrganization", new RouteValueDictionary { { "Id", organizationId } });

        }
        public IActionResult RemoveVolunteersFromOrganization(Guid organizationId, List<Guid> addlistUserId)
        {
            var updateOrg = _context.Organizations.Include(o => o.VolunteerUsers).FirstOrDefault(u => u.Id == organizationId);
            var currentVolunteers = updateOrg.VolunteerUsers.ToList();
            var volunteerUsers = new List<VolunteerUser>();
            foreach (var item in addlistUserId)
            {
                var newUser = _context.VolunteerUsers.FirstOrDefault(u => u.Id == item);
                volunteerUsers.Add(newUser);
            }
            var volunteersToAdd = currentVolunteers.Except(volunteerUsers).ToList();
            updateOrg.VolunteerUsers.Clear();
            updateOrg.VolunteerUsers.AddRange(volunteersToAdd);
            _context.Organizations.Update(updateOrg);
            _context.SaveChanges();
            return RedirectToAction("ViewAllInformationCompositionOrganization", new RouteValueDictionary { { "Id", organizationId } });
        }

        //public void AddVolunteersToOrganization(Organization organization, List<Guid> addlistUserId)
        //{
        //    var org = _context.Organizations.FirstOrDefault(o => o.Id == organization.Id);
        //    if (organization == null)
        //    {
        //        var volunteerUsers = new List<VolunteerUser>();
        //        foreach (var item in addlistUserId)
        //        {
        //            var newUser = _context.VolunteerUsers.FirstOrDefault(u => u.Id == item);
        //            volunteerUsers.Add(newUser);
        //        }
        //        organization.VolunteerUsers = volunteerUsers;
        //    }
        //}
    }
}
