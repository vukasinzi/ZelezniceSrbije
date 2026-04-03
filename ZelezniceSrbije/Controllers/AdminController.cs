using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        [HttpPut]
        public async Task<IActionResult> IzmeniAdministratora(int id,string ime,string prezime,string email,DateTime? datum_zaposlenja)
        {
        
            var ok = await servis.IzmeniAdministratora(id,ime, prezime, email, datum_zaposlenja);
            if (!ok)
                return BadRequest("Neuspesna izmena!");
            return Ok("Uspesno izmenjen admin!");
        }
        [HttpPut]
        public async Task<IActionResult> IzmeniKonduktera(int id, string ime, string prezime, string email, string broj_legitimacije)
        {
            var ok = await servis.IzmeniKonduktera(id, ime, prezime, email, broj_legitimacije);
            if (!ok)
                return BadRequest("Neuspesna izmena!");
            return Ok("Uspesno izmenjen admin!");
        }

        [HttpDelete]
        public async Task<IActionResult> UkloniAdministratora(int id)
        {
        
            if (id == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Ne mozes obrisati sebe!");
            var ok = await servis.UkloniAdministratora(id);
            if (!ok)
                return BadRequest("Neuspesno brisanje!");
            return Ok("Uspesno obrisan admin!");
       
        }

        [HttpDelete]
        public async Task<IActionResult> UkloniKonduktera(int id)
        {

            var ok = await servis.UkloniKonduktera(id);
            if (!ok)
                return BadRequest("Neuspesno brisanje!");
            return Ok("Uspesno obrisan kondukter!");
        }

    }
}
