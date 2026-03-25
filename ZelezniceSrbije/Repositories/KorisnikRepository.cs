using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public class KorisnikRepository : IKorisnikRepository
    {

        private readonly VozAppContext db;
        public KorisnikRepository(VozAppContext db)
        {
            this.db = db;
        }
        public async Task<Korisnik> LogInAsync(string email, string lozinka)
        {
            
            return await db.Korisnici.FirstOrDefaultAsync(k => k.Email.ToLower() == cistMejl && k.Lozinka == lozinka);

        }
    }
}
