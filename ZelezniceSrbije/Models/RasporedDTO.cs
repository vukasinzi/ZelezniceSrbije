namespace ZelezniceSrbije.Models
{
    public class RasporedDTO
    {
        public int Id { get; set; }   
        public string Linija { get; set; }
        public string TipVoza { get; set; }
        public DateTime PolazakSaPol { get; set; }
        public DateTime DolazakNaOdr { get; set; }
    }
}
