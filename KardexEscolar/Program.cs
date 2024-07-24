using KardexEscolar.Datos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configuracion de la conexion a sql server para hacer uso de la inyeccion de dependencias
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionASql")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuracion de la cadena de conexion a sql server mediante ADO NET
builder.Services.AddSingleton(new Contexto(builder.Configuration.GetConnectionString("ConexionASql")));


//Manejo de Cookies para el inicio de sesion
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Acceso/Index";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        option.AccessDeniedPath = "/Home/Privacidad";
    });





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//Agregamos el autenticador
app.UseAuthentication();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Index}/{id?}");

app.Run();
