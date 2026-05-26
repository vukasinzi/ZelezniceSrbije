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
    /// Dozvoljena vrednost: 0 za novu kartu ili pozitivan broj za postojeću kartu.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Cena karte.
    /// Dozvoljena vrednost: mora biti veća od 0.
    /// </summary>
    public decimal Cena { get; set; }

    /// <summary>
    /// Označava da li je karta očitana.
    /// Dozvoljena vrednost: true ako je karta očitana, false ako nije.
    /// </summary>
    public bool Ocitana { get; set; }

    /// <summary>
    /// Datum kada je karta očitana.
    /// Dozvoljena vrednost: null ako karta nije očitana ili datum očitavanja ako jeste.
    /// </summary>
    public DateTime? Datum_ocitavanja { get; set; }

    /// <summary>
    /// Identifikator putnika koji poseduje kartu.
    /// Dozvoljena vrednost: mora biti veći od 0.
    /// </summary>
    public int Putnik_id { get; set; }

    /// <summary>
    /// Identifikator rasporeda vožnje za koji je karta vezana.
    /// Dozvoljena vrednost: mora biti veći od 0.
    /// </summary>
    public int Raspored_id { get; set; }

    /// <summary>
    /// Polazna stanica.
    /// Dozvoljena vrednost: ne sme biti prazna.
    /// </summary>
    public string Polaziste { get; set; } = string.Empty;

    /// <summary>
    /// Odredišna stanica.
    /// Dozvoljena vrednost: ne sme biti prazna.
    /// </summary>
    public string Odrediste { get; set; } = string.Empty;

    /// <summary>
    /// Naziv linije.
    /// Dozvoljena vrednost: ne sme biti prazan.
    /// </summary>
    public string Linija { get; set; } = string.Empty;

    /// <summary>
    /// Tip voza.
    /// Dozvoljena vrednost: ne sme biti prazan.
    /// </summary>
    public string Tip_voza { get; set; } = string.Empty;

    /// <summary>
    /// Ime konduktera koji je očitao kartu.
    /// Dozvoljena vrednost: null ako karta nije očitana ili ime konduktera ako jeste.
    /// </summary>
    public string? Kondukter { get; set; }

    /// <summary>
    /// Trajanje putovanja u minutima.
    /// Dozvoljena vrednost: mora biti veće od 0.
    /// </summary>
    public int Trajanje_min { get; set; }

    /// <summary>
    /// Vreme polaska voza.
    /// Dozvoljena vrednost: mora biti pre vremena dolaska.
    /// </summary>
    public DateTime Vreme_polaska { get; set; }

    /// <summary>
    /// Vreme dolaska voza.
    /// Dozvoljena vrednost: mora biti posle vremena polaska.
    /// </summary>
    public DateTime Vreme_dolaska { get; set; }

    /// <summary>
    /// QR token koji se koristi za proveru karte.
    /// Dozvoljena vrednost: ne sme biti prazan Guid.
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
        Polaziste = polaziste?.Trim() ?? string.Empty;
        Odrediste = odrediste?.Trim() ?? string.Empty;
        Linija = linija?.Trim() ?? string.Empty;
        Tip_voza = tipVoza?.Trim() ?? string.Empty;
        Vreme_polaska = polazak;
        Vreme_dolaska = dolazak;
        Trajanje_min = trajanje;
        Kondukter = kondukter?.Trim();
        Qr_token = token;
        Ocitana = false;
    }

    /// <summary>
    /// Proverava da li je karta validna.
    /// </summary>
    /// <returns>
    /// True ako je karta validna, false ako nije.
    /// </returns>
    public bool JeValidan()
    {
        if (Cena <= 0 || Putnik_id <= 0 || Raspored_id <= 0)
            return false;

        if (string.IsNullOrWhiteSpace(Polaziste) || string.IsNullOrWhiteSpace(Odrediste))
            return false;

        if (string.IsNullOrWhiteSpace(Linija) || string.IsNullOrWhiteSpace(Tip_voza))
            return false;

        if (Trajanje_min <= 0)
            return false;

        if (Vreme_dolaska <= Vreme_polaska)
            return false;

        if (Qr_token == Guid.Empty)
            return false;

        return true;
    }
}