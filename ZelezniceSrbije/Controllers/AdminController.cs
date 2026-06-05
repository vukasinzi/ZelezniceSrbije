using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ZelezniceSrbije.Models.ViewModels;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers
{
    /// <summary>
    /// Kontroler za administraciju sistema.
    /// Omogućava upravljanje korisnicima, vozovima, linijama, stanicama i rasporedima.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        /// <summary>
        /// Servis za rad sa korisnicima.
        /// </summary>
        private readonly IKorisnikService korisnikServis;

        /// <summary>
        /// Servis za rad sa vozovima.
        /// </summary>
        private readonly IVozService vozServis;

        /// <summary>
        /// Servis za rad sa linijama i stanicama.
        /// </summary>
        private readonly ILinijeServis linijeServis;

        /// <summary>
        /// Servis za rad sa rasporedima vožnje.
        /// </summary>
        private readonly IRasporedService rasporedServis;

        /// <summary>
        /// Kreira novi administratorski kontroler.
        /// </summary>
        /// <param name="korisnikServis">Servis za rad sa korisnicima.</param>
        /// <param name="vozServis">Servis za rad sa vozovima.</param>
        /// <param name="linijeServis">Servis za rad sa linijama i stanicama.</param>
        /// <param name="rasporedServis">Servis za rad sa rasporedima.</param>
        public AdminController(IKorisnikService korisnikServis, IVozService vozServis, ILinijeServis linijeServis, IRasporedService rasporedServis)
        {
            this.korisnikServis = korisnikServis;
            this.vozServis = vozServis;
            this.linijeServis = linijeServis;
            this.rasporedServis = rasporedServis;
        }

        /// <summary>
        /// Prikazuje početnu stranicu admin panela.
        /// </summary>
        /// <returns>
        /// View ako je korisnik administrator, inače preusmerava na početnu stranicu.
        /// </returns>
        public IActionResult Index()
        {
            if (User.IsInRole("Administrator"))
                return View();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Učitava administratore i konduktere.
        /// </summary>
        /// <returns>
        /// PartialView sa podacima o administratorima i kondukterima.
        /// </returns>
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

        /// <summary>
        /// Promoviše korisnika u zadatu ulogu.
        /// </summary>
        /// <param name="email">Email korisnika.</param>
        /// <param name="uloga">Nova uloga korisnika.</param>
        /// <param name="datum">Datum zaposlenja korisnika.</param>
        /// <param name="broj_legitimacije">Broj legitimacije konduktera.</param>
        /// <returns>
        /// Ok ako je uloga uspešno promenjena, inače BadRequest.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PromovisiUlogu(string email, string uloga, DateTime? datum, string broj_legitimacije)
        {
            var ok = await korisnikServis.PromovisiUlogu(email, uloga, datum, broj_legitimacije);
            if (!ok)
                return BadRequest("Promocija nije uspela.");

            return Ok("Uloga je uspe�no promenjena.");
        }

        /// <summary>
        /// Menja podatke administratora.
        /// </summary>
        /// <param name="id">Identifikator administratora.</param>
        /// <param name="ime">Ime administratora.</param>
        /// <param name="prezime">Prezime administratora.</param>
        /// <param name="email">Email administratora.</param>
        /// <param name="datum_zaposlenja">Datum zaposlenja administratora.</param>
        /// <returns>
        /// Ok ako je administrator uspešno izmenjen, inače BadRequest.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> IzmeniAdministratora(int id, string ime, string prezime, string email, DateTime? datum_zaposlenja)
        {
            var ok = await korisnikServis.IzmeniAdministratora(id, ime, prezime, email, datum_zaposlenja);
            if (!ok)
                return BadRequest("Neuspesna izmena!");

            return Ok("Uspesno izmenjen admin!");
        }

        /// <summary>
        /// Menja podatke konduktera.
        /// </summary>
        /// <param name="id">Identifikator konduktera.</param>
        /// <param name="ime">Ime konduktera.</param>
        /// <param name="prezime">Prezime konduktera.</param>
        /// <param name="email">Email konduktera.</param>
        /// <param name="broj_legitimacije">Broj legitimacije konduktera.</param>
        /// <returns>
        /// Ok ako je kondukter uspešno izmenjen, inače BadRequest.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> IzmeniKonduktera(int id, string ime, string prezime, string email, string broj_legitimacije)
        {
            var ok = await korisnikServis.IzmeniKonduktera(id, ime, prezime, email, broj_legitimacije);
            if (!ok)
                return BadRequest("Neuspesna izmena!");

            return Ok("Uspesno izmenjen admin!");
        }

        /// <summary>
        /// Uklanja administratora iz sistema.
        /// </summary>
        /// <param name="id">Identifikator administratora.</param>
        /// <returns>
        /// Ok ako je administrator uspešno obrisan, inače BadRequest.
        /// </returns>
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

        /// <summary>
        /// Uklanja konduktera iz sistema.
        /// </summary>
        /// <param name="id">Identifikator konduktera.</param>
        /// <returns>
        /// Ok ako je kondukter uspešno obrisan, inače BadRequest.
        /// </returns>
        [HttpDelete]
        public async Task<IActionResult> UkloniKonduktera(int id)
        {
            var ok = await korisnikServis.UkloniKonduktera(id);
            if (!ok)
                return BadRequest("Neuspesno brisanje!");

            return Ok("Uspesno obrisan kondukter!");
        }

        /// <summary>
        /// Dodaje novi tip voza.
        /// </summary>
        /// <param name="naziv">Naziv tipa voza.</param>
        /// <param name="opis">Opis tipa voza.</param>
        /// <returns>
        /// Ok ako je tip voza uspešno dodat, inače BadRequest.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> DodajTipVoza(string naziv, string opis)
        {
            var ok = await vozServis.DodajTipVoza(naziv, opis);
            if (!ok)
                return BadRequest("Neuspesno dodavanje!");

            return Ok("Uspesno unesen tip voza!");
        }

        /// <summary>
        /// Dodaje novi voz.
        /// </summary>
        /// <param name="naziv">Naziv voza.</param>
        /// <param name="serijski_broj">Serijski broj voza.</param>
        /// <param name="tip_voza_id">Identifikator tipa voza.</param>
        /// <param name="aktivan">Označava da li je voz aktivan.</param>
        /// <returns>
        /// Ok ako je voz uspešno dodat, inače BadRequest.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> DodajVoz(string naziv, string serijski_broj, int tip_voza_id, bool aktivan)
        {
            var ok = await vozServis.DodajVoz(naziv, serijski_broj, tip_voza_id, aktivan);
            if (!ok)
                return BadRequest("Neuspesno dodavanje!");

            return Ok("Uspesno unesen voza!");
        }

        /// <summary>
        /// Učitava sve vozove i tipove vozova.
        /// </summary>
        /// <returns>
        /// PartialView sa podacima o vozovima i tipovima vozova.
        /// </returns>
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

        /// <summary>
        /// Uklanja tip voza iz sistema.
        /// </summary>
        /// <param name="id">Identifikator tipa voza.</param>
        /// <returns>
        /// Ok ako je tip voza uspešno obrisan, inače BadRequest.
        /// </returns>
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

        /// <summary>
        /// Uklanja voz iz sistema.
        /// </summary>
        /// <param name="id">Identifikator voza.</param>
        /// <returns>
        /// Ok ako je voz uspešno obrisan, inače BadRequest.
        /// </returns>
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

        /// <summary>
        /// Menja podatke voza.
        /// </summary>
        /// <param name="id">Identifikator voza.</param>
        /// <param name="naziv">Naziv voza.</param>
        /// <param name="serijski_broj">Serijski broj voza.</param>
        /// <param name="aktivan">Označava da li je voz aktivan.</param>
        /// <param name="tip_voza_id">Identifikator tipa voza.</param>
        /// <returns>
        /// Ok ako je voz uspešno izmenjen, inače BadRequest.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> IzmeniVoz(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id)
        {
            var ok = await vozServis.IzmeniVoz(id, naziv, serijski_broj, aktivan, tip_voza_id);
            if (!ok)
                return BadRequest("Neuspesna izmena!");

            return Ok("Uspesno izmenjen voz.");
        }

        /// <summary>
        /// Menja podatke tipa voza.
        /// </summary>
        /// <param name="id">Identifikator tipa voza.</param>
        /// <param name="naziv">Naziv tipa voza.</param>
        /// <param name="opis">Opis tipa voza.</param>
        /// <returns>
        /// Ok ako je tip voza uspešno izmenjen, inače BadRequest.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> IzmeniTipVoza(int id, string naziv, string opis)
        {
            var ok = await vozServis.IzmeniTipVoza(id, naziv, opis);
            if (!ok)
                return BadRequest("Neuspesna izmena!");

            return Ok("Uspesno izmenjen tip voza.");
        }

        /// <summary>
        /// Učitava linije i stanice za zadati region.
        /// </summary>
        /// <param name="region">Region za koji se učitavaju stanice.</param>
        /// <returns>
        /// PartialView sa podacima o linijama i stanicama.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> UcitajLinijeStanice(string region)
        {
            var vm = new LinijeStaniceVM()
            {
                Linije = await linijeServis.UcitajSveLinije(),
                Stanice = await linijeServis.UcitajSveStanice(region)
            };

            return PartialView("LinijeStaniceTab", vm);
        }

        /// <summary>
        /// Dodaje novu stanicu.
        /// </summary>
        /// <param name="naziv">Naziv stanice.</param>
        /// <param name="region">Region stanice.</param>
        /// <returns>
        /// Ok ako je stanica uspešno dodata, inače BadRequest.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> DodajStanicu(string naziv, string region)
        {
            var ok = await linijeServis.DodajStanicu(naziv, region);
            if (!ok)
                return BadRequest("Neuspesno dodavanje!");

            return Ok("Uspesno dodata stanica.");
        }

        /// <summary>
        /// Dodaje novu liniju sa stanicama, redosledom i vremenima od polaska.
        /// </summary>
        /// <param name="naziv">Naziv linije.</param>
        /// <param name="cena_po_minutu">Cena putovanja po minutu.</param>
        /// <param name="stanicaIds">Identifikatori stanica na liniji.</param>
        /// <param name="redosled">Redosled stanica na liniji, u istom broju kao stanice.</param>
        /// <param name="vreme_od_polaska">Vremena dolaska do stanica od polaska, u istom broju kao stanice.</param>
        /// <returns>
        /// Ok ako je linija uspešno dodata, inače BadRequest.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> DodajLiniju(string naziv, int cena_po_minutu, List<int> stanicaIds, List<int> redosled, List<int> vreme_od_polaska)
        {
            if (!ModelState.IsValid)
                return BadRequest("Cena po minutu mora biti broj.");

            var ok = await linijeServis.DodajLiniju(naziv, cena_po_minutu, stanicaIds, redosled, vreme_od_polaska);
            if (!ok)
                return BadRequest("Neuspesno dodavanje!");

            return Ok("Uspesno dodata linija.");
        }

        /// <summary>
        /// Uklanja liniju iz sistema.
        /// </summary>
        /// <param name="id">Identifikator linije.</param>
        /// <returns>
        /// Ok ako je linija uspešno uklonjena, inače BadRequest.
        /// </returns>
        [HttpDelete]
        public async Task<IActionResult> UkloniLiniju(int id)
        {
            var ok = await linijeServis.UkloniLiniju(id);
            if (!ok)
                return BadRequest("Neuspesno uklanjanje!");

            return Ok("Uspesno uklonjena linija.");
        }

        /// <summary>
        /// Uklanja stanicu iz sistema.
        /// </summary>
        /// <param name="id">Identifikator stanice.</param>
        /// <returns>
        /// Ok ako je stanica uspešno uklonjena, inače BadRequest.
        /// </returns>
        [HttpDelete]
        public async Task<IActionResult> UkloniStanicu(int id)
        {
            var ok = await linijeServis.UkloniStanicu(id);
            if (!ok)
                return BadRequest("Neuspesno uklanjanje!");

            return Ok("Uspesno uklonjena stanica.");
        }

        /// <summary>
        /// Menja podatke linije.
        /// </summary>
        /// <param name="id">Identifikator linije.</param>
        /// <param name="naziv">Naziv linije.</param>
        /// <param name="cena_po_minutu">Cena putovanja po minutu.</param>
        /// <param name="stanicaIds">Identifikatori stanica na liniji.</param>
        /// <param name="redosled">Redosled stanica na liniji.</param>
        /// <param name="vreme_od_polaska">Vremena dolaska do stanica od polaska.</param>
        /// <returns>
        /// Ok ako je linija uspešno izmenjena, inače BadRequest.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> IzmeniLiniju(int id, string naziv, int cena_po_minutu, List<int> stanicaIds, List<int> redosled, List<int> vreme_od_polaska)
        {
            var ok = await linijeServis.IzmeniLiniju(id, naziv, cena_po_minutu, stanicaIds, redosled, vreme_od_polaska);
            if (!ok)
                return BadRequest("Neuspesna izmena!");

            return Ok("Uspesno izmenjena linija.");
        }

        /// <summary>
        /// Menja podatke stanice.
        /// </summary>
        /// <param name="id">Identifikator stanice.</param>
        /// <param name="naziv">Naziv stanice.</param>
        /// <param name="region">Region stanice.</param>
        /// <returns>
        /// Ok ako je stanica uspešno izmenjena, inače BadRequest.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> IzmeniStanicu(int id, string naziv, string region)
        {
            var ok = await linijeServis.IzmeniStanicu(id, naziv, region);
            if (!ok)
                return BadRequest("Neuspesna izmena!");

            return Ok("Uspesno izmenjena linija.");
        }

        /// <summary>
        /// Učitava rasporede za zadati datum.
        /// </summary>
        /// <param name="datum">Datum za koji se učitavaju rasporedi.</param>
        /// <returns>
        /// PartialView sa podacima o rasporedima.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> UcitajRasporede(DateTime? datum)
        {
            RasporediVM vm = new();
            vm.rasporedi = await rasporedServis.UcitajRasporede(datum);
            return PartialView("RasporediTab", vm);
        }

        /// <summary>
        /// Uklanja raspored iz sistema.
        /// </summary>
        /// <param name="id">Identifikator rasporeda.</param>
        /// <returns>
        /// Ok ako je raspored uspešno uklonjen, inače BadRequest.
        /// </returns>
        [HttpDelete]
        public async Task<IActionResult> UkloniRaspored(int id)
        {
            var ok = await rasporedServis.UkloniRaspored(id);
            if (!ok)
                return BadRequest("Neuspesno uklanjanje!");

            return Ok("Uspesno uklonjen raspored.");
        }

        /// <summary>
        /// Dodaje novi raspored vožnje.
        /// </summary>
        /// <param name="linija_id">Identifikator linije.</param>
        /// <param name="voz_id">Identifikator voza.</param>
        /// <param name="vreme_polaska">Vreme polaska voza.</param>
        /// <returns>
        /// Ok ako je raspored uspešno dodat, inače BadRequest.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> DodajRaspored(int linija_id, int voz_id, DateTime vreme_polaska)
        {
            var ok = await rasporedServis.DodajRaspored(linija_id, voz_id, vreme_polaska);
            if (!ok)
                return BadRequest("Neuspesno dodavanje.");

            return Ok("Uspesno dodat raspored");
        }

        /// <summary>
        /// Menja podatke rasporeda vožnje.
        /// </summary>
        /// <param name="id">Identifikator rasporeda.</param>
        /// <param name="linija_id">Identifikator linije.</param>
        /// <param name="voz_id">Identifikator voza.</param>
        /// <param name="vreme_polaska">Vreme polaska voza.</param>
        /// <returns>
        /// Ok ako je raspored uspešno izmenjen, inače BadRequest.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> IzmeniRaspored(int id, int linija_id, int voz_id, DateTime vreme_polaska)
        {
            var ok = await rasporedServis.IzmeniRaspored(id, linija_id, voz_id, vreme_polaska);
            if (!ok)
                return BadRequest("Neuspesna izmena.");

            return Ok("Uspesno izmenjen raspored.");
        }
    }
}
