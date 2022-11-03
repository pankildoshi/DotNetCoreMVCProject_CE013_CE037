using CollegeApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using stationary.Models;
using System.Security.Claims;

namespace stationary.Controllers
{
    public class LoginController : Controller
    {

        private readonly IAdminRepository _adminRepo;
        public LoginController(IAdminRepository adminRepo)
        {
            _adminRepo = adminRepo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin admin)
        { 
            if (ModelState.IsValid)
            {
                var data = _adminRepo.GetAdmin(admin.Name, admin.Password);
                if(data != null)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, admin.Name) }, 
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    var principle = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
                    HttpContext.Session.SetString("username", admin.Name);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["errorLogin"] = "Invalid Username or Password";
                    return View(admin);
                }
            }
            return View(admin);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var cookies = Request.Cookies.Keys;
            foreach(var cookie in cookies)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Login", "Login");
        }
    }

}
