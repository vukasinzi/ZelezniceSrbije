using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Models.ViewModels;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers;

[Authorize(Roles = "Kondukter,Administrator")]
public class KondukterController : Controller
{
    private IKondukterService servis;
    public KondukterController(IKondukterService kondukterService)
    {
        this.servis =  kondukterService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        if (!User.IsInRole("Kondukter") && !User.IsInRole("Administrator"))
            return RedirectToAction("Index", "Home");
        return RedirectToAction("Panel","Kondukter");
    }

    [HttpGet]
    public async Task<IActionResult> Panel()
    {
        var cookie = Request.Cookies["aktivna_voznja"];
    
        if (cookie != null)
        {
            var aktivna = JsonSerializer.Deserialize<AktivnaVoznja>(cookie);
            var detalji = await servis.VratiRaspored(aktivna.raspored_id);
            return View(new KondukterPanelVM { 
                aktivna_voznja = aktivna,
                aktivna_voznja_detalji = detalji
            });
        }
    
        var rasporedi = await servis.VratiRasporedeZaDanas();
        return View(new KondukterPanelVM { 
            dostupni_rasporedi = rasporedi
        });
    }
    [HttpPost]
    public IActionResult IzaberiVoznju(int raspored_id)
    {
        Response.Cookies.Append("aktivna_voznja",JsonSerializer.Serialize(new{raspored_id}), new CookieOptions {HttpOnly = true});
        return RedirectToAction("Panel");
    }
    [HttpPost]
    public IActionResult ZavrsiVoznju()
    {
        Response.Cookies.Delete("aktivna_voznja");
        return RedirectToAction("Panel");
    }

    [HttpGet]
    public async Task<IActionResult> Ocitaj(Guid token)
    {
        var cookie = Request.Cookies["aktivna_voznja"];
        if (cookie == null)
            return RedirectToAction("Index");

        var aktivna = JsonSerializer.Deserialize<AktivnaVoznja>(cookie);
        var rezultat = await servis.OcitajKartu(token, aktivna.raspored_id);

        if (!rezultat)
            return View("Nevalidna karta");

        return View("Uspesno");
    }
    
}