using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Tests.AuthTest
{
    internal class FakeKorisnikRepository : IKorisnikRepository
    {
        List<Korisnik> korisnici { get; set; } = new();
      

        public async Task<Korisnik> LogInAsync(string email, string lozinka)
        {
            var postoji = korisnici.FirstOrDefault(k => k.Email == email);
            if (postoji == null)
                return null;
            PasswordHasher<string> hasher = new();
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
    }
}
