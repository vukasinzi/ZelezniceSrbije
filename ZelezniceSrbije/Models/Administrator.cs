namespace ZelezniceSrbije.Models
{
    public class Administrator : Korisnik
    {
        public DateTime Datum_zaposlenja { get; set; }

        public Administrator(string ime, string prezime, string email, string lozinka, DateTime datum_zaposlenja)
            : base(ime, prezime, email, lozinka)
        {
            Datum_zaposlenja = datum_zaposlenja;
        }

        public override bool JeValidan()
        {
            if (!base.JeValidan())
                return false;

            if (Datum_zaposlenja.Date > DateTime.Today)
                return false;

            return true;
        }
    }
}