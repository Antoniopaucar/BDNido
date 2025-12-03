using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLUsuarioRol
    {
        public List<clsEntidades.clsUsuarioRol> listar_usuario_rol()
        {
            clsDAC.clsDacUsuarioRol xUr = new clsDAC.clsDacUsuarioRol();
            List<clsEntidades.clsUsuarioRol> xlistaUR = xUr.listarUsuarioRol();
            return xlistaUR;
        }

        public void eliminar_usuario_rol(int id_user,int id_rol)
        {
            try
            {
                clsDAC.clsDacUsuarioRol xUr = new clsDAC.clsDacUsuarioRol();
                xUr.EliminarUsuarioRol(id_user,id_rol);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
        public void insertar_usuario_rol(clsEntidades.clsUsuarioRol xUr)
        {

            try
            {
                clsDAC.clsDacUsuarioRol db = new clsDAC.clsDacUsuarioRol();
                db.InsertarUsuarioRol(xUr);
            }
            catch (SqlException ex)
            {
                clsBLError dacError = new clsBLError();
                dacError.Control_Sql_Error(ex);
            }
        }
    }
}
