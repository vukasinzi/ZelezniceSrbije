using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services
{
    /// <summary>
    /// Servis za rad sa linijama i stanicama.
    /// Omogućava dodavanje, izmenu, učitavanje i uklanjanje linija i stanica.
    /// </summary>
    public interface ILinijeServis
    {
        /// <summary>
        /// Dodaje novu liniju sa zadatim stanicama, redosledom i vremenima od polaska.
        /// </summary>
        /// <param name="naziv">Naziv linije.</param>
        /// <param name="cena_po_minutu">Cena putovanja po minutu.</param>
        /// <param name="stanicaIds">Identifikatori stanica na liniji.</param>
        /// <param name="redosled">Redosled stanica na liniji, u istom broju kao stanice.</param>
        /// <param name="vreme_od_polaska">Vremena dolaska do stanica od polaska, u istom broju kao stanice.</param>
        /// <returns>
        /// True ako je linija uspešno dodata, false ako podaci nisu validni ili linija već postoji.
        /// </returns>
        Task<bool> DodajLiniju(string naziv, int cena_po_minutu, List<int> stanicaIds, List<int> redosled, List<int> vreme_od_polaska);

        /// <summary>
        /// Dodaje novu stanicu.
        /// </summary>
        /// <param name="naziv">Naziv stanice.</param>
        /// <param name="region">Region stanice.</param>
        /// <returns>
        /// True ako je stanica uspešno dodata, false ako nije.
        /// </returns>
        Task<bool> DodajStanicu(string naziv, string region);

        /// <summary>
        /// Menja podatke linije i njenih stanica.
        /// </summary>
        /// <param name="id">Identifikator linije.</param>
        /// <param name="naziv">Naziv linije.</param>
        /// <param name="cena_po_minutu">Cena putovanja po minutu.</param>
        /// <param name="stanicaIds">Identifikatori stanica na liniji.</param>
        /// <param name="redosled">Redosled stanica na liniji.</param>
        /// <param name="vreme_od_polaska">Vremena dolaska do stanica od polaska.</param>
        /// <returns>
        /// True ako je linija uspešno izmenjena, false ako nije.
        /// </returns>
        Task<bool> IzmeniLiniju(int id, string naziv, int cena_po_minutu, List<int> stanicaIds, List<int> redosled, List<int> vreme_od_polaska);

        /// <summary>
        /// Menja podatke stanice.
        /// </summary>
        /// <param name="id">Identifikator stanice.</param>
        /// <param name="naziv">Naziv stanice.</param>
        /// <param name="region">Region stanice.</param>
        /// <returns>
        /// True ako je stanica uspešno izmenjena, false ako nije.
        /// </returns>
        Task<bool> IzmeniStanicu(int id, string naziv, string region);

        /// <summary>
        /// Učitava sve linije.
        /// </summary>
        /// <returns>
        /// Lista svih linija.
        /// </returns>
        Task<List<LinijaDTO>> UcitajSveLinije();

        /// <summary>
        /// Učitava sve stanice za zadati region.
        /// </summary>
        /// <param name="region">Region za koji se učitavaju stanice.</param>
        /// <returns>
        /// Lista stanica iz zadatog regiona.
        /// </returns>
        Task<List<Stanica>> UcitajSveStanice(string region);

        /// <summary>
        /// Uklanja liniju iz sistema.
        /// </summary>
        /// <param name="id">Identifikator linije.</param>
        /// <returns>
        /// True ako je linija uspešno uklonjena, false ako nije.
        /// </returns>
        Task<bool> UkloniLiniju(int id);

        /// <summary>
        /// Uklanja stanicu iz sistema.
        /// </summary>
        /// <param name="id">Identifikator stanice.</param>
        /// <returns>
        /// True ako je stanica uspešno uklonjena, false ako nije.
        /// </returns>
        Task<bool> UkloniStanicu(int id);

    }
}
