using KardexEscolar.Models;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
                        conexion.Close();
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



        //Dentro ya del sistema
        public List<Materia_Calificacion> ObtenMateriaCalificacion(int clave_Unica)
        {
            Materia_Calificacion materiasCalificaciones = null;
            List<Materia_Calificacion> listaMateriasCalificaciones = new List<Materia_Calificacion>();
            using (SqlConnection conexion = new SqlConnection(_contexto.Conexion))
            {
                using (SqlCommand comando = new SqlCommand("SP_ObtenCalificacionesYMaterias", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Clave_Unica", clave_Unica);
                    try
                    {
                        //Apertura de la conexión
                        conexion.Open();
                        //Ejecutar el procedimiento
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                materiasCalificaciones = new Materia_Calificacion
                                {
                                    Clave = (int)lector["Clave"],
                                    Grupo = (int)lector["Grupo"],
                                    NombreMateria = lector["NombreMateria"].ToString(),
                                    //Tratamos diferentes estos datos ya que en algun momento pueden llegar a ser nulos
                                    Parcial_1 = lector["Parcial_1"] != DBNull.Value ? Convert.ToSingle(lector["Parcial_1"]) : 0.0f,
                                    Parcial_2 = lector["Parcial_2"] != DBNull.Value ? Convert.ToSingle(lector["Parcial_2"]) : 0.0f,
                                    Parcial_3 = lector["Parcial_3"] != DBNull.Value ? Convert.ToSingle(lector["Parcial_3"]) : 0.0f,
                                    Parcial_4 = lector["Parcial_4"] != DBNull.Value ? Convert.ToSingle(lector["Parcial_4"]) : 0.0f,
                                    Ordinario = lector["Ordinario"] != DBNull.Value ? Convert.ToSingle(lector["Ordinario"]) : 0.0f,
                                    Extraordinario = lector["Extraordinario"] != DBNull.Value ? Convert.ToSingle(lector["Extraordinario"]) : 0.0f,
                                    Titulo = lector["Titulo"] != DBNull.Value ? Convert.ToSingle(lector["Titulo"]) : 0.0f,
                                };
                                listaMateriasCalificaciones.Add(materiasCalificaciones);
                                //return materiasCalificaciones;
                            }
                        }
                        conexion.Close();
                        return listaMateriasCalificaciones;
                    }
                    catch (Exception ex)
                    {
                        // Manejo de la excepción (por ejemplo, registrar el error)
                        Console.WriteLine(ex.Message);
                    }
                }

            }
            return null;
        }

        public List<Materia_Profesor> ObtenMateriaProfesor(int clave_Unica)
        {
            Materia_Profesor materiasProfesores = null;
            List<Materia_Profesor> listaMateriasProfesores = new List<Materia_Profesor>();

            using (SqlConnection conexion = new SqlConnection(_contexto.Conexion))
            {
                using (SqlCommand comando = new SqlCommand("SP_ObtenMateriasAlumno", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Clave_Unica", clave_Unica);

                    try
                    {
                        conexion.Open();
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                materiasProfesores = new Materia_Profesor()
                                {
                                    Grupo = (int)lector["Grupo"],
                                    NombreMateria = lector["NombreMateria"].ToString(),
                                    Nombre = lector["Nombre"].ToString(),
                                    Apellido_Paterno = lector["Apellido_Paterno"].ToString(),
                                    Apellido_Materno = lector["Apellido_Materno"].ToString()
                                };
                                listaMateriasProfesores.Add(materiasProfesores);
                            }
                        }
                        conexion.Close();
                        return listaMateriasProfesores;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return null;
        }

        public Alumno ObtenDatosAlumno(int claveUnica)
        {
            Alumno alumno = null;
            using (SqlConnection conexion = new SqlConnection(_contexto.Conexion))
            {

                using (SqlCommand comando = new SqlCommand("SP_ObtenDatosAlumno", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Clave_Unica", claveUnica);

                    try
                    {
                        // Apertura de la conexión
                        conexion.Open();

                        // Ejecutar el procedimiento
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                alumno = new Alumno
                                {
                                    Id_Alumno = (int)lector["Id_Alumno"],
                                    Nombre = lector["Nombre"].ToString(),
                                    Apellido_Paterno = lector["Apellido_Paterno"].ToString(),
                                    Apellido_Materno = lector["Apellido_Materno"].ToString(),
                                    Semestre = (int)lector["Semestre"],
                                    Clave_Unica = (int)lector["Clave_Unica"],
                                    Id_Usuario = (int)lector["Id_Usuario"],
                                };

                                //Si el usuario si existe lo retornamos
                                return alumno;

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

        public int CambiaContrasena(int claveUnica, string contrasenaAnterior, string contrasenaNueva)
        {
            using (SqlConnection conexion = new SqlConnection(_contexto.Conexion))
            {
                using (SqlCommand comando = new SqlCommand("SP_CambiarContrasena", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Clave_Unica", claveUnica);
                    comando.Parameters.AddWithValue("@contrasenaAnterior", contrasenaAnterior);
                    comando.Parameters.AddWithValue("@contrasenaNueva", contrasenaNueva);

                    try
                    {
                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();

                        if(filasAfectadas > 0)
                        {
                            return filasAfectadas;
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                conexion.Close();
                return 0;
            }
        }



    }
}
