using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacComunicado
    {
        public List<clsEntidades.clsComunicado> listarComunicados()
        {
            List<clsEntidades.clsComunicado> lista = new List<clsEntidades.clsComunicado>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_comunicados", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsComunicado c = new clsEntidades.clsComunicado();
                            c.Usuario = new clsEntidades.clsUsuario();

                            c.Id = Convert.ToInt32(dr["Id_Comunicado"]);
                            c.Usuario.Id = Convert.ToInt32(dr["Id_Usuario"]);
                            c.Usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                            c.Nombre = dr["Nombre"].ToString();
                            c.Descripcion = dr["Descripcion"].ToString();
                            c.FechaCreacion = dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));
                            c.FechaFinal = dr.IsDBNull(dr.GetOrdinal("FechaFinal")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("FechaFinal"));

                            lista.Add(c);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarComunicado(int id)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_comunicados", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertarComunicado(clsEntidades.clsComunicado xcom)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("insertar_comunicados", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id_Usuario", xcom.Usuario.Id);
                    cmd.Parameters.AddWithValue("@Nombre", xcom.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", xcom.Descripcion);
                    cmd.Parameters.AddWithValue("@FechaFinal", xcom.FechaFinal);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ModificarComunicado(clsEntidades.clsComunicado xcom)
        {
            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_comunicados", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", xcom.Id);
                    cmd.Parameters.AddWithValue("@Id_Usuario", xcom.Usuario.Id);
                    cmd.Parameters.AddWithValue("@Nombre", xcom.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", xcom.Descripcion);
                    cmd.Parameters.AddWithValue("@FechaFinal", xcom.FechaFinal);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
