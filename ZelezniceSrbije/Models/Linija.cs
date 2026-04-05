namespace ZelezniceSrbije.Models
{
    public class Linija
    {
        public Linija(string naziv, int cena_po_minutu)
        {
            Naziv = naziv.Trim();
            Cena_po_minutu = cena_po_minutu;
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public int Cena_po_minutu { get; set; }

        internal bool JeValidan()
        {
            if (string.IsNullOrWhiteSpace(Naziv) || Naziv.Length > 30)
                return false;
            if (Cena_po_minutu <= 0)
                return false;
            return true;
               
        }
    }
}
