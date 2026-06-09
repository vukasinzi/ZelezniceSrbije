using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers
{
    /// <summary>
    /// Kontroler za autentifikaciju korisnika.
    /// Omogućava prijavu, odjavu i registraciju korisnika.
    /// </summary>
    public class AuthController : Controller
    {
        /// <summary>
        /// Servis za rad sa korisnicima.
        /// </summary>
        private readonly IKorisnikService servis;

        /// <summary>
        /// Kreira novi kontroler za autentifikaciju.
        /// </summary>
        /// <param name="servis">Servis za rad sa korisnicima.</param>
        public AuthController(IKorisnikService servis)
        {
            this.servis = servis;
        }

        /// <summary>
        /// Prikazuje stranicu za prijavu korisnika.
        /// </summary>
        /// <returns>
        /// View za prijavu ako korisnik nije prijavljen, inače preusmerava na početnu stranicu.
        /// </returns>
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        /// <summary>
        /// Prijavljuje korisnika u sistem.
        /// </summary>
        /// <param name="email">Email korisnika.</param>
        /// <param name="lozinka">Lozinka korisnika.</param>
        /// <returns>
        /// Preusmerava na početnu stranicu ako je prijava uspešna, inače vraća formu sa greškom.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Login(string email, string lozinka)
        {
            var korisnik = await servis.LogInAsync(email, lozinka);
            if (korisnik == null)
            {
                ModelState.AddModelError(string.Empty, "Pogrešna lozinka ili mejl");
                return View("Login");
            }

            var rola = korisnik.GetType().Name;
            Debug.WriteLine(rola);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, korisnik.Id.ToString()),
                new Claim(ClaimTypes.Name, korisnik.Ime),
                new Claim(ClaimTypes.Role, rola)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = true });

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Odjavljuje korisnika iz sistema.
        /// </summary>
        /// <returns>
        /// Preusmerava korisnika na stranicu za prijavu.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        /// <summary>
        /// Registruje novog putnika u sistemu.
        /// </summary>
        /// <param name="ime">Ime putnika.</param>
        /// <param name="prezime">Prezime putnika.</param>
        /// <param name="email">Email putnika.</param>
        /// <param name="broj_telefona">Broj telefona putnika.</param>
        /// <param name="lozinka">Lozinka putnika.</param>
        /// <returns>
        /// Preusmerava na početnu stranicu ako je registracija uspešna, inače vraća formu sa greškom.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Registracija(string ime, string prezime, string email, string broj_telefona, string lozinka)
        {
            Putnik p = new(ime, prezime, email, broj_telefona, lozinka);

            var korisnik = await servis.RegistrujAsync(p);
            if (korisnik == null)
            {
                ModelState.AddModelError(string.Empty, "Podaci nisu validni ili je mejl zauzet");
                return View(p);
            }

            var rola = korisnik.GetType().Name;
            Debug.WriteLine(rola);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, korisnik.Id.ToString()),
                new Claim(ClaimTypes.Name, korisnik.Ime),
                new Claim(ClaimTypes.Role, rola)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = true });

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Prikazuje stranicu za registraciju korisnika.
        /// </summary>
        /// <returns>
        /// View za registraciju ako korisnik nije prijavljen, inače preusmerava na početnu stranicu.
        /// </returns>
        [HttpGet]
        public IActionResult Registracija()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }
    }
}
