public class Karta
{
    public Karta() { }

    public Karta(decimal cena, int putnik_id, int raspored_id, int polaziste_id, int odrediste_id, Guid qrToken)
    {
        Cena = cena;
        Putnik_id = putnik_id;
        Raspored_id = raspored_id;
        Polaziste_id = polaziste_id;
        Odrediste_id = odrediste_id;
        QrToken = qrToken;
    }

    public int Id { get; set; }
    public decimal Cena { get; set; }
    public bool Ocitana { get; set; }
    public DateTime? Datum_ocitavanja { get; set; }
    public int Putnik_id { get; set; }
    public int? Kondukter_id { get; set; }
    public int Raspored_id { get; set; }
    public int Polaziste_id { get; set; }
    public int Odrediste_id { get; set; }
    public Guid QrToken { get; set; }
}