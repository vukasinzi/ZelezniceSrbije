namespace ZelezniceSrbije.Models
{
    public class StanicaLinija//StanicaLinija
    {
        public StanicaLinija(int vreme_od_polaska, int redosled, int stanica_id, int linija_id)
        {
            Vreme_od_polaska = vreme_od_polaska;
            Redosled = redosled;
            Stanica_id = stanica_id;
            Linija_id = linija_id;
        }

        public int Id { get; set; }
        public int Vreme_od_polaska { get; set; }
        public int Redosled { get; set; }
        public int Stanica_id { get; set; }
        public int Linija_id { get; set; }
        public Stanica Stanica { get; set; }
        public Linija Linija { get; set; }
    }
}
