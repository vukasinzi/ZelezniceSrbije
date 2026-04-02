using Microsoft.AspNetCore.Identity;
using NuGet.Packaging.Signing;
using System.Threading.Tasks;
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

        //netestiran
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
            if (uloga == "Administrator" && datum  == null)
                return false;
            await repo.IzbrisiDrugeUloge(pronadji.Id);
            await repo.Promovisi(pronadji.Id, uloga,datum,broj_legitimacije);
            return true;
             
        }
    }
}