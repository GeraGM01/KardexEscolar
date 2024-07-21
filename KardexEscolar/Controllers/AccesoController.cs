using Microsoft.AspNetCore.Mvc;

namespace KardexEscolar.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
