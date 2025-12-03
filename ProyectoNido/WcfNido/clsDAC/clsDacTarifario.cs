using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacTarifario
    {
        public List<clsEntidades.clsTarifario> Listar()
        {
            var lista = new List<clsEntidades.clsTarifario>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("listar_tarifario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var t = new clsEntidades.clsTarifario
                            {
                                Id_Tarifario = Convert.ToInt32(dr["Id_Tarifario"]),
                                Tipo = dr["Tipo"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion"))
                                              ? null
                                              : dr["Descripcion"].ToString(),
                                Periodo = Convert.ToByte(dr["Periodo"]),
                                Valor = Convert.ToDecimal(dr["Valor"])
                            };

                            lista.Add(t);
                        }
                    }
                }
            }

            return lista;
        }
        public List<clsEntidades.clsTarifario> Filtrar(string texto)
        {
            var lista = new List<clsEntidades.clsTarifario>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("tarifario_filtrar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TextoBuscar",
                        string.IsNullOrWhiteSpace(texto) ? (object)DBNull.Value : texto);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new clsEntidades.clsTarifario
                            {
                                Id_Tarifario = Convert.ToInt32(dr["Id_Tarifario"]),
                                Tipo = dr["Tipo"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Periodo = Convert.ToByte(dr["Periodo"]),
                                Valor = Convert.ToDecimal(dr["Valor"])
                            });
                        }
                    }
                }
            }

            return lista;
        }


        public void Insertar(clsEntidades.clsTarifario t)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("insertar_tarifario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Tipo", t.Tipo);
                        cmd.Parameters.AddWithValue("@Nombre", t.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion",
                            (object)t.Descripcion ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Periodo", t.Periodo);
                        cmd.Parameters.AddWithValue("@Valor", t.Valor);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar Tarifario: " + ex.Message);
            }
        }

        public void Modificar(clsEntidades.clsTarifario t)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("modificar_tarifario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id_Tarifario", t.Id_Tarifario);
                        cmd.Parameters.AddWithValue("@Tipo", t.Tipo);
                        cmd.Parameters.AddWithValue("@Nombre", t.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion",
                            (object)t.Descripcion ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Periodo", t.Periodo);
                        cmd.Parameters.AddWithValue("@Valor", t.Valor);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar Tarifario: " + ex.Message);
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("eliminar_tarifario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id_Tarifario", id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar Tarifario: " + ex.Message);
            }
        }
    }
}
