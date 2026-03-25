using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IKorisnikRepository repo;
        public KorisnikService(KorisnikRepository repo)
        {
            this.repo = repo;
        }
        public async Task<Korisnik> LogInAsync(string email, string lozinka)
        {
            string novi_mejl = email.ToLowerInvariant().Trim();
            return await repo.LogInAsync(novi_mejl, lozinka);
        }
    }
}
