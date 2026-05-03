using System.ComponentModel.DataAnnotations;

namespace ZelezniceSrbije.Models;

public class Karta
{
    [Key]
    public int Id { get; set; }
    public decimal Cena { get; set; }
    public bool Ocitana { get; set; }
    public DateTime? Datum_ocitavanja { get; set; }
    public int Putnik_id { get; set; }
    public int Raspored_id { get; set; }
    public string Polaziste { get; set; } = string.Empty;
    public string Odrediste { get; set; } = string.Empty;
    public string Linija { get; set; } = string.Empty;
    public string Tip_voza { get; set; } = string.Empty;
    public string? Kondukter { get; set; }
    public int Trajanje_min { get; set; }
    public DateTime Vreme_polaska { get; set; }
    public DateTime Vreme_dolaska { get; set; }
    public Guid Qr_token { get; set; }

    public Karta() { }

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