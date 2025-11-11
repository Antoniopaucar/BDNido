using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace clsDAC
{
    public class clsDacUsuario
    {
        public List<clsEntidades.clsUsuario> listarUsuarios()
        {
            List<clsEntidades.clsUsuario> lista = new List<clsEntidades.clsUsuario>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_usuarios", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsUsuario u = new clsEntidades.clsUsuario();
                            

                            u.Id = Convert.ToInt32(dr["Id"]);
                            u.NombreUsuario =dr["NombreUsuario"].ToString();
                            u.Clave = dr["Clave"].ToString();
                            u.Nombres = dr["Nombres"].ToString();
                            u.ApellidoPaterno = dr["ApPaterno"].ToString();
                            u.ApellidoMaterno = dr["ApMaterno"].ToString();
                            u.Dni = dr["Dni"].ToString();
                            u.FechaNacimiento = dr.IsDBNull(dr.GetOrdinal("FechaNacimiento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaNacimiento"));
                            u.Sexo = dr["Sexo"].ToString();
                            u.Direccion = dr["Direccion"].ToString();
                            u.Telefono = dr["Telefono"].ToString();
                            u.Email = dr["Email"].ToString();
                            u.Activo = Convert.ToBoolean(dr["Activo"]);
                            u.Intentos = Convert.ToInt32(dr["Intentos"]);
                            u.Bloqueado = Convert.ToBoolean(dr["Bloqueado"]);
                            u.FechaBloqueo = dr.IsDBNull(dr.GetOrdinal("FechaBloqueo")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaBloqueo"));
                            u.UltimoIntento = dr.IsDBNull(dr.GetOrdinal("UltimoIntento")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("UltimoIntento"));
                            u.UltimoLoginExitoso = dr.IsDBNull(dr.GetOrdinal("UltimoLoginExitoso")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("UltimoLoginExitoso"));
                            u.FechaCreacion = dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));

                            lista.Add(u);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarUsuario(int id)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_usuarios", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertarUsuario(clsEntidades.clsUsuario xusr)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("insertar_usuarios", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreUsuario",xusr.NombreUsuario);
                    cmd.Parameters.AddWithValue("@Clave", clsUtilidades.clsHash.ObtenerSha256(xusr.Clave));
                    cmd.Parameters.AddWithValue("@Nombres", xusr.Nombres);
                    cmd.Parameters.AddWithValue("@ApPaterno", xusr.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApMaterno", xusr.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Dni", xusr.Dni);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", xusr.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", xusr.Sexo);
                    cmd.Parameters.AddWithValue("@Direccion", xusr.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", xusr.Telefono);
                    cmd.Parameters.AddWithValue("@Email", xusr.Email);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ModificarUsuario(clsEntidades.clsUsuario xusr)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_usuarios", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", xusr.Id);
                    cmd.Parameters.AddWithValue("@NombreUsuario", xusr.NombreUsuario);
                    cmd.Parameters.AddWithValue("@Clave", clsUtilidades.clsHash.ObtenerSha256(xusr.Clave));
                    cmd.Parameters.AddWithValue("@Nombres", xusr.Nombres);
                    cmd.Parameters.AddWithValue("@ApPaterno", xusr.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApMaterno", xusr.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Dni", xusr.Dni);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", xusr.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", xusr.Sexo);
                    cmd.Parameters.AddWithValue("@Direccion", xusr.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", xusr.Telefono);
                    cmd.Parameters.AddWithValue("@Email", xusr.Email);
                    cmd.Parameters.AddWithValue("@Activo", xusr.Activo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public (bool Exito, string Mensaje) ValidarContraseniaSegura(string usuario, string contrasena)
        {
            bool exito = false;
            string mensaje = string.Empty;

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("ValidarContraseniaSegura", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            exito = Convert.ToBoolean(dr["Exito"]);
                            mensaje = dr["Mensaje"].ToString();
                        }
                    }
                }
            }

            return (exito, mensaje);
        }
    }
}
