using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers;

public class HomeController : Controller
{
    private IRasporedService servis;
    public HomeController(IRasporedService servis)
    {
        this.servis = servis;
    }
    public async Task<IActionResult> Pretraga(string polaziste,string odrediste,DateTime datum)
    {
        if (datum.Date < DateTime.Today)
        {
            ModelState.AddModelError("datum", "Datum ne može biti u prošlosti.");
            return View("Index");
        }
        List<Raspored> rasporedi = await servis.PretraziAsync(polaziste,odrediste,datum);
        

        return View();
    }

    public async Task<IActionResult> Index()
    {
        var svm = new StaniceViewModel
        {
            Stanice = await servis.UcitajStaniceAsync()
        };
        return View(svm);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
