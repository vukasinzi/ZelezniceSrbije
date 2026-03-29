namespace ZelezniceSrbije.Models
{
    public class Raspored
    {
        public int Id { get; set; }
        public DateTime DatumPolaska { get; set; }
        public Linija Linija { get; set; }
        public Voz Voz { get; set; }
        public Raspored()
        {

        }
        
    }
}
