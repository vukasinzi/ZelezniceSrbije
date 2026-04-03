using Microsoft.AspNetCore.Identity;
using NuGet.Packaging.Signing;
using System.Threading.Tasks;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZelezniceSrbije.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IKorisnikRepository repo;

        public KorisnikService(IKorisnikRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Korisnik> LogInAsync(string email, string lozinka)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(lozinka))
                return null;

            string novi_mejl = email.ToLowerInvariant().Trim();

            return await repo.LogInAsync(novi_mejl, lozinka);
        }

    

        public async Task<Korisnik> RegistrujAsync(Putnik p)
        {
            if (!p.JeValidan())
                return null;

            p.Email = p.Email.ToLowerInvariant().Trim();

            PasswordHasher<string> hasher = new PasswordHasher<string>();
            p.Lozinka = hasher.HashPassword(null, p.Lozinka);

            return await repo.RegistrujAsync(p);
        }
        public async Task<List<Administrator>> UcitajSveAdmine()
        {
            return await repo.UcitajSveAdmine();
            
        }

        public async Task<List<Kondukter>> UcitajSveKonduktere()
        {
            return await repo.UcitajSveKonduktere();
        }

      
        public async Task<bool> PromovisiUlogu(string email, string uloga, DateTime? datum, string? broj_legitimacije)
        {
            var pronadji = await repo.Pronadji(email);
            if (pronadji == null)
                return false;
            if (uloga == "Kondukter" && string.IsNullOrEmpty(broj_legitimacije))
                return false;
            if (uloga == "Administrator" && datum == null)
                return false;
            await repo.IzbrisiDrugeUloge(pronadji.Id);
            await repo.Promovisi(pronadji.Id, uloga,datum,broj_legitimacije);
            return true;
             
        }

        public async Task<bool> IzmeniAdministratora(int id, string ime, string prezime, string email, DateTime? datum)
        {
            if (datum == null || datum.Value > DateTime.Now || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(ime) || string.IsNullOrEmpty(prezime))
                return false;
            Administrator admin = new(ime, prezime, email,"dummypolje",datum.Value);
            await repo.IzmeniAdministratora(admin,id);

            return true;

        }

        public async Task<bool> IzmeniKonduktera(int id, string ime, string prezime, string email, string broj_legitimacije)
        {
            if (string.IsNullOrEmpty(broj_legitimacije) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(ime) || string.IsNullOrEmpty(prezime))
                return false;
            Kondukter kondukter = new(ime, prezime, email, "dummypolje", broj_legitimacije);
            await repo.IzmeniKonduktera(kondukter, id);

            return true;
        }

        public async Task<bool> UkloniAdministratora(int id)
        {
            if (id == null || id < 0)
                return false;
            await repo.UkloniAdministratora(id);

            return true;
        }

        public async Task<bool> UkloniKonduktera(int id)
        {
            if (id == null || id < 0)
                return false;
            await repo.UkloniKonduktera(id);

            return true;
        }

        public async Task<bool> DodajTipVoza(string naziv, string opis)
        {
           
            TipVoza tv = new(naziv, opis);
            if (!tv.JeValidan())
                return false;
            await repo.DodajTipVoza(tv);
            return true;
        }

        public async Task<List<Voz>> UcitajSveVozove()
        {
            return await repo.UcitajSveVozove();
        }

        public async Task<List<TipVoza>> UcitajSveTipoveVoza()
        {
            return await repo.UcitajSveTipoveVoza();
        }

        public async Task<bool> UkloniTipVoza(int id)
        {
            if (id == null || id < 0)
                return false;
            await repo.UkloniTipVoza(id);

            return true;
        }

        public  async Task<bool> UkloniVoz(int id)
        {
            if (id == null || id < 0)
                return false;
            await repo.UkloniVoz(id);

            return true;
        }

        public async Task<bool> IzmeniVoz(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id)
        {
            Voz v = new(id, naziv, serijski_broj, aktivan, tip_voza_id);
            if (!v.JeValidan())
                return false;
            await repo.IzmeniVoz(v);

            return true;
        }

        public async Task<bool> IzmeniTipVoza(int id, string naziv, string opis)
        {
            TipVoza tv = new(id, naziv, opis);
            if (!tv.JeValidan())
                return false;
            await repo.IzmeniTipVoza(tv);
            return true;
        }

        public async Task<bool> DodajVoz(string naziv, string serijski_broj, int tip_voza_id, bool aktivan)
        {
            Voz v = new(naziv, serijski_broj, aktivan, tip_voza_id);
            if (!v.JeValidan())
                return false;
            await repo.DodajVoz(v);
            return true;
        }
    }
}