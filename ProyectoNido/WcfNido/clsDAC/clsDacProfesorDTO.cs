using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacProfesorDTO
    {
        public List<clsEntidades.clsProfesorDTO> listarProfesores()
        {
            var lista = new List<clsEntidades.clsProfesorDTO>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Profesor_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var p = new clsEntidades.clsProfesorDTO
                            {
                                Id_Profesor = Convert.ToInt32(dr["Id_Profesor"]),
                                Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                                Nombres = dr["Nombres"].ToString(),
                                ApellidoPaterno = dr["ApPaterno"].ToString(),
                                ApellidoMaterno = dr["ApMaterno"].ToString()
                            };

                            lista.Add(p);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
