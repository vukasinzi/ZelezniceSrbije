namespace ZelezniceSrbije.Models
{
    public class TipVoza
    {
        public TipVoza(int id,string naziv, string opis)
        {
            Id = id;
            Naziv = naziv;
            Opis = opis;
        }
        public TipVoza(string naziv,string opis)
        {
            Naziv = naziv;
            Opis = opis;
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public bool JeValidan()
        {
            if (string.IsNullOrWhiteSpace(Naziv) || Naziv.Trim().Length > 100)
                return false;

            if (Opis != null && Opis.Length > 500)
                return false;

            return true;
        }

    }
}
