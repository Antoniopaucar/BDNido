using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLComunicado
    {
        public List<clsEntidades.clsComunicado> listar_comunicados(int idUsuario)
        {
            clsDAC.clsDacComunicado xcomunicados = new clsDAC.clsDacComunicado();
            List<clsEntidades.clsComunicado> xlistacomunicados = xcomunicados.listarComunicados(idUsuario);
            return xlistacomunicados;
        }

        public void marcar_comunicado_visto(int idComunicado, int idUsuario)
        {
            try
            {
                clsDAC.clsDacComunicado db = new clsDAC.clsDacComunicado();
                db.MarcarComunicadoVisto(idComunicado, idUsuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void eliminar_comunicado(int xcodigo)
        {
            try
            {
                clsDAC.clsDacComunicado xcoms = new clsDAC.clsDacComunicado();
                xcoms.EliminarComunicado(xcodigo);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
        public void insertar_comunicado(clsEntidades.clsComunicado xcom)
        {

            try
            {
                clsDAC.clsDacComunicado db = new clsDAC.clsDacComunicado();
                db.InsertarComunicado(xcom);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }

        public void modificar_comunicado(clsEntidades.clsComunicado xCom)
        {
            try
            {
                clsDAC.clsDacComunicado db = new clsDAC.clsDacComunicado();
                db.ModificarComunicado(xCom);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
    }
}
