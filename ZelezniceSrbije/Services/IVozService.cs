using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services
{
    /// <summary>
    /// Servis za rad sa vozovima i tipovima vozova.
    /// Omogućava dodavanje, izmenu, učitavanje i uklanjanje vozova i tipova vozova.
    /// </summary>
    public interface IVozService
    {
        /// <summary>
        /// Dodaje novi tip voza.
        /// </summary>
        /// <param name="naziv">Naziv tipa voza.</param>
        /// <param name="opis">Opis tipa voza.</param>
        /// <returns>
        /// True ako je tip voza uspešno dodat, false ako nije.
        /// </returns>
        Task<bool> DodajTipVoza(string naziv, string opis);

        /// <summary>
        /// Dodaje novi voz.
        /// </summary>
        /// <param name="naziv">Naziv voza.</param>
        /// <param name="serijski_broj">Serijski broj voza.</param>
        /// <param name="tip_voza_id">Identifikator tipa voza.</param>
        /// <param name="aktivan">Označava da li je voz aktivan.</param>
        /// <returns>
        /// True ako je voz uspešno dodat, false ako nije.
        /// </returns>
        Task<bool> DodajVoz(string naziv, string serijski_broj, int tip_voza_id, bool aktivan);

        /// <summary>
        /// Menja podatke tipa voza.
        /// </summary>
        /// <param name="id">Identifikator tipa voza.</param>
        /// <param name="naziv">Naziv tipa voza.</param>
        /// <param name="opis">Opis tipa voza.</param>
        /// <returns>
        /// True ako je tip voza uspešno izmenjen, false ako nije.
        /// </returns>
        Task<bool> IzmeniTipVoza(int id, string naziv, string opis);

        /// <summary>
        /// Menja podatke voza.
        /// </summary>
        /// <param name="id">Identifikator voza.</param>
        /// <param name="naziv">Naziv voza.</param>
        /// <param name="serijski_broj">Serijski broj voza.</param>
        /// <param name="aktivan">Označava da li je voz aktivan.</param>
        /// <param name="tip_voza_id">Identifikator tipa voza.</param>
        /// <returns>
        /// True ako je voz uspešno izmenjen, false ako nije.
        /// </returns>
        Task<bool> IzmeniVoz(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id);

        /// <summary>
        /// Učitava sve tipove vozova.
        /// </summary>
        /// <returns>
        /// Lista tipova vozova.
        /// </returns>
        Task<List<TipVoza>> UcitajSveTipoveVoza();

        /// <summary>
        /// Učitava sve vozove.
        /// </summary>
        /// <returns>
        /// Lista vozova.
        /// </returns>
        Task<List<Voz>> UcitajSveVozove();

        /// <summary>
        /// Uklanja tip voza iz sistema.
        /// </summary>
        /// <param name="id">Identifikator tipa voza.</param>
        /// <returns>
        /// True ako je tip voza uspešno uklonjen, false ako nije.
        /// </returns>
        Task<bool> UkloniTipVoza(int id);

        /// <summary>
        /// Uklanja voz iz sistema.
        /// </summary>
        /// <param name="id">Identifikator voza.</param>
        /// <returns>
        /// True ako je voz uspešno uklonjen, false ako nije.
        /// </returns>
        Task<bool> UkloniVoz(int id);
    }
}