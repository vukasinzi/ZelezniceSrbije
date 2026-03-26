using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services
{
    public interface IKorisnikService
    {
        Task<Korisnik> LogInAsync(string email, string lozinka);
        Task<Korisnik> RegistrujAsync(string ime,string prezime,string email, string lozinka);
    }
}
