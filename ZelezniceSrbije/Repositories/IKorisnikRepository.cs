using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public interface IKorisnikRepository
    {
        Task IzbrisiDrugeUloge(int id);
        Task<Korisnik> LogInAsync(string email,string lozinka);
        Task Promovisi(int id, string uloga, DateTime? datum, string? broj_legitimacije);
        Task<Korisnik> Pronadji(string email);
        Task<Korisnik> RegistrujAsync(Putnik p);
        Task<List<Administrator>> UcitajSveAdmine();
        Task<List<Kondukter>> UcitajSveKonduktere();
    }
}
