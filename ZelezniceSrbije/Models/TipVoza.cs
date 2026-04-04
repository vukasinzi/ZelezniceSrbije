namespace ZelezniceSrbije.Models
{
    public class TipVoza
    {
        public TipVoza(int id, string naziv, string opis)
        {
            Id = id;
            Naziv = naziv?.Trim();
            Opis = opis?.Trim();
        }
        public TipVoza(string naziv, string opis)
        {
            Naziv = naziv?.Trim();
            Opis = opis?.Trim();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }

        public bool JeValidan()
        {
            if (string.IsNullOrWhiteSpace(Naziv) || Naziv.Length > 100)
                return false;

            if (Opis != null && (string.IsNullOrWhiteSpace(Opis) || Opis.Length > 500))
                return false;

            return true;
        }
    }
}