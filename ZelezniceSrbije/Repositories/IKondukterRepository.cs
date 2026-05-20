using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories;

/// <summary>
/// Repozitorijum za rad sa kondukterskim funkcionalnostima.
/// Omogućava učitavanje rasporeda i očitavanje karata.
/// </summary>
public interface IKondukterRepository
{
    /// <summary>
    /// Vraća podatke o zadatom rasporedu.
    /// </summary>
    /// <param name="raspored_id">Identifikator rasporeda.</param>
    /// <returns>
    /// Podaci o rasporedu ako postoji, inače null.
    /// </returns>
    Task<RasporedDTO?> VratiRaspored(int raspored_id);

    /// <summary>
    /// Vraća rasporede dostupne za današnji dan.
    /// </summary>
    /// <returns>
    /// Lista današnjih rasporeda ako postoje, inače null.
    /// </returns>
    Task<List<RasporedDTO>?> VratiRasporedeZaDanas();

    /// <summary>
    /// Očitava kartu za zadati raspored pomoću QR tokena.
    /// </summary>
    /// <param name="token">QR token karte.</param>
    /// <param name="rasporedId">Identifikator rasporeda.</param>
    /// <returns>
    /// True ako je karta uspešno očitana, false ako nije.
    /// </returns>
    Task<bool> OcitajKartu(Guid token, int rasporedId);
}