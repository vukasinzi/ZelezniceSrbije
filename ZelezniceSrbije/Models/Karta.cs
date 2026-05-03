public class Karta
{
    public Karta() { }

    public Karta(decimal cena, int putnik_id, int raspored_id, string polaziste, string odrediste, string linija, string tipVoza, DateTime vremePolaska, DateTime vremeDolaska, int trajanjeMin, string kondukter, Guid qrToken)
    {
        Cena = cena;
        Putnik_id = putnik_id;
        Raspored_id = raspored_id;
        Polaziste = polaziste;
        Odrediste = odrediste;
        Linija = linija;
        TipVoza = tipVoza;
        VremePolaska = vremePolaska;
        VremeDolaska = vremeDolaska;
        TrajanjeMin = trajanjeMin;
        Kondukter = kondukter;
        Qr_token = qrToken; 
    }

    public int Id { get; set; }
    public decimal Cena { get; set; }
    public bool Ocitana { get; set; }
    public DateTime? Datum_ocitavanja { get; set; }
    
    public int Putnik_id { get; set; }
    public int Raspored_id { get; set; }
    
    public string Kondukter { get; set; }
    public string Polaziste { get; set; }
    public string Odrediste { get; set; }
    public string Linija { get; set; }
    public string TipVoza { get; set; }
    
    public DateTime VremePolaska { get; set; }
    public DateTime VremeDolaska { get; set; }
    public int TrajanjeMin { get; set; }
    public Guid Qr_token { get; set; } 
}