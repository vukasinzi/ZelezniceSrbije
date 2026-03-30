namespace ZelezniceSrbije.Models
{
    public class StanicaLinija//StanicaLinija
    {
        public int Id { get; set; }
        public int Vreme_od_polaska { get; set; }
        public int Redosled { get; set; }
        public int Stanica_id { get; set; }
        public int Linija_id { get; set; }
        public Stanica Stanica { get; set; }
        public Linija Linija { get; set; }
    }
}
