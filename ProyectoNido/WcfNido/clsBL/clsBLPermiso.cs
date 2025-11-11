using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLPermiso
    {
        public List<clsEntidades.clsPermiso> listar_permisos()
        {
            clsDAC.clsDacPermiso xPer = new clsDAC.clsDacPermiso();
            List<clsEntidades.clsPermiso> xlistaPer = xPer.listarPermiso();
            return xlistaPer;
        }

        public void eliminar_permiso(int xcodigo)
        {
            try
            {
                clsDAC.clsDacPermiso xPer = new clsDAC.clsDacPermiso();
                xPer.EliminarPermiso(xcodigo);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
        public void insertar_permiso(clsEntidades.clsPermiso xPer)
        {
            try
            {
                clsDAC.clsDacPermiso db = new clsDAC.clsDacPermiso();
                db.InsertarPermiso(xPer);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }

        public void modificar_permiso(clsEntidades.clsPermiso xPer)
        {
            try
            {
                clsDAC.clsDacPermiso db = new clsDAC.clsDacPermiso();
                db.ModificarPermiso(xPer);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
    }
}
