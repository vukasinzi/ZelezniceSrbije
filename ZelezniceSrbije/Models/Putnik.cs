namespace ZelezniceSrbije.Models
{
    public class Putnik : Korisnik
    {
        public Putnik(string ime, string prezime, string email, string lozinka) : base(ime, prezime, email, lozinka)
        {
        }

        public string Broj_telefona { get; set; }
    }
}
