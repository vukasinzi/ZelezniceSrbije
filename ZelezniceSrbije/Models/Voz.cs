using System.ComponentModel.DataAnnotations.Schema;

namespace ZelezniceSrbije.Models
{
    public class Voz
    {
        public Voz(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id)
        {
            Id = id;
            Naziv = naziv;
            Serijski_broj = serijski_broj;
            Aktivan = aktivan;
            Tip_voza_id = tip_voza_id;
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Serijski_broj { get; set; }
        public bool Aktivan { get; set; }
        public int Tip_voza_id { get; set; }
        [ForeignKey(nameof(Tip_voza_id))]

        public TipVoza TipVoza { get; set; }
        
    }
}
