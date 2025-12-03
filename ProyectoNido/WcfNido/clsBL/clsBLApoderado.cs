using clsUtilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLApoderado
    {
        public List<clsEntidades.clsApoderado> listar_apoderados()
        {
            clsDAC.clsDacApoderado xapos = new clsDAC.clsDacApoderado();
            List<clsEntidades.clsApoderado> xlistaApos = xapos.listarApoderados();
            return xlistaApos;
        }

        public void eliminar_apoderado(int xcodigo)
        {
            try
            {
                clsDAC.clsDacApoderado xApos = new clsDAC.clsDacApoderado();
                xApos.EliminarApoderado(xcodigo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_apoderado(clsEntidades.clsApoderado xapo)
        {
            try
            {
                clsDAC.clsDacApoderado db = new clsDAC.clsDacApoderado();
                db.InsertarApoderado(xapo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }

        public void modificar_apoderado(clsEntidades.clsApoderado xapo)
        {
            try
            {
                clsDAC.clsDacApoderado db = new clsDAC.clsDacApoderado();
                db.ModificarApoderado(xapo);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
    }
}
