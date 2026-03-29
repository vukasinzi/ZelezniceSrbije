namespace ZelezniceSrbije.Models
{
    public class Voz
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Serijski_broj { get; set; }
        public bool Aktivan { get; set; }
        public TipVoza TipVoza { get; set; }
    }
}
