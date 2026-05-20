using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services
{
    /// <summary>
    /// Servis za rad sa rasporedima vožnje.
    /// Omogućava dodavanje, izmenu, pretragu, učitavanje i uklanjanje rasporeda.
    /// </summary>
    public interface IRasporedService
    {
        /// <summary>
        /// Dodaje novi raspored vožnje.
        /// </summary>
        /// <param name="linija_id">Identifikator linije.</param>
        /// <param name="voz_id">Identifikator voza.</param>
        /// <param name="vreme_polaska">Vreme polaska voza.</param>
        /// <returns>
        /// True ako je raspored uspešno dodat, false ako nije.
        /// </returns>
        Task<bool> DodajRaspored(int linija_id, int voz_id, DateTime vreme_polaska);

        /// <summary>
        /// Menja podatke rasporeda vožnje.
        /// </summary>
        /// <param name="id">Identifikator rasporeda.</param>
        /// <param name="linija_id">Identifikator linije.</param>
        /// <param name="voz_id">Identifikator voza.</param>
        /// <param name="vreme_polaska">Vreme polaska voza.</param>
        /// <returns>
        /// True ako je raspored uspešno izmenjen, false ako nije.
        /// </returns>
        Task<bool> IzmeniRaspored(int id,int linija_id, int voz_id, DateTime vreme_polaska);

        /// <summary>
        /// Pretražuje rasporede prema polazištu, odredištu i datumu.
        /// </summary>
        /// <param name="polaziste">Polazna stanica.</param>
        /// <param name="odrediste">Odredišna stanica.</param>
        /// <param name="datum">Datum putovanja.</param>
        /// <returns>
        /// Lista rasporeda koji odgovaraju pretrazi.
        /// </returns>
        Task<List<RasporedDTO>> PretraziAsync(string polaziste,string odrediste,DateTime datum);

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
        /// <returns>
        /// True ako je raspored uspešno uklonjen, false ako nije.
        /// </returns>
        Task<bool> UkloniRaspored(int id);
    }
}