using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ZelezniceSrbije.Models.ViewModels;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers
{
    public class AdminController : Controller
    {
        private readonly IKorisnikService korisnikServis;
        private readonly IVozService vozServis;
        private readonly ILinijeServis linijeServis;

        public AdminController(IKorisnikService korisnikServis, IVozService vozServis, ILinijeServis linijeServis)
        {
            this.korisnikServis = korisnikServis;
            this.vozServis = vozServis;
            this.linijeServis = linijeServis;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Administrator"))
                return View();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UcitajKorisnike()
        {
            var vm = new KorisniciVM
            {
                Admini = await korisnikServis.UcitajSveAdmine(),
                Kondukteri = await korisnikServis.UcitajSveKonduktere()
            };
            return PartialView("KorisnikTab", vm);
        }

        [HttpPost]
        public async Task<IActionResult> PromovisiUlogu(string email, string uloga, DateTime? datum, string broj_legitimacije)
        {
            var ok = await korisnikServis.PromovisiUlogu(email, uloga, datum, broj_legitimacije);
            if (!ok)
                return BadRequest("Promocija nije uspela.");

            return Ok("Uloga je uspešno promenjena.");
        }

        [HttpPut]
        public async Task<IActionResult> IzmeniAdministratora(int id, string ime, string prezime, string email, DateTime? datum_zaposlenja)
        {
            var ok = await korisnikServis.IzmeniAdministratora(id, ime, prezime, email, datum_zaposlenja);
            if (!ok)
                return BadRequest("Neuspesna izmena!");

            return Ok("Uspesno izmenjen admin!");
        }

        [HttpPut]
        public async Task<IActionResult> IzmeniKonduktera(int id, string ime, string prezime, string email, string broj_legitimacije)
        {
            var ok = await korisnikServis.IzmeniKonduktera(id, ime, prezime, email, broj_legitimacije);
            if (!ok)
                return BadRequest("Neuspesna izmena!");

            return Ok("Uspesno izmenjen admin!");
        }

        [HttpDelete]
        public async Task<IActionResult> UkloniAdministratora(int id)
        {
            if (id == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Ne mozes obrisati sebe!");

            var ok = await korisnikServis.UkloniAdministratora(id);
            if (!ok)
                return BadRequest("Neuspesno brisanje!");

            return Ok("Uspesno obrisan admin!");
        }

        [HttpDelete]
        public async Task<IActionResult> UkloniKonduktera(int id)
        {
            var ok = await korisnikServis.UkloniKonduktera(id);
            if (!ok)
                return BadRequest("Neuspesno brisanje!");

            return Ok("Uspesno obrisan kondukter!");
        }

        [HttpPost]
        public async Task<IActionResult> DodajTipVoza(string naziv, string opis)
        {
            var ok = await vozServis.DodajTipVoza(naziv, opis);
            if (!ok)
                return BadRequest("Neuspesno dodavanje!");

            return Ok("Uspesno unesen tip voza!");
        }

        [HttpPost]
        public async Task<IActionResult> DodajVoz(string naziv, string serijski_broj, int tip_voza_id, bool aktivan)
        {
            var ok = await vozServis.DodajVoz(naziv, serijski_broj, tip_voza_id, aktivan);
            if (!ok)
                return BadRequest("Neuspesno dodavanje!");

            return Ok("Uspesno unesen voza!");
        }

        [HttpGet]
        public async Task<IActionResult> UcitajSveVozove()
        {
            var vm = new VozoviVM
            {
                Vozovi = await vozServis.UcitajSveVozove(),
                TipoviVoza = await vozServis.UcitajSveTipoveVoza()
            };
            return PartialView("VozTab", vm);
        }

        [HttpDelete]
        public async Task<IActionResult> UkloniTipVoza(int id)
        {
            var ok = false;
            try
            {
                ok = await vozServis.UkloniTipVoza(id);
            }
            catch (DbUpdateException)
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
                ok = await vozServis.UkloniVoz(id);
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
            var ok = await vozServis.IzmeniVoz(id, naziv, serijski_broj, aktivan, tip_voza_id);
            if (!ok)
                return BadRequest("Neuspesna izmena!");

            return Ok("Uspesno izmenjen voz.");
        }

        [HttpPut]
        public async Task<IActionResult> IzmeniTipVoza(int id, string naziv, string opis)
        {
            var ok = await vozServis.IzmeniTipVoza(id, naziv, opis);
            if (!ok)
                return BadRequest("Neuspesna izmena!");

            return Ok("Uspesno izmenjen tip voza.");
        }

        [HttpGet]
        public async Task <IActionResult> UcitajLinijeStanice(string region)
        {
            var vm = new LinijeStaniceVM()
            {
                Linije = await linijeServis.UcitajSveLinije(),
                Stanice = await linijeServis.UcitajSveStanice(region)
            };
          
            return PartialView("LinijeStaniceTab", vm);
           
        }
        [HttpPost]
        public async Task<IActionResult> DodajStanicu(string naziv,string region)
        {
            var ok = await linijeServis.DodajStanicu(naziv, region);
            if (!ok)
                return BadRequest("Neuspesno dodavanje!");

            return Ok("Uspesno dodata stanica.");
        }
        [HttpPost]
        public async Task<IActionResult> DodajLiniju(string naziv, int cena_po_minutu, List<int> stanicaIds, List<int> redosled, List<int> vreme_od_polaska)
        {
            if (!ModelState.IsValid)
                return BadRequest("Cena po minutu mora biti broj.");

            var ok = await linijeServis.DodajLiniju(naziv, cena_po_minutu,stanicaIds,redosled,vreme_od_polaska);
            if (!ok)
                return BadRequest("Neuspesno dodavanje!");

            return Ok("Uspesno dodata linija.");
        }
    }
}
