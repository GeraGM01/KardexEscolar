using KardexEscolar.Datos;
using KardexEscolar.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
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
            int cu = Convert.ToInt32(claveUnica);

            if (claveUnica == null)
            {
                return NotFound();
            }

            LogicaDB logicaDB = new LogicaDB(_contexto);
            var kardexCalificaciones = logicaDB.ObtenMateriaCalificacion(cu);

            var kardexProfesores = logicaDB.ObtenMateriaProfesor(cu);

            //Encapsular tanto calificaciones y las materias dadas de alta, esto lo hacemos con un modelo auxiliar

            var ProfesorMateriaAlumno = new ProfesorMateriaAlumno()
            {
                KardexCalificacion = kardexCalificaciones,
                KardexMateriaProfesor = kardexProfesores
            };

            return View(ProfesorMateriaAlumno);
        }

        public IActionResult PerfilAlumno()
        {
            var claveUnica = User.FindFirst(ClaimTypes.Name)?.Value;
            int cu = Convert.ToInt32(claveUnica);

            if (claveUnica == null)
            {
                return NotFound();
            }

            LogicaDB logicaDB = new LogicaDB(_contexto);
            var datosAlumno = logicaDB.ObtenDatosAlumno(cu);

            return View(datosAlumno);
        }

        [HttpPost]
        public IActionResult CambiarContrasenaAlumno(CambiarContrasena modeloCambiarContrasena)
        {
            if (ModelState.IsValid)
            {
                var claveUnica = User.FindFirst(ClaimTypes.Name)?.Value;
                int cu = Convert.ToInt32(claveUnica);
                string actual = modeloCambiarContrasena.ContrasenaActual.ToString();
                string nueva = modeloCambiarContrasena.ContrasenaNueva.ToString();
                string confiracionNueva = modeloCambiarContrasena.ConfirmarContrasenaNueva.ToString();

                LogicaDB logicaDB = new LogicaDB(_contexto);
                int filasAfectadas = logicaDB.CambiaContrasena(cu, modeloCambiarContrasena.ContrasenaActual.ToString().Trim(), modeloCambiarContrasena.ContrasenaNueva.ToString().Trim());

                if(filasAfectadas > 0)
                {
                    //Cambio exitoso
                    return Json(new { success = true, message = "La contraseña ha sido cambiada exitosamente." });
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Error al cambiar la contraseña.", errors });
        }


        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Acceso");
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

        public static string determinaCalificacionNulo(double calificacionModel)
        {
            if(calificacionModel == 0.0)
            {
                return " ";
            }
            return calificacionModel.ToString();
        }
    }
}
