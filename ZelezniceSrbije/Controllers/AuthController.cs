using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZelezniceSrbije.Models;
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
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
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
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,new AuthenticationProperties { IsPersistent = true});


            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Auth");
        }
        [HttpPost]
        public async Task<IActionResult> Registracija(string ime,string prezime,string email,string broj_telefona, string lozinka)
        {
            Putnik p = new(ime, prezime, email, broj_telefona, lozinka);
            var korisnik = await servis.RegistrujAsync(p);
            if(korisnik == null)
            {
                ModelState.AddModelError("", "Već postoji korisnik sa tim mejlom.");
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
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Registracija()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");   
            return View();
        }

    }
}
