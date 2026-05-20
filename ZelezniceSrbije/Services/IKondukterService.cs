using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services;

/// <summary>
/// Servis za rad sa kondukterskim funkcionalnostima.
/// Omogućava učitavanje rasporeda i očitavanje karata.
/// </summary>
public interface IKondukterService
{
    /// <summary>
    /// Vraća podatke o aktivnom rasporedu.
    /// </summary>
    /// <param name="aktivnaRasporedId">Identifikator aktivnog rasporeda.</param>
    /// <returns>
    /// Podaci o rasporedu ako postoji, inače null.
    /// </returns>
    Task<RasporedDTO?> VratiRaspored(int aktivnaRasporedId);

    /// <summary>
    /// Vraća rasporede dostupne za današnji dan.
    /// </summary>
    /// <returns>
    /// Lista današnjih rasporeda ako postoje, inače null.
    /// </returns>
    Task<List<RasporedDTO>?> VratiRasporedeZaDanas();

    /// <summary>
    /// Očitava kartu za aktivni raspored pomoću QR tokena.
    /// </summary>
    /// <param name="token">QR token karte.</param>
    /// <param name="aktivnaRasporedId">Identifikator aktivnog rasporeda.</param>
    /// <returns>
    /// True ako je karta uspešno očitana, false ako nije.
    /// </returns>
    Task<bool> OcitajKartu(Guid token, int aktivnaRasporedId);
}