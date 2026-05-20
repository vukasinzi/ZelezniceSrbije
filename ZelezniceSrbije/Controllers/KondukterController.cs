using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZelezniceSrbije.Models.ViewModels;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers;

/// <summary>
/// Kontroler za rad konduktera.
/// Omogućava izbor vožnje, završetak vožnje i očitavanje karata.
/// </summary>
[Authorize(Roles = "Kondukter,Administrator")]
public class KondukterController : Controller
{
    /// <summary>
    /// Servis za rad sa kondukterskim funkcionalnostima.
    /// </summary>
    private IKondukterService servis;

    /// <summary>
    /// Kreira novi kontroler za konduktera.
    /// </summary>
    /// <param name="kondukterService">Servis za rad sa kondukterskim funkcionalnostima.</param>
    public KondukterController(IKondukterService kondukterService)
    {
        this.servis =  kondukterService;
    }

    /// <summary>
    /// Prikazuje početnu stranicu kondukterskog dela.
    /// </summary>
    /// <returns>
    /// Preusmerava na panel ako korisnik ima dozvoljenu ulogu, inače na početnu stranicu.
    /// </returns>
    [HttpGet]
    public IActionResult Index()
    {
        if (!User.IsInRole("Kondukter") && !User.IsInRole("Administrator"))
            return RedirectToAction("Index", "Home");
        return RedirectToAction("Panel","Kondukter");
    }

    /// <summary>
    /// Prikazuje kondukterski panel.
    /// </summary>
    /// <returns>
    /// View sa aktivnom vožnjom ili dostupnim rasporedima za danas.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Panel()
    {
        var cookie = Request.Cookies["aktivna_voznja"];
    
        if (cookie != null)
        {
            var raspored_id = int.Parse(cookie);
            var detalji = await servis.VratiRaspored(raspored_id);
            return View(new KondukterPanelVM { 
                aktivna_voznja_detalji = detalji
            });
        }
    
        var rasporedi = await servis.VratiRasporedeZaDanas();
        return View(new KondukterPanelVM { 
            dostupni_rasporedi = rasporedi
        });
    }

    /// <summary>
    /// Postavlja izabranu vožnju kao aktivnu.
    /// </summary>
    /// <param name="raspored_id">Identifikator rasporeda vožnje.</param>
    /// <returns>
    /// Preusmerava na kondukterski panel.
    /// </returns>
    [HttpPost]
    public IActionResult IzaberiVoznju(int raspored_id)
    {
        Response.Cookies.Append("aktivna_voznja",raspored_id.ToString(), new CookieOptions {HttpOnly = true});
        return RedirectToAction("Panel");
    }

    /// <summary>
    /// Završava trenutno aktivnu vožnju.
    /// </summary>
    /// <returns>
    /// Preusmerava na kondukterski panel.
    /// </returns>
    [HttpPost]
    public IActionResult ZavrsiVoznju()
    {
        Response.Cookies.Delete("aktivna_voznja");
        return RedirectToAction("Panel");
    }

    /// <summary>
    /// Očitava kartu pomoću QR tokena.
    /// </summary>
    /// <param name="token">QR token karte.</param>
    /// <returns>
    /// View za uspešno očitavanje ako je karta validna, inače View za nevalidnu kartu.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Ocitaj(Guid token)
    {
        var cookie = Request.Cookies["aktivna_voznja"];
        if (cookie == null)
            return RedirectToAction("Index");

        var raspored_id = int.Parse(cookie);
        var rezultat = await servis.OcitajKartu(token, raspored_id);

        if (!rezultat)
            return View("Nevalidna karta");

        return View("Uspesno");
    }
    
}