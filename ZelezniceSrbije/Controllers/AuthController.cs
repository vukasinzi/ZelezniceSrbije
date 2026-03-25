using Microsoft.AspNetCore.Mvc;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers
{
    public class AuthController : Controller
    {

        private readonly IKorisnikService servis;
        public AuthController(KorisnikService servis)
        {
            this.servis = servis;
        }
        [HttpGet]
        public IActionResult Login(string email,string lozinka)
        {
            return View(servis.LogInAsync(email, lozinka));
            
        }
    }
}
