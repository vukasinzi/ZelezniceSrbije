namespace ZelezniceSrbije.Services;

public interface IQrService
{
    byte[] GenerisiQrKod(string payload);
}