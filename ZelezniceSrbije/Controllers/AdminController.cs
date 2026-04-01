using Microsoft.AspNetCore.Mvc;

namespace ZelezniceSrbije.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
