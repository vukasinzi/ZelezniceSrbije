using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return PartialView("KorisnikTab", vm);
        }
        [HttpPost]
        public async Task<IActionResult> PromovisiUlogu(string email, string uloga, DateTime? datum, string broj_legitimacije)
        {

            var ok = await servis.PromovisiUlogu(email, uloga, datum, broj_legitimacije);
            if (!ok)
                return BadRequest("Promocija nije uspela.");

            return Ok("Uloga je uspešno promenjena.");
        }
        [HttpPut]
        public async Task<IActionResult> IzmeniAdministratora(int id, string ime, string prezime, string email, DateTime? datum_zaposlenja)
        {

            var ok = await servis.IzmeniAdministratora(id, ime, prezime, email, datum_zaposlenja);
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

        [HttpPost]
        public async Task<IActionResult> DodajTipVoza(string naziv, string opis)
        {
            var ok = await servis.DodajTipVoza(naziv, opis);
            if (!ok)
                return BadRequest("Neuspesno dodavanje!");
            return Ok("Uspesno unesen tip voza!");
        }
        [HttpPost]
        public async Task<IActionResult> DodajVoz(string naziv,string serijski_broj,int tip_voza_id,bool aktivan)
        {
            var ok = await servis.DodajVoz(naziv, serijski_broj,tip_voza_id,aktivan);
            if (!ok)
                return BadRequest("Neuspesno dodavanje!");
            return Ok("Uspesno unesen voza!");
        }
        [HttpGet]
        public async Task<IActionResult> UcitajSveVozove()
        {
            var vm = new VozoviVM();
            vm.Vozovi = await servis.UcitajSveVozove();
            vm.TipoviVoza = await servis.UcitajSveTipoveVoza();
            return PartialView("VozTab", vm);
        }
        [HttpDelete]
        public async Task<IActionResult> UkloniTipVoza(int id)
        {
            var ok = false;
            try
            {
                ok = await servis.UkloniTipVoza(id);
            }
            catch(DbUpdateException)
            {
                return BadRequest("Postoje vozovi sa tim tipom voza. Brisanje onemoguceno.");

            }
            if (!ok)
                return BadRequest("Neuspesno brisanje!");
            return Ok("Uspesno obrisan tip voza!");
        }
        [HttpDelete]
        public async Task<IActionResult> UkloniVoz(int id)
        {
            var ok = false;
            try
            {
                ok = await servis.UkloniVoz(id);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Postoje linije/rasporedi sa tim vozom. Brisanje onemoguceno.");

            }
            if (!ok)
                return BadRequest("Neuspesno brisanje!");
            return Ok("Uspesno obrisan voza!");
        }
        [HttpPut]
        public async Task<IActionResult> IzmeniVoz(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id)
        {
            var ok = await servis.IzmeniVoz(id, naziv, serijski_broj, aktivan, tip_voza_id);
            if (!ok)
                return BadRequest("Neuspesna izmena!");
            return Ok("Uspesno izmenjen voz.");
        }
        public async Task<IActionResult> IzmeniTipVoza(int id, string naziv,string opis)
        {
            var ok = await servis.IzmeniTipVoza(id, naziv, opis);
            if (!ok)
                return BadRequest("Neuspesna izmena!");
            return Ok("Uspesno izmenjen tip voza.");
        }

    }
}
