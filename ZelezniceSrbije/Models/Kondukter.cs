namespace ZelezniceSrbije.Models
{
    public class Kondukter : Korisnik
    {
        public Kondukter(string ime, string prezime, string email, string lozinka) : base(ime, prezime, email, lozinka)
        {
        }

        public string Broj_legitimacije { get; set; }
    }
}
