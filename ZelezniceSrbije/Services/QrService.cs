using QRCoder;

namespace ZelezniceSrbije.Services;

public class QrService : IQrService
{
    public byte[] GenerisiQrKod(string payload)
    {
        if (string.IsNullOrWhiteSpace(payload))
            throw new ArgumentException("QR payload je prazan.");

        using var generator = new QRCodeGenerator();//genereator
        using var data = generator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);//generise qr kod
        var png = new PngByteQRCode(data);//pretvara u png 
        return png.GetGraphic(20);//vraca png
    }
}