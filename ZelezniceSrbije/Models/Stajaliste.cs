namespace ZelezniceSrbije.Models
{
    public class Stajaliste//StanicaLinija
    {
        public int Id { get; set; }
        public int VremeOdPolaska { get; set; }
        public int Redosled { get; set; }
        public Stanica Stanica { get; set; }
        public Linija Linija { get; set; }
    }
}
