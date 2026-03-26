using Microsoft.AspNetCore.Identity;
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
            PasswordHasher<string> hasher = new PasswordHasher<string>();
           
            var korisnik =  await db.Korisnik.FirstOrDefaultAsync(p => p.Email == email);
            if (korisnik == null) return korisnik;

            if (hasher.VerifyHashedPassword(null, korisnik.Lozinka, lozinka) == PasswordVerificationResult.Failed)
                return null;


            var putnik =  await db.Putnik.FirstOrDefaultAsync(k => k.Id == korisnik.Id);
            if (putnik != null) return putnik;
            var admin = await db.Admin.FirstOrDefaultAsync(k => k.Id == korisnik.Id);
            if (admin != null) return admin;
            var kondukter = await db.Kondukter.FirstOrDefaultAsync(k => k.Id == korisnik.Id);
            if (kondukter != null) return kondukter;

            return korisnik;
        }

        public async Task<Korisnik> RegistrujAsync(Putnik p)
        {
            var postoji = await db.Korisnik.FirstOrDefaultAsync(x => x.Email == p.Email);
            if (postoji != null)
            {
                return null;
            }
            Korisnik k = new Korisnik(p.Ime, p.Prezime, p.Email, p.Lozinka);
 
       
            await db.Putnik.AddAsync(p);
            await db.SaveChangesAsync();
            return k;
        }
    }
}
