using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public interface IKorisnikRepository
    {
        Task<Korisnik> LogInAsync(string email,string lozinka);
        Task<Korisnik> RegistrujAsync(Putnik p);
    }
}
