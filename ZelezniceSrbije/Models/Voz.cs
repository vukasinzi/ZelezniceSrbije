using System.ComponentModel.DataAnnotations.Schema;

namespace ZelezniceSrbije.Models
{
    public class Voz
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Serijski_broj { get; set; }
        public bool Aktivan { get; set; }
        public int Tip_voza_id { get; set; }
        [ForeignKey(nameof(Tip_voza_id))]

        public TipVoza TipVoza { get; set; }
    }
}
