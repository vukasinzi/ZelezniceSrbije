using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers;
using Microsoft.AspNetCore.Mvc;
[Authorize(Roles = "Administrator,Putnik,Kondukter")]
public class KartaController : Controller
{
   private IKartaService servis;
   private IQrService qr_servis;

   public KartaController(IKartaService servis,IQrService qr_servis)
   {
      this.servis = servis;
      this.qr_servis = qr_servis;
   }
   [HttpGet]
   public IActionResult Index()
   {
      if(User.IsInRole("Administrator") || User.IsInRole("Kondukter") || User.IsInRole("Putnik"))
         return View();
      return RedirectToAction("Home", "Index");
   }
   [HttpPost]
   public async Task<IActionResult> Kupi(int raspored_id,int polaziste_id,int odrediste_id)
   {
      int putnik_id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
      if(putnik_id == 0)
         return RedirectToAction("Home", "Index");

      var Karta = await servis.Kupi(putnik_id,raspored_id,polaziste_id,odrediste_id);
      if (Karta == null)
         return BadRequest("Neuspesna kupovina");
      var Karta_DTO = await servis.VratiPodatke(Karta.Id,putnik_id);
      //sad ide qr
      if (Karta_DTO == null)
      {
         return BadRequest("Neuspesna kupovina");
      }
      var url = Url.Action("Ocitaj", "Kondukter", new { t = Karta.QrToken },Request.Scheme)!;
      var qr = qr_servis.GenerisiQrKod(url);
      ViewData["QrImageData"] = $"data:image/png;base64,{Convert.ToBase64String(qr)}";
      ViewData["QrPayload"] = url;
      
      return View("Print", Karta_DTO);

   }
}