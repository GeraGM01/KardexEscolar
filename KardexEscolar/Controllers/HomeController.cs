using KardexEscolar.Datos;
using KardexEscolar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace KardexEscolar.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        // Para ADO.NET
        private readonly Contexto _contexto;

        public HomeController(Contexto contexto)
        {
            _contexto = contexto;
        }


        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Alumno")]
        public IActionResult Kardex()
        {
            //Obtenemos la Clave unica del alumno mediante la claims de sesion
            var claveUnica = User.FindFirst(ClaimTypes.Name)?.Value;

            if(claveUnica == null)
            {
                return NotFound();
            }

            LogicaDB logicaDB = new LogicaDB(_contexto);
            var kardex = 


            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Privacidad()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
