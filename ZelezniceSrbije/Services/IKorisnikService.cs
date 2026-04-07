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
        Task<bool> IzmeniAdministratora(int id,string ime, string prezime, string email, DateTime? datum);
        Task<bool> IzmeniKonduktera(int id, string ime, string prezime, string email, string broj_legitimacije);
        Task<bool> UkloniAdministratora(int id);
        Task<bool> UkloniKonduktera(int id);
    }
}
