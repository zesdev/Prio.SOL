using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prio.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Prio.MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View(new UserViewModel());
        }
        [HttpPost]
        public IActionResult Login(UserViewModel model)
        {
            if (model.Username != null)
            {
                if (model.Username.ToLower() == "erik")
                {
                    if (model.Password == "Logon123")
                    {
                        var userClaims = new List<Claim>()
                {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.Email, ""),
                 };

                        var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

                        var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                        HttpContext.SignInAsync(userPrincipal);
                        var identity = User.Identity;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewBag.errorMessage = "Fel användarnamn eller Lösenord";
            return View(model);
        }
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
