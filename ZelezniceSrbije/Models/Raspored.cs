using ZelezniceSrbije.Models;

public class Raspored
{
    public int Id { get; set; }
    public DateTime Vreme_polaska { get; set; }
    public int Linija_id { get; set; }
    public int Voz_id { get; set; }

    public Linija? Linija { get; set; }
    public Voz? Voz { get; set; }

    public Raspored() { }

    public Raspored(DateTime vreme_polaska, int linija_id, int voz_id)
    {
        Vreme_polaska = vreme_polaska;
        Linija_id = linija_id;
        Voz_id = voz_id;
    }

    public bool JeValidan()
    {
        if (Vreme_polaska == default)
            return false;

        if (Linija_id <= 0 || Voz_id <= 0)
            return false;

        return true;
    }
}