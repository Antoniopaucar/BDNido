using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacApoderado
    {
        public List<clsEntidades.clsApoderado> listarApoderados()
        {
            List<clsEntidades.clsApoderado> lista = new List<clsEntidades.clsApoderado>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_apoderados", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clsEntidades.clsApoderado a = new clsEntidades.clsApoderado();

                            a.Id = Convert.ToInt32(dr["Id_Apoderado"]);
                            a.Nombres= dr["Nombres"].ToString();
                            a.ApellidoPaterno = dr["ApPaterno"].ToString();
                            a.ApellidoMaterno = dr["ApMaterno"].ToString();
                            a.Dni = dr["Dni"].ToString();
                            a.CopiaDni = dr["CopiaDni"].ToString();

                            lista.Add(a);
                        }
                    }
                }
            }

            return lista;
        }

        public void EliminarApoderado(int id)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_apoderados", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void InsertarApoderado(clsEntidades.clsApoderado xAp)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("insertar_apoderados", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id_Apoderado", xAp.Id);
                        cmd.Parameters.AddWithValue("@CopiaDni", xAp.CopiaDni);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void ModificarApoderado(clsEntidades.clsApoderado xAp)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("modificar_apoderados", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id_Apoderado", xAp.Id);
                        cmd.Parameters.AddWithValue("@CopiaDni", xAp.CopiaDni);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
    }
}
