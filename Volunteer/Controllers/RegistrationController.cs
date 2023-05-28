using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using Volunteer.Models;
using System.Data;
using Volunteer.Data;

namespace Volunteer.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public RegistrationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult SigIn() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SigIn(ApplicationUser model, int role, string Password)
        {
            if (model != null)
            {
                if (role == 1)
                {
                    var user = new VolunteerUser { FirstName = model.FirstName, SecondName = model.SecondName, Email = model.Email, UserName = model.Email };

                    var result = await _userManager.CreateAsync(user, Password);
                    if (result.Succeeded)
                    {
                        user.EmailConfirmed = true;
                        await _userManager.UpdateAsync(user);
                        await _userManager.AddToRoleAsync(user, "Volunteer");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                if (role == 2)
                {
                    var user = new SoldierUser { FirstName = model.FirstName, SecondName = model.SecondName, Email = model.Email, UserName = model.Email };

                    var result = await _userManager.CreateAsync(user, Password);
                    if (result.Succeeded)
                    {
                        user.EmailConfirmed = true;
                        await _userManager.UpdateAsync(user);
                        await _userManager.AddToRoleAsync(user, "Soldier");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                //сообщать об ошибке
                return View();
            }
            return View();
        }

        public async Task<IActionResult> LogIn(string email, string password, bool rememberMe)
        {
            if (email != null && password != null)
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                return RedirectToAction("Index", "VolunteerAcount");
            }
            else
            {
                return View();
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
