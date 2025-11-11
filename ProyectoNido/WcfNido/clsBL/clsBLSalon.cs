using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLSalon
    {
        public List<clsEntidades.clsSalon> listar_salon()
        {
            clsDAC.clsDacSalon xsalon = new clsDAC.clsDacSalon();
            List<clsEntidades.clsSalon> xlistasalon = xsalon.listarSalon();
            return xlistasalon;
        }

        public void eliminar_salon(int xcodigo)
        {
            try
            {
                clsDAC.clsDacSalon xsal = new clsDAC.clsDacSalon();
                xsal.EliminarSalon(xcodigo);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
        public void insertar_salon(clsEntidades.clsSalon xSal)
        {
            try
            {
                clsDAC.clsDacSalon db = new clsDAC.clsDacSalon();
                db.InsertarSalon(xSal);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }

        public void modificar_salon(clsEntidades.clsSalon xSal)
        {
            try
            {
                clsDAC.clsDacSalon db = new clsDAC.clsDacSalon();
                db.ModificarSalon(xSal);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
    }
}
