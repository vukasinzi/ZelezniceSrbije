namespace ZelezniceSrbije.Services;

/// <summary>
/// Servis za generisanje QR kodova.
/// </summary>
public interface IQrService
{
    /// <summary>
    /// Generiše QR kod za zadati sadržaj.
    /// </summary>
    /// <param name="payload">Sadržaj koji se upisuje u QR kod.</param>
    /// <returns>
    /// QR kod predstavljen kao niz bajtova.
    /// </returns>
    byte[] GenerisiQrKod(string payload);
}