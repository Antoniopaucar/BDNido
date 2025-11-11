using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLRol
    {
        public List<clsEntidades.clsRol> listar_roles()
        {
            clsDAC.clsDacRol xRol = new clsDAC.clsDacRol();
            List<clsEntidades.clsRol> xlistaRol = xRol.listarRol();
            return xlistaRol;
        }

        public void eliminar_rol(int xcodigo)
        {
            try
            {
                clsDAC.clsDacRol xRol = new clsDAC.clsDacRol();
                xRol.EliminarRol(xcodigo);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
        public void insertar_rol(clsEntidades.clsRol xRol)
        {
            try
            {
                clsDAC.clsDacRol db = new clsDAC.clsDacRol();
                db.InsertarRol(xRol);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }

        public void modificar_rol(clsEntidades.clsRol xRol)
        {
            try
            {
                clsDAC.clsDacRol db = new clsDAC.clsDacRol();
                db.ModificarRol(xRol);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
    }
}
