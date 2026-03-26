using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            string novi_mejl = email.ToLowerInvariant().Trim();
           
           return await repo.LogInAsync(novi_mejl, lozinka);

        }

       

        public async Task<Korisnik> RegistrujAsync(Putnik p)
        {
            Putnik novi = new Putnik(p.Ime, p.Prezime, p.Email, p.Broj_telefona, p.Lozinka);
             novi.Email = p.Email.ToLowerInvariant().Trim();
            PasswordHasher<string> hasher = new();
            novi.Lozinka = hasher.HashPassword(null, p.Lozinka);
            if (p.Ime.Length > 20 || p.Prezime.Length > 20)
                return null;
            
            return await repo.RegistrujAsync(novi);

        }
    }
}
