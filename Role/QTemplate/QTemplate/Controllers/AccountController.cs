using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace QTemplate.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == "sefa" && password == "123123" ||
                username == "admin" && password == "123123")
            {

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, username)
                };

                if (username == "sefa")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                }
                else if (username == "admin")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }

                var identiy = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(identiy);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}