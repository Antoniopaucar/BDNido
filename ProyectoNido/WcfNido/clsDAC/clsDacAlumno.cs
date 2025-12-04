using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDAC
{
    public class clsDacAlumno
    {
        public List<clsEntidades.clsAlumno> listarAlumnos()
        {
            List<clsEntidades.clsAlumno> lista = new List<clsEntidades.clsAlumno>();

            using (SqlConnection cn = clsConexion.getInstance().GetSqlConnection())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Alumno_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var a = new clsEntidades.clsAlumno
                            {
                                Id_Alumno = Convert.ToInt32(dr["Id_Alumno"]),
                                Nombres = dr["Nombres"].ToString(),
                                ApellidoPaterno = dr["ApPaterno"].ToString(),
                                ApellidoMaterno = dr["ApMaterno"].ToString()
                                // lo demás (Dni, FechaNacimiento, etc.) lo puedes agregar después si lo necesitas
                            };

                            lista.Add(a);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
