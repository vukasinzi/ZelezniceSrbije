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
            var korisnik =  await db.Korisnik.FirstOrDefaultAsync(p => p.Email == email && p.Lozinka == lozinka);
            if (korisnik == null) return korisnik;

            var putnik =  await db.Putnik.FirstOrDefaultAsync(k => k.Id == korisnik.Id);
            if (putnik != null) return putnik;
            var admin = await db.Admin.FirstOrDefaultAsync(k => k.Id == korisnik.Id);
            if (admin != null) return admin;
            var kondukter = await db.Kondukter.FirstOrDefaultAsync(k => k.Id == korisnik.Id);
            if (kondukter != null) return kondukter;

            return null;
        }

        public async Task<Korisnik> RegistrujAsync(string ime, string prezime, string email, string lozinka)
        {
            var postoji = await db.Korisnik.FirstOrDefaultAsync(p => p.Email == email);
            if (postoji != null)
            {
                return null;
            }
            Korisnik k = new Korisnik(ime, prezime, email, lozinka);
            await db.Korisnik.AddAsync(k);
            await db.SaveChangesAsync();
            return k;
        }
    }
}
