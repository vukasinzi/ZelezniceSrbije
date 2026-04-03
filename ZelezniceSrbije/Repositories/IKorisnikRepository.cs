using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public interface IKorisnikRepository
    {
        Task DodajTipVoza(TipVoza tv);
        Task DodajVoz(Voz v);
        Task IzbrisiDrugeUloge(int id);
        Task IzmeniAdministratora(Administrator admin,int id);
        Task IzmeniKonduktera(Kondukter kondukter, int id);
        Task IzmeniTipVoza(TipVoza tv);
        Task IzmeniVoz(Voz v);
        Task<Korisnik> LogInAsync(string email,string lozinka);
        Task Promovisi(int id, string uloga, DateTime? datum, string? broj_legitimacije);
        Task<Korisnik> Pronadji(string email);
        Task<Korisnik> RegistrujAsync(Putnik p);
        Task<List<Administrator>> UcitajSveAdmine();
        Task<List<Kondukter>> UcitajSveKonduktere();
        Task<List<TipVoza>> UcitajSveTipoveVoza();
        Task<List<Voz>> UcitajSveVozove();
        Task UkloniAdministratora(int id);
        Task UkloniKonduktera(int id);
        Task UkloniTipVoza(int id);
        Task UkloniVoz(int id);
    }
}
