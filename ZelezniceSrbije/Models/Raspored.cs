namespace ZelezniceSrbije.Models
{
    public class Raspored
    {
        public int Id { get; set; }
        public DateTime Vreme_polaska { get; set; }
        public int Linija_id { get; set; }
        public int Voz_id { get; set; }
        public Linija Linija { get; set; }
        public Voz Voz { get; set; }
        public Raspored()
        {

        }
        
    }
}
