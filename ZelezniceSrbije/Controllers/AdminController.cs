using Microsoft.AspNetCore.Mvc;
using ZelezniceSrbije.Models.ViewModels;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers
{
    public class AdminController : Controller
    {
        private IKorisnikService servis;
        public AdminController(IKorisnikService servis)
        {
            this.servis = servis;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public async Task<IActionResult> UcitajKorisnike()
        {
            var vm = new KorisniciVM();
             vm.Admini = await servis.UcitajSveAdmine();
            vm.Kondukteri = await servis.UcitajSveKonduktere();
            return PartialView("KorisnikTab",vm);
        }
        [HttpPost]
        public async Task<IActionResult> PromovisiUlogu(string email,string uloga,DateTime? datum,string broj_legitimacije)
        {
            var ok = await servis.PromovisiUlogu(email,uloga,datum,broj_legitimacije);
            if (!ok)
                return BadRequest("Promocija nije uspela.");

            return Ok("Uloga je uspešno promenjena.");
        }

    }
}
