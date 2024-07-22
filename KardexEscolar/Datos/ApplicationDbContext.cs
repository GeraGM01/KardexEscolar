using KardexEscolar.Models;
using Microsoft.EntityFrameworkCore;

namespace KardexEscolar.Datos
{
    public class ApplicationDbContext : DbContext
    {
        //Constructor para cargar la inyeccion de dependencias
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
