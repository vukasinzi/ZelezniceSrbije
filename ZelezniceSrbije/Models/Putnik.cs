namespace ZelezniceSrbije.Models
{
    public class Putnik : Korisnik
    {

        
        public Putnik(string ime, string prezime, string email,string broj_telefona, string lozinka) : base(ime, prezime, email, lozinka)
        {
            Broj_telefona = broj_telefona;
        }

        public string Broj_telefona { get; set; }
    }
}
