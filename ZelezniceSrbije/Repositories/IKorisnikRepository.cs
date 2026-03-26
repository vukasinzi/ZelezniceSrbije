using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public interface IKorisnikRepository
    {
        Task<Korisnik> LogInAsync(string email,string lozinka);
        Task<Korisnik> RegistrujAsync(string ime, string prezime, string novi_mejl, string nova_lozinka);
    }
}
