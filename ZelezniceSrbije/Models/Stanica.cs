namespace ZelezniceSrbije.Models
{
    public class Stanica
    {
        public Stanica(int id, string naziv, string region)
        {
            Id = id;
            Naziv = naziv?.Trim(); 
            Region = region?.Trim(); 
        }
        public Stanica(string naziv, string region)
        { 
            Naziv = naziv?.Trim();
            Region = region?.Trim();
        }
        public bool JeValidan()
        {
            if (string.IsNullOrWhiteSpace(Naziv.Trim()) || string.IsNullOrWhiteSpace(Region.Trim()))
                return false;

            return true;
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Region { get; set; }

    }
}
