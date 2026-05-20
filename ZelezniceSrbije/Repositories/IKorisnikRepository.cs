using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    /// <summary>
    /// Repozitorijum za rad sa korisnicima.
    /// Omogućava prijavu, registraciju, promenu uloga i upravljanje administratorima i kondukterima.
    /// </summary>
    public interface IKorisnikRepository
    {
        /// <summary>
        /// Briše druge uloge korisnika.
        /// </summary>
        /// <param name="id">Identifikator korisnika.</param>
        Task IzbrisiDrugeUloge(int id);

        /// <summary>
        /// Menja podatke administratora.
        /// </summary>
        /// <param name="admin">Novi podaci administratora.</param>
        /// <param name="id">Identifikator administratora.</param>
        /// <returns>
        /// True ako je administrator uspešno izmenjen, false ako nije.
        /// </returns>
        Task<bool> IzmeniAdministratora(Administrator admin,int id);

        /// <summary>
        /// Menja podatke konduktera.
        /// </summary>
        /// <param name="kondukter">Novi podaci konduktera.</param>
        /// <param name="id">Identifikator konduktera.</param>
        /// <returns>
        /// True ako je kondukter uspešno izmenjen, false ako nije.
        /// </returns>
        Task<bool> IzmeniKonduktera(Kondukter kondukter, int id);

        /// <summary>
        /// Prijavljuje korisnika na osnovu emaila i lozinke.
        /// </summary>
        /// <param name="email">Email korisnika.</param>
        /// <param name="lozinka">Lozinka korisnika.</param>
        /// <returns>
        /// Korisnik ako su podaci ispravni.
        /// </returns>
        Task<Korisnik> LogInAsync(string email,string lozinka);

        /// <summary>
        /// Promoviše korisnika u zadatu ulogu.
        /// </summary>
        /// <param name="id">Identifikator korisnika.</param>
        /// <param name="uloga">Nova uloga korisnika.</param>
        /// <param name="datum">Datum zaposlenja korisnika.</param>
        /// <param name="broj_legitimacije">Broj legitimacije konduktera.</param>
        Task Promovisi(int id, string uloga, DateTime? datum, string? broj_legitimacije);

        /// <summary>
        /// Pronalazi korisnika po email adresi.
        /// </summary>
        /// <param name="email">Email korisnika.</param>
        /// <returns>
        /// Korisnik sa zadatom email adresom.
        /// </returns>
        Task<Korisnik> Pronadji(string email);

        /// <summary>
        /// Registruje novog putnika.
        /// </summary>
        /// <param name="p">Podaci putnika za registraciju.</param>
        /// <returns>
        /// Registrovani korisnik.
        /// </returns>
        Task<Korisnik> RegistrujAsync(Putnik p);

        /// <summary>
        /// Učitava sve administratore.
        /// </summary>
        /// <returns>
        /// Lista administratora.
        /// </returns>
        Task<List<Administrator>> UcitajSveAdmine();

        /// <summary>
        /// Učitava sve konduktere.
        /// </summary>
        /// <returns>
        /// Lista konduktera.
        /// </returns>
        Task<List<Kondukter>> UcitajSveKonduktere();

        /// <summary>
        /// Uklanja administratora iz sistema.
        /// </summary>
        /// <param name="id">Identifikator administratora.</param>
        /// <returns>
        /// True ako je administrator uspešno uklonjen, false ako nije.
        /// </returns>
        Task<bool> UkloniAdministratora(int id);

        /// <summary>
        /// Uklanja konduktera iz sistema.
        /// </summary>
        /// <param name="id">Identifikator konduktera.</param>
        /// <returns>
        /// True ako je kondukter uspešno uklonjen, false ako nije.
        /// </returns>
        Task<bool> UkloniKonduktera(int id);

        /// <summary>
        /// Promoviše korisnika u zadatu ulogu kroz transakciju.
        /// </summary>
        /// <param name="pronadjiId">Identifikator pronađenog korisnika.</param>
        /// <param name="uloga">Nova uloga korisnika.</param>
        /// <param name="datum">Datum zaposlenja korisnika.</param>
        /// <param name="trim">Broj legitimacije konduktera.</param>
        /// <returns>
        /// True ako je uloga uspešno promenjena, false ako nije.
        /// </returns>
        Task<bool> PromovisiUloguTransakciono(int pronadjiId, string uloga, DateTime? datum, string? trim);
    }
}