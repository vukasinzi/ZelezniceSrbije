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
        Task<bool> DodajTipVoza(string naziv, string opis);
        Task<List<Voz>> UcitajSveVozove();
        Task<List<TipVoza>> UcitajSveTipoveVoza();
        Task<bool> UkloniTipVoza(int id);
        Task<bool> UkloniVoz(int id);
        Task<bool> IzmeniVoz(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id);
        Task<bool> IzmeniTipVoza(int id, string naziv, string opis);
        Task<bool> DodajVoz(string naziv, string serijski_broj, int tip_voza_id, bool aktivan);
    }
}
