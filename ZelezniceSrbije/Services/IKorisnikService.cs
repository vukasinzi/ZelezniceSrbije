using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services
{
    public interface IKorisnikService
    {
        Task<Korisnik> LogInAsync(string email, string lozinka);
        Task<bool> PromovisiUlogu(string email, string uloga, DateTime? datum, string? broj_legitimacije);
        Task<Korisnik> RegistrujAsync(Putnik p);
        Task<List<Administrator>> UcitajSveAdmine();
        Task<List<Kondukter>> UcitajSveKonduktere();
    }
}
