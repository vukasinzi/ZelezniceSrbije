using System.ComponentModel.DataAnnotations.Schema;

namespace ZelezniceSrbije.Models
{
    public class Voz
    {
        public Voz(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id)
        {
            Id = id;
            Naziv = naziv?.Trim();
            Serijski_broj = serijski_broj?.Trim();
            Aktivan = aktivan;
            Tip_voza_id = tip_voza_id;
        }
        public Voz(string naziv, string serijski_broj, bool aktivan, int tip_voza_id)
        {
            Naziv = naziv?.Trim();
            Serijski_broj = serijski_broj?.Trim();
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

        public bool JeValidan()
        {
            if (string.IsNullOrWhiteSpace(Naziv) || Naziv.Length > 100)
                return false;

            if (string.IsNullOrWhiteSpace(Serijski_broj) || Serijski_broj.Length > 50)
                return false;

            if (Tip_voza_id <= 0)
                return false;

            return true;
        }
    }
}