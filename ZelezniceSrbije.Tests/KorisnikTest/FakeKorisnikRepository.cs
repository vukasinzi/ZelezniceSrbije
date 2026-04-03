using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Tests.KorisnikTest
{
    public class FakeKorisnikRepository : IKorisnikRepository
    {
        public List<Korisnik> korisnici { get; set; } = new();
        public List<Administrator> admini { get; set; } = new();
        public List<Kondukter> kondukteri { get; set; } = new();

        PasswordHasher<string> hasher = new();

        public async Task<Korisnik> LogInAsync(string email, string lozinka)
        {
          
            var postoji = korisnici.FirstOrDefault(k => k.Email == email);
            if (postoji == null)
                return null;
            if (hasher.VerifyHashedPassword(null, postoji.Lozinka, lozinka) == PasswordVerificationResult.Failed)
                return null;
            return await Task.FromResult(postoji);

        }

        public async Task<Korisnik> RegistrujAsync(Putnik p)
        {

            if (korisnici.Any(k => k.Email == p.Email))
                return null;
            korisnici.Add(p);
            return await Task.FromResult<Korisnik>(p);
        }

        public Task IzbrisiDrugeUloge(int id)
        {
            korisnici.RemoveAll(k => k.Id == id);
            admini.RemoveAll(a => a.Id == id);
            return Task.CompletedTask;
        }

        public Task Promovisi(int id, string uloga, DateTime? datum, string? broj_legitimacije)
        {
            var postojeci = korisnici.FirstOrDefault(x => x.Id == id);
            if (postojeci == null) return Task.CompletedTask;

            if (uloga == "Kondukter")
            {
                var k = new Kondukter(postojeci.Ime, postojeci.Prezime, postojeci.Email, postojeci.Lozinka, broj_legitimacije);
                kondukteri.Add(k);
            }
            else if (uloga == "Administrator")
            {
                if (datum == null) return Task.CompletedTask;
                var a = new Administrator(postojeci.Ime, postojeci.Prezime, postojeci.Email, postojeci.Lozinka, datum.Value);
                admini.Add(a);
            }

            return Task.CompletedTask;
        }

        public Task<Korisnik> Pronadji(string email)
        {
            var k = korisnici.FirstOrDefault(x => x.Email == email);
            return Task.FromResult(k);
        }


        public Task<List<Administrator>> UcitajSveAdmine()
        {
            return Task.FromResult(admini);
        }

        public Task<List<Kondukter>> UcitajSveKonduktere()
        {
            return Task.FromResult(kondukteri);

        }
    }
}
