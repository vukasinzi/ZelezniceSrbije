using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    /// <summary>
    /// Repozitorijum za rad sa linijama i stanicama.
    /// Omogućava dodavanje, uklanjanje, izmenu i učitavanje linija i stanica.
    /// </summary>
    public interface ILinijeRepository
    {
        /// <summary>
        /// Dodaje novu liniju zajedno sa njenim stajalištima.
        /// </summary>
        /// <param name="l">Linija koja se dodaje.</param>
        /// <param name="stajalista">Lista stajališta koja pripada liniji.</param>
        Task DodajLinijuSaStajalistima(Linija l, List<StanicaLinija> stajalista);

        /// <summary>
        /// Dodaje novu stanicu.
        /// </summary>
        /// <param name="s">Stanica koja se dodaje.</param>
        Task DodajStanicu(Stanica s);

        /// <summary>
        /// Uklanja liniju iz sistema.
        /// </summary>
        /// <param name="id">Identifikator linije.</param>
        Task UkloniLiniju(int id);

        /// <summary>
        /// Uklanja stanicu iz sistema.
        /// </summary>
        /// <param name="id">Identifikator stanice.</param>
        Task UkloniStanicu(int id);

        /// <summary>
        /// Proverava da li linija postoji po nazivu.
        /// </summary>
        /// <param name="naziv">Naziv linije.</param>
        /// <returns>
        /// Podaci o liniji ako postoji.
        /// </returns>
        Task<object> ProveriLiniju(string naziv);

        /// <summary>
        /// Proverava da li stanica postoji po nazivu.
        /// </summary>
        /// <param name="naziv">Naziv stanice.</param>
        /// <returns>
        /// Podaci o stanici ako postoji.
        /// </returns>
        Task<object> ProveriStanicu(string naziv);

        /// <summary>
        /// Proverava da li linija postoji po identifikatoru.
        /// </summary>
        /// <param name="id">Identifikator linije.</param>
        /// <returns>
        /// Podaci o liniji ako postoji.
        /// </returns>
        Task<object> ProveriLiniju(int id);

        /// <summary>
        /// Proverava da li stanica postoji po identifikatoru.
        /// </summary>
        /// <param name="id">Identifikator stanice.</param>
        /// <returns>
        /// Podaci o stanici ako postoji.
        /// </returns>
        Task<object> ProveriStanicu(int id);

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
        Task<List<Stanica>>UcitajSveStanice(string region);

        /// <summary>
        /// Menja podatke linije i njenih stajališta.
        /// </summary>
        /// <param name="l">Novi podaci linije.</param>
        /// <param name="stajalista">Nova lista stajališta linije.</param>
        /// <returns>
        /// True ako je linija uspešno izmenjena, false ako nije.
        /// </returns>
        Task<bool> IzmeniLiniju(Linija l, List<StanicaLinija> stajalista);

        /// <summary>
        /// Menja podatke stanice.
        /// </summary>
        /// <param name="s">Novi podaci stanice.</param>
        /// <returns>
        /// True ako je stanica uspešno izmenjena, false ako nije.
        /// </returns>
        Task<bool> IzmeniStanicu(Stanica s);
    }
}
