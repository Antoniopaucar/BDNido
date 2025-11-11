using System;
using System.Collections.Generic;
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
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
        public void insertar_usuario_rol(clsEntidades.clsUsuarioRol xUr)
        {

            try
            {
                clsDAC.clsDacUsuarioRol db = new clsDAC.clsDacUsuarioRol();
                db.InsertarUsuarioRol(xUr);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }

        public void modificar_usuario_rol(clsEntidades.clsUsuarioRol xUr)
        {
            try
            {
                clsDAC.clsDacUsuarioRol db = new clsDAC.clsDacUsuarioRol();
                db.ModificarUsuarioRol(xUr);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
    }
}
