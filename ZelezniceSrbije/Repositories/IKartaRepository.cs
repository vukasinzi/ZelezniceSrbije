using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories;

/// <summary>
/// Repozitorijum za rad sa kartama.
/// Omogućava proveru, kupovinu i učitavanje karata.
/// </summary>
public interface IKartaRepository
{
    /// <summary>
    /// Proverava da li karta već postoji za zadate podatke.
    /// </summary>
    /// <param name="putnik_id">Identifikator putnika.</param>
    /// <param name="raspored_id">Identifikator rasporeda.</param>
    /// <param name="polaziste_id">Identifikator polazne stanice.</param>
    /// <param name="odrediste_id">Identifikator odredišne stanice.</param>
    /// <returns>
    /// True ako karta postoji, false ako ne postoji.
    /// </returns>
    Task<bool> ProveriKartu(int putnik_id, int raspored_id, int polaziste_id, int odrediste_id);

    /// <summary>
    /// Kupuje novu kartu za putnika.
    /// </summary>
    /// <param name="putnik_id">Identifikator putnika.</param>
    /// <param name="raspored_id">Identifikator rasporeda.</param>
    /// <param name="polaziste_id">Identifikator polazne stanice.</param>
    /// <param name="odrediste_id">Identifikator odredišne stanice.</param>
    /// <returns>
    /// Kupljena karta.
    /// </returns>
    Task<Karta> KupiKartu(int putnik_id, int raspored_id, int polaziste_id, int odrediste_id);

    /// <summary>
    /// Vraća podatke o jednoj karti za zadatog putnika.
    /// </summary>
    /// <param name="karta_id">Identifikator karte.</param>
    /// <param name="putnik_id">Identifikator putnika.</param>
    /// <returns>
    /// Podaci o karti.
    /// </returns>
    Task<KartaDTO> VratiKartu(int karta_id, int putnik_id);

    /// <summary>
    /// Vraća sve karte zadatog putnika.
    /// </summary>
    /// <param name="putnik_id">Identifikator putnika.</param>
    /// <returns>
    /// Lista podataka o kartama putnika.
    /// </returns>
    Task<List<KartaDTO>> VratiKarte(int putnik_id);
}