using System.ComponentModel.DataAnnotations;

namespace ZelezniceSrbije.Models;

/// <summary>
/// Predstavlja kartu za vožnju vozom.
/// Karta sadrži podatke o putniku, rasporedu, relaciji, vremenu putovanja i QR tokenu.
/// </summary>
public class Karta
{
    /// <summary>
    /// Jedinstveni identifikator karte.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Cena karte.
    /// </summary>
    public decimal Cena { get; set; }

    /// <summary>
    /// Označava da li je karta očitana.
    /// </summary>
    public bool Ocitana { get; set; }

    /// <summary>
    /// Datum kada je karta očitana.
    /// </summary>
    public DateTime? Datum_ocitavanja { get; set; }

    /// <summary>
    /// Identifikator putnika koji poseduje kartu.
    /// </summary>
    public int Putnik_id { get; set; }

    /// <summary>
    /// Identifikator rasporeda vožnje za koji je karta vezana.
    /// </summary>
    public int Raspored_id { get; set; }

    /// <summary>
    /// Polazna stanica.
    /// </summary>
    public string Polaziste { get; set; } = string.Empty;

    /// <summary>
    /// Odredišna stanica.
    /// </summary>
    public string Odrediste { get; set; } = string.Empty;

    /// <summary>
    /// Naziv linije.
    /// </summary>
    public string Linija { get; set; } = string.Empty;

    /// <summary>
    /// Tip voza.
    /// </summary>
    public string Tip_voza { get; set; } = string.Empty;

    /// <summary>
    /// Ime konduktera koji je očitao kartu.
    /// </summary>
    public string? Kondukter { get; set; }

    /// <summary>
    /// Trajanje putovanja u minutima.
    /// </summary>
    public int Trajanje_min { get; set; }

    /// <summary>
    /// Vreme polaska voza.
    /// </summary>
    public DateTime Vreme_polaska { get; set; }

    /// <summary>
    /// Vreme dolaska voza.
    /// </summary>
    public DateTime Vreme_dolaska { get; set; }

    /// <summary>
    /// QR token koji se koristi za proveru karte.
    /// </summary>
    public Guid Qr_token { get; set; }

    /// <summary>
    /// Kreira praznu kartu.
    /// </summary>
    public Karta() { }

    /// <summary>
    /// Kreira novu kartu sa prosleđenim podacima.
    /// </summary>
    /// <param name="cena">Cena karte.</param>
    /// <param name="putnikId">Identifikator putnika.</param>
    /// <param name="rasporedId">Identifikator rasporeda vožnje.</param>
    /// <param name="polaziste">Polazna stanica.</param>
    /// <param name="odrediste">Odredišna stanica.</param>
    /// <param name="linija">Naziv linije.</param>
    /// <param name="tipVoza">Tip voza.</param>
    /// <param name="polazak">Vreme polaska.</param>
    /// <param name="dolazak">Vreme dolaska.</param>
    /// <param name="trajanje">Trajanje putovanja u minutima.</param>
    /// <param name="kondukter">Ime konduktera.</param>
    /// <param name="token">QR token karte.</param>
    public Karta(decimal cena, int putnikId, int rasporedId, string polaziste, string odrediste, string linija, string tipVoza, DateTime polazak, DateTime dolazak, int trajanje, string? kondukter, Guid token)
    {
        Cena = cena;
        Putnik_id = putnikId;
        Raspored_id = rasporedId;
        Polaziste = polaziste;
        Odrediste = odrediste;
        Linija = linija;
        Tip_voza = tipVoza;
        Vreme_polaska = polazak;
        Vreme_dolaska = dolazak;
        Trajanje_min = trajanje;
        Kondukter = kondukter;
        Qr_token = token;
        Ocitana = false;
    }
}