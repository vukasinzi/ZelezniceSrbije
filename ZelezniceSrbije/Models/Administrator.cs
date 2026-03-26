namespace ZelezniceSrbije.Models
{
    public class Administrator : Korisnik
    {
        public Administrator(string ime, string prezime, string email, string lozinka) : base(ime, prezime, email, lozinka)
        {
        }

        public DateTime Datum_zaposlenja { get; set; }

    }
}
