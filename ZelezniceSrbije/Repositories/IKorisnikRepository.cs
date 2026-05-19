using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public interface IKorisnikRepository
    {
        Task IzbrisiDrugeUloge(int id);
        Task<bool> IzmeniAdministratora(Administrator admin,int id);
        Task<bool> IzmeniKonduktera(Kondukter kondukter, int id);
        Task<Korisnik> LogInAsync(string email,string lozinka);
        Task Promovisi(int id, string uloga, DateTime? datum, string? broj_legitimacije);
        Task<Korisnik> Pronadji(string email);
        Task<Korisnik> RegistrujAsync(Putnik p);
        Task<List<Administrator>> UcitajSveAdmine();
        Task<List<Kondukter>> UcitajSveKonduktere();
        Task<bool> UkloniAdministratora(int id);
        Task<bool> UkloniKonduktera(int id);
        Task<bool> PromovisiUloguTransakciono(int pronadjiId, string uloga, DateTime? datum, string? trim);
    }
}
