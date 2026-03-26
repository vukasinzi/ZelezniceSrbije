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
            PasswordHasher<string> hasher = new PasswordHasher<string>();
            string nova_lozinka = hasher.HashPassword(null, lozinka);
           return await repo.LogInAsync(novi_mejl, nova_lozinka);

        }

       

        public async Task<Korisnik> RegistrujAsync(string ime, string prezime, string email, string lozinka)
        {
            string novi_mejl = email.ToLowerInvariant().Trim();
            PasswordHasher<string> hasher = new();
            string nova_lozinka = hasher.HashPassword(null, lozinka);
            if (ime.Length > 20 || prezime.Length > 20)
                return null;
            
            return await repo.RegistrujAsync(ime,prezime,novi_mejl,nova_lozinka);

        }
    }
}
