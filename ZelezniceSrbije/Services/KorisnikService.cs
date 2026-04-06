using Microsoft.AspNetCore.Identity;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

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

            string noviMejl = email.ToLowerInvariant().Trim();
            return await repo.LogInAsync(noviMejl, lozinka);
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

            if (uloga == "Kondukter" && (broj_legitimacije == null ||  string.IsNullOrWhiteSpace(broj_legitimacije.Trim())))
                return false;

            if (uloga == "Administrator" && datum == null)
                return false;

            await repo.IzbrisiDrugeUloge(pronadji.Id);
            await repo.Promovisi(pronadji.Id, uloga, datum, broj_legitimacije);
            return true;
        }

        public async Task<bool> IzmeniAdministratora(int id, string ime, string prezime, string email, DateTime? datum)
        {
         
            Administrator admin = new(ime, prezime, email, "dummypolje", datum);
            if (!admin.JeValidan())
                return false;
            await repo.IzmeniAdministratora(admin, id);
            return true;
        }

        public async Task<bool> IzmeniKonduktera(int id, string ime, string prezime, string email, string broj_legitimacije)

        { 
            Kondukter kondukter = new(ime, prezime, email, "dummypolje", broj_legitimacije);
            if (!kondukter.JeValidan())
                return false;
            await repo.IzmeniKonduktera(kondukter, id);
            return true;
        }

        public async Task<bool> UkloniAdministratora(int id)
        {
            if (id <= 0)
                return false;

            await repo.UkloniAdministratora(id);
            return true;
        }

        public async Task<bool> UkloniKonduktera(int id)
        {
            if (id <= 0)
                return false;

            await repo.UkloniKonduktera(id);
            return true;
        }
    }
}
