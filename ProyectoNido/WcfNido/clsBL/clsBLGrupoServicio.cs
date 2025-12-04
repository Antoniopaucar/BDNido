using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLGrupoServicio
    {
        public List<clsEntidades.clsGrupoServicio> listar_grupoServicio()
        {
            clsDAC.clsDacGrupoServicio dac = new clsDAC.clsDacGrupoServicio();
            List<clsEntidades.clsGrupoServicio> lista = dac.ListarGrupoServicio();
            return lista;
        }

        public void eliminar_grupoServicio(int idGrupoServicio)
        {
            try
            {
                clsDAC.clsDacGrupoServicio dac = new clsDAC.clsDacGrupoServicio();
                dac.EliminarGrupoServicio(idGrupoServicio);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void insertar_grupoServicio(clsEntidades.clsGrupoServicio grupo)
        {
            try
            {
                clsDAC.clsDacGrupoServicio dac = new clsDAC.clsDacGrupoServicio();
                dac.InsertarGrupoServicio(grupo);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void modificar_grupoServicio(clsEntidades.clsGrupoServicio grupo)
        {
            try
            {
                clsDAC.clsDacGrupoServicio dac = new clsDAC.clsDacGrupoServicio();
                dac.ModificarGrupoServicio(grupo);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
