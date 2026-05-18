namespace ZelezniceSrbije.Models.ViewModels;

public class KarteVM
{
    public KarteVM(KartaDTO karta, string qrImageData)
    {
        Karta = karta;
        QrImageData = qrImageData;
    }

    public KartaDTO Karta { get; set; }
    public string QrImageData { get; set; }

}