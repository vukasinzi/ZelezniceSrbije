using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    /// <summary>
    /// Repozitorijum za rad sa rasporedima vožnje.
    /// Omogućava dodavanje, izmenu, pretragu, učitavanje i uklanjanje rasporeda.
    /// </summary>
    public interface IRasporedRepository
    {
        /// <summary>
        /// Dodaje novi raspored vožnje.
        /// </summary>
        /// <param name="r">Raspored koji se dodaje.</param>
        /// <returns>
        /// True ako je raspored uspešno dodat, false ako nije.
        /// </returns>
        Task<bool> DodajRaspored(Raspored r);

        /// <summary>
        /// Menja podatke rasporeda vožnje.
        /// </summary>
        /// <param name="r">Novi podaci rasporeda.</param>
        /// <returns>
        /// True ako je raspored uspešno izmenjen, false ako nije.
        /// </returns>
        Task<bool> IzmeniRaspored(Raspored r);

        /// <summary>
        /// Pretražuje rasporede prema polazištu, odredištu i datumu.
        /// </summary>
        /// <param name="polaziste">Polazna stanica.</param>
        /// <param name="odrediste">Odredišna stanica.</param>
        /// <param name="datum">Datum putovanja.</param>
        /// <returns>
        /// Lista rasporeda koji odgovaraju pretrazi.
        /// </returns>
        Task<List<RasporedDTO>> PretraziAsync(string polaziste, string odrediste, DateTime datum);

        /// <summary>
        /// Proverava da li raspored postoji.
        /// </summary>
        /// <param name="id">Identifikator rasporeda.</param>
        /// <returns>
        /// Raspored ako postoji.
        /// </returns>
        Task<Raspored> ProveriRaspored(int id);

        /// <summary>
        /// Učitava rasporede za zadati datum.
        /// </summary>
        /// <param name="datum">Datum za koji se učitavaju rasporedi.</param>
        /// <returns>
        /// Lista rasporeda.
        /// </returns>
        Task<List<Raspored>> UcitajRasporede(DateTime? datum);

        /// <summary>
        /// Učitava sve stanice.
        /// </summary>
        /// <returns>
        /// Lista stanica.
        /// </returns>
        Task<List<Stanica>> UcitajStaniceAsync();

        /// <summary>
        /// Uklanja raspored iz sistema.
        /// </summary>
        /// <param name="id">Identifikator rasporeda.</param>
        Task UkloniRaspored(int id);
    }
}