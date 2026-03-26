using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Principal;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers
{
    public class AuthController : Controller
    {

        private readonly IKorisnikService servis;
        public AuthController(IKorisnikService servis)
        {
            this.servis = servis;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email,string lozinka)
        {
            var korisnik = await servis.LogInAsync(email, lozinka);
            if (korisnik == null)
            {
                ModelState.AddModelError("", "Pogresna lozinka ili mejl");
                return View();
            }
            var rola = korisnik.GetType().Name;
            var claims = new List<Claim>
             {
                new Claim(ClaimTypes.NameIdentifier, korisnik.Id.ToString()),
                new Claim(ClaimTypes.Name, korisnik.Ime),
                new Claim(ClaimTypes.Role, rola) 
             };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
