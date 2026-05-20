using ZelezniceSrbije.Models;

/// <summary>
/// Predstavlja raspored polaska voza.
/// Raspored sadrži vreme polaska, liniju i voz.
/// </summary>
public class Raspored
{
    /// <summary>
    /// Jedinstveni identifikator rasporeda.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Vreme polaska voza.
    /// </summary>
    public DateTime Vreme_polaska { get; set; }

    /// <summary>
    /// Identifikator linije.
    /// </summary>
    public int Linija_id { get; set; }

    /// <summary>
    /// Identifikator voza.
    /// </summary>
    public int Voz_id { get; set; }

    /// <summary>
    /// Linija koja pripada rasporedu.
    /// </summary>
    public Linija? Linija { get; set; }

    /// <summary>
    /// Voz koji pripada rasporedu.
    /// </summary>
    public Voz? Voz { get; set; }

    /// <summary>
    /// Kreira prazan raspored.
    /// </summary>
    public Raspored() { }

    /// <summary>
    /// Kreira novi raspored.
    /// </summary>
    /// <param name="vreme_polaska">Vreme polaska voza.</param>
    /// <param name="linija_id">Identifikator linije.</param>
    /// <param name="voz_id">Identifikator voza.</param>
    public Raspored(DateTime vreme_polaska, int linija_id, int voz_id)
    {
        Vreme_polaska = vreme_polaska;
        Linija_id = linija_id;
        Voz_id = voz_id;
    }

    /// <summary>
    /// Kreira novi raspored sa zadatim identifikatorom.
    /// </summary>
    /// <param name="id">Identifikator rasporeda.</param>
    /// <param name="vreme_polaska">Vreme polaska voza.</param>
    /// <param name="linija_id">Identifikator linije.</param>
    /// <param name="voz_id">Identifikator voza.</param>
    public Raspored(int id, DateTime vreme_polaska, int linija_id, int voz_id)
    {
        Id = id;
        Vreme_polaska = vreme_polaska;
        Linija_id = linija_id;
        Voz_id = voz_id;
    }

    /// <summary>
    /// Proverava da li je raspored validan.
    /// </summary>
    /// <returns>
    /// True ako je raspored validan, false ako nije.
    /// </returns>
    public bool JeValidan()
    {
        if (Vreme_polaska == default)
            return false;

        if (Linija_id <= 0 || Voz_id <= 0)
            return false;

        return true;
    }
}