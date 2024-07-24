using KardexEscolar.Datos;
using KardexEscolar.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KardexEscolar.Controllers
{
    public class AccesoController : Controller
    {
        // Para ADO.NET
        private readonly Contexto _contexto;

        public AccesoController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            //Verificamos si el usuario ya esta loggeado, si es asi lo mandamos directamente al Home
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Método para obtener la info del formulario de login y validar si el usuario puede ingresar al sistema o no
        [HttpPost]
        public async Task<IActionResult> Index(Usuario usuario)
        {
            //Validacion a nivel de backend con entity para saber si estan correctos los campos de acuerdo a las restricciones dadas
            if (ModelState.IsValid)
            {
                LogicaDB logicaDB = new LogicaDB(_contexto);
                var usuarioEncontrado = logicaDB.ExisteUsuarioEnBD(usuario);

                //Caso que no se haya enconrado el usuario, mandamos directamente a la misma vista
                if(usuarioEncontrado == null)
                {
                    return View();
                }

                //Si no es null si se encontro el usuario y hacemos la asignacion de sesion y cookies
                //Creacion del cookies de sesion para el usuario
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Clave_Unica.ToString()),
                    new Claim("Id_Usuario", usuario.Id_Usuario.ToString())
                };

                //Aqui hacemos la busqueda de los roles que va a tener nuestro usuario

                List<string> rolesDelUsuario = new List<string>();
                rolesDelUsuario = logicaDB.ObtenRol(usuarioEncontrado);


                //Recorremos todos los roles que tiene nuestro usuario
                foreach (string rol in rolesDelUsuario)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                //Asignada la sesion al usuario, lo reenviamos a la vista de usuario autenticado
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}