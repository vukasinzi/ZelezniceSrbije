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

            var korisnik = await db.Korisnik.FirstOrDefaultAsync(p => p.Email == email);
            if (korisnik == null)
                return null;

            if (hasher.VerifyHashedPassword(null, korisnik.Lozinka, lozinka) == PasswordVerificationResult.Failed)
                return null;

            var admin = await db.Admin.FirstOrDefaultAsync(k => k.Id == korisnik.Id);
            if (admin != null)
                return admin;

            var kondukter = await db.Kondukter.FirstOrDefaultAsync(k => k.Id == korisnik.Id);
            if (kondukter != null)
                return kondukter;

            var putnik = await db.Putnik.FirstOrDefaultAsync(k => k.Id == korisnik.Id);
            if (putnik != null)
                return putnik;

            return korisnik;
        }

        public async Task<Korisnik> RegistrujAsync(Putnik p)
        {
            var postoji = await db.Korisnik.FirstOrDefaultAsync(x => x.Email == p.Email);
            if (postoji != null)
                return null;

            await db.Putnik.AddAsync(p);
            await db.SaveChangesAsync();
            return p;
        }

        public async Task<List<Administrator>> UcitajSveAdmine()
        {
            return await db.Admin.ToListAsync();
        }

        public async Task<List<Kondukter>> UcitajSveKonduktere()
        {
            return await db.Kondukter.ToListAsync();
        }

        public async Task IzbrisiDrugeUloge(int id)
        {
            await db.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Putnik WHERE Id = {id}");
            await db.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Kondukter WHERE Id = {id}");
            await db.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Administrator WHERE Id = {id}");
        }

        public async Task Promovisi(int id, string uloga, DateTime? datum, string? broj_legitimacije)
        {
            if (uloga == "Kondukter")
                await db.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO Kondukter (id, broj_legitimacije) VALUES ({id}, {broj_legitimacije})");
            else if (uloga == "Administrator")
                await db.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO Administrator (id, datum_zaposlenja) VALUES ({id}, {datum})");
        }

        public async Task<Korisnik> Pronadji(string email)
        {
            return await db.Korisnik.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task IzmeniAdministratora(Administrator admin, int id)
        {
            var a = await db.Admin.FirstOrDefaultAsync(x => x.Id == id);
            if (a == null)
                return;

            a.Ime = admin.Ime;
            a.Prezime = admin.Prezime;
            a.Email = admin.Email;
            a.Datum_zaposlenja = admin.Datum_zaposlenja;

            await db.SaveChangesAsync();
        }

        public async Task IzmeniKonduktera(Kondukter kondukter, int id)
        {
            var k = await db.Kondukter.FirstOrDefaultAsync(x => x.Id == id);
            if (k == null)
                return;

            k.Ime = kondukter.Ime;
            k.Prezime = kondukter.Prezime;
            k.Email = kondukter.Email;
            k.Broj_legitimacije = kondukter.Broj_legitimacije;

            await db.SaveChangesAsync();
        }

        public async Task UkloniAdministratora(int id)
        {
            await db.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Administrator WHERE Id = {id}");
        }

        public async Task UkloniKonduktera(int id)
        {
            await db.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Kondukter WHERE Id = {id}");
        }
    }
}
