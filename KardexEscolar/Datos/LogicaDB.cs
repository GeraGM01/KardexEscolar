using KardexEscolar.Models;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace KardexEscolar.Datos
{
    public class LogicaDB
    {
        //En esta clase manejaremos la logica para el acceso a datos de la base de datos

        // Para ADO.NET
        private readonly Contexto _contexto;

        public LogicaDB(Contexto contexto)
        {
            _contexto = contexto;
        }



        public Usuario ExisteUsuarioEnBD(Usuario usuario)
        {
            Usuario usuarioEncontrado = null;
            using (SqlConnection conexion = new SqlConnection(_contexto.Conexion))
            {

                using (SqlCommand comando = new SqlCommand("SP_ExisteUsuario", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Clave_Unica", usuario.Clave_Unica);
                    comando.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);

                    try
                    {
                        // Apertura de la conexión
                        conexion.Open();

                        // Ejecutar el procedimiento
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while(lector.Read())
                            {
                                usuarioEncontrado = new Usuario
                                {
                                    Id_Usuario = (int)lector["Id_Usuario"],
                                    Clave_Unica = (int)lector["Clave_Unica"],
                                    Contrasena = lector["Contrasena"].ToString()
                                };

                                //Si el usuario si existe lo retornamos
                                return usuarioEncontrado;

                                //Aqui hacemos la busqueda de los roles que va a tener nuestro usuario
                                //Esto se hace con la consulta a la BD entre las tablas Usuario, Usuario-Rol y Rol
                                //y se busca mediante el id del usuario.

                                //List<string> listaRoles = new List<string>();
                                //listaRoles = ObtenRol(usuario);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de la excepción (por ejemplo, registrar el error)
                        Console.WriteLine(ex.Message);
                    }
                }
                conexion.Close();
                return null;
            }
        }

        public List<string> ObtenRol(Usuario usuario)
        {
            List<string> listaRoles = new List<string>();
            using (SqlConnection conexion = new SqlConnection(_contexto.Conexion))
            {
                using (SqlCommand comando = new SqlCommand("SP_ObtenRol", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id_Usuario", usuario.Id_Usuario);

                    try
                    {
                        // Apertura de la conexión
                        conexion.Open();
                        // Ejecutar el procedimiento
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                listaRoles.Add(lector["Nombre_Rol"].ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de la excepción (por ejemplo, registrar el error)
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return listaRoles;
        }

    }
}
