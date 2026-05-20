using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services
{
    /// <summary>
    /// Servis za rad sa korisnicima.
    /// Omogućava prijavu, registraciju, promenu uloga i upravljanje administratorima i kondukterima.
    /// </summary>
    public interface IKorisnikService
    {
        /// <summary>
        /// Prijavljuje korisnika na osnovu emaila i lozinke.
        /// </summary>
        /// <param name="email">Email korisnika.</param>
        /// <param name="lozinka">Lozinka korisnika.</param>
        /// <returns>
        /// Korisnik ako su podaci ispravni.
        /// </returns>
        Task<Korisnik> LogInAsync(string email, string lozinka);

        /// <summary>
        /// Promoviše korisnika u zadatu ulogu.
        /// </summary>
        /// <param name="email">Email korisnika.</param>
        /// <param name="uloga">Nova uloga korisnika.</param>
        /// <param name="datum">Datum zaposlenja korisnika.</param>
        /// <param name="broj_legitimacije">Broj legitimacije konduktera.</param>
        /// <returns>
        /// True ako je uloga uspešno promenjena, false ako nije.
        /// </returns>
        Task<bool> PromovisiUlogu(string email, string uloga, DateTime? datum, string? broj_legitimacije);

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
        /// Menja podatke administratora.
        /// </summary>
        /// <param name="id">Identifikator administratora.</param>
        /// <param name="ime">Ime administratora.</param>
        /// <param name="prezime">Prezime administratora.</param>
        /// <param name="email">Email administratora.</param>
        /// <param name="datum">Datum zaposlenja administratora.</param>
        /// <returns>
        /// True ako je administrator uspešno izmenjen, false ako nije.
        /// </returns>
        Task<bool> IzmeniAdministratora(int id,string ime, string prezime, string email, DateTime? datum);

        /// <summary>
        /// Menja podatke konduktera.
        /// </summary>
        /// <param name="id">Identifikator konduktera.</param>
        /// <param name="ime">Ime konduktera.</param>
        /// <param name="prezime">Prezime konduktera.</param>
        /// <param name="email">Email konduktera.</param>
        /// <param name="broj_legitimacije">Broj legitimacije konduktera.</param>
        /// <returns>
        /// True ako je kondukter uspešno izmenjen, false ako nije.
        /// </returns>
        Task<bool> IzmeniKonduktera(int id, string ime, string prezime, string email, string broj_legitimacije);

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
    }
}