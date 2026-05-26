using System.Collections.Generic;
using System.Threading.Tasks;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    /// <summary>
    /// Repozitorijum za rad sa vozovima i tipovima vozova.
    /// Omogućava dodavanje, izmenu, proveru, učitavanje i uklanjanje vozova i tipova vozova.
    /// </summary>
    public interface IVozRepository
    {
        /// <summary>
        /// Dodaje novi tip voza.
        /// </summary>
        /// <param name="tipVoza">Tip voza koji se dodaje.</param>
        Task DodajTipVoza(TipVoza tipVoza);

        /// <summary>
        /// Dodaje novi voz.
        /// </summary>
        /// <param name="voz">Voz koji se dodaje.</param>
        Task DodajVoz(Voz voz);

        /// <summary>
        /// Menja podatke tipa voza.
        /// </summary>
        /// <param name="tipVoza">Novi podaci tipa voza.</param>
        Task IzmeniTipVoza(TipVoza tipVoza);

        /// <summary>
        /// Menja podatke voza.
        /// </summary>
        /// <param name="voz">Novi podaci voza.</param>
        Task IzmeniVoz(Voz voz);

        /// <summary>
        /// Proverava da li tip voza postoji.
        /// </summary>
        /// <param name="id">Identifikator tipa voza.</param>
        /// <returns>
        /// True ako tip voza postoji, false ako ne postoji.
        /// </returns>
        Task<bool> PostojiTipVoza(int id);

        /// <summary>
        /// Proverava da li voz postoji.
        /// </summary>
        /// <param name="id">Identifikator voza.</param>
        /// <returns>
        /// True ako voz postoji, false ako ne postoji.
        /// </returns>
        Task<bool> PostojiVoz(int id);

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
        Task UkloniTipVoza(int id);

        /// <summary>
        /// Uklanja voz iz sistema.
        /// </summary>
        /// <param name="id">Identifikator voza.</param>
        Task UkloniVoz(int id);
    }
}