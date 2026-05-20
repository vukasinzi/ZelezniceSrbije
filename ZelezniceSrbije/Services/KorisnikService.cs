using Microsoft.AspNetCore.Identity;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Services
{
    /// <inheritdoc/>
    public class KorisnikService : IKorisnikService
    {
        /// <summary>
        /// Repozitorijum za rad sa korisnicima.
        /// </summary>
        private readonly IKorisnikRepository repo;

        /// <summary>
        /// Kreira novi servis za korisnike.
        /// </summary>
        /// <param name="repo">Repozitorijum za rad sa korisnicima.</param>
        public KorisnikService(IKorisnikRepository repo)
        {
            this.repo = repo;
        }

        /// <inheritdoc/>
        public async Task<Korisnik> LogInAsync(string email, string lozinka)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(lozinka))
                return null;

            string noviMejl = email.ToLowerInvariant().Trim();
            return await repo.LogInAsync(noviMejl, lozinka);
        }

        /// <inheritdoc/>
        public async Task<Korisnik> RegistrujAsync(Putnik p)
        {
            if (!p.JeValidan())
                return null;

            p.Email = p.Email.ToLowerInvariant().Trim();

            PasswordHasher<string> hasher = new PasswordHasher<string>();
            p.Lozinka = hasher.HashPassword(null, p.Lozinka);

            return await repo.RegistrujAsync(p);
        }

        /// <inheritdoc/>
        public async Task<List<Administrator>> UcitajSveAdmine()
        {
            return await repo.UcitajSveAdmine();
        }

        /// <inheritdoc/>
        public async Task<List<Kondukter>> UcitajSveKonduktere()
        {
            return await repo.UcitajSveKonduktere();
        }

        /// <inheritdoc/>
        public async Task<bool> PromovisiUlogu(string email, string uloga, DateTime? datum, string? broj_legitimacije)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var pronadji = await repo.Pronadji(email.Trim().ToLowerInvariant());
            if (pronadji == null)
                return false;

            if (uloga != "Administrator" && uloga != "Kondukter")
                return false;

            if (uloga == "Kondukter" && string.IsNullOrWhiteSpace(broj_legitimacije))
                return false;

            if (uloga == "Administrator" && datum == null)
                return false;

            return await repo.PromovisiUloguTransakciono(pronadji.Id,uloga, datum, broj_legitimacije?.Trim());
        }

        /// <inheritdoc/>
        public async Task<bool> UkloniAdministratora(int id)
        {
            if (id <= 0)
                return false;
            return await repo.UkloniAdministratora(id);
        }

        /// <inheritdoc/>
        public async Task<bool> UkloniKonduktera(int id)
        {
            if (id <= 0)
                return false;
            return await repo.UkloniKonduktera(id);
        }

        /// <inheritdoc/>
        public async Task<bool> IzmeniAdministratora(int id, string ime, string prezime, string email, DateTime? datum)
        {
            Administrator admin = new(ime, prezime, email, "dummypolje", datum);
            if (!admin.JeValidan())
                return false;
            return await repo.IzmeniAdministratora(admin, id);
        }

        /// <inheritdoc/>
        public async Task<bool> IzmeniKonduktera(int id, string ime, string prezime, string email, string broj_legitimacije)
        {
            Kondukter kondukter = new(ime, prezime, email, "dummypolje", broj_legitimacije);
            if (!kondukter.JeValidan())
                return false;
            return await repo.IzmeniKonduktera(kondukter, id);
        }
    }
}