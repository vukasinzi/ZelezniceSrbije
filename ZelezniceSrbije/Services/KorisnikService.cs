using Microsoft.AspNetCore.Identity;
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
    }
}