using clsDAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLTarifario
    {
        // LISTAR
        public List<clsEntidades.clsTarifario> listar_tarifario()
        {
            clsDAC.clsDacTarifario xTarifario = new clsDAC.clsDacTarifario();
            List<clsEntidades.clsTarifario> lista = xTarifario.Listar();   // <- aquí llama al DAC
            return lista;
        }

        public List<clsEntidades.clsTarifario> filtrar_tarifario(string texto)
        {
            clsDAC.clsDacTarifario db = new clsDAC.clsDacTarifario();
            return db.Filtrar(texto);
        }

        // ELIMINAR
        public void eliminar_tarifario(int idTarifario)
        {
            try
            {
                clsDAC.clsDacTarifario xTarifario = new clsDAC.clsDacTarifario();
                xTarifario.Eliminar(idTarifario);   // <- DAC.Eliminar
            }
            catch (ArgumentException)
            {
                // Se relanza tal cual, igual que en clsBLUsuario
                throw;
            }
            catch (Exception ex)
            {
                // Si quieres, puedes envolver en ApplicationException
                throw new ApplicationException("Error al eliminar el tarifario: " + ex.Message);
            }
        }

        // INSERTAR
        public void insertar_tarifario(clsEntidades.clsTarifario t)
        {
            try
            {
                // Ejemplo de validación de negocio simple (opcional)
                if (string.IsNullOrWhiteSpace(t.Nombre))
                    throw new ArgumentException("El nombre del tarifario es obligatorio.");

                if (t.Valor <= 0)
                    throw new ArgumentException("El valor del tarifario debe ser mayor a 0.");

                clsDAC.clsDacTarifario db = new clsDAC.clsDacTarifario();
                db.Insertar(t);   // <- DAC.Insertar
            }
            catch (ArgumentException)
            {
                // Igual que en insertar_usuario, se relanza para que lo capture WCF o la UI
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al insertar el tarifario: " + ex.Message);
            }
        }

        // MODIFICAR
        public void modificar_tarifario(clsEntidades.clsTarifario t)
        {
            try
            {
                if (t.Id_Tarifario <= 0)
                    throw new ArgumentException("El Id_Tarifario no es válido.");

                if (string.IsNullOrWhiteSpace(t.Nombre))
                    throw new ArgumentException("El nombre del tarifario es obligatorio.");

                clsDAC.clsDacTarifario db = new clsDAC.clsDacTarifario();
                db.Modificar(t);   // <- DAC.Modificar
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al modificar el tarifario: " + ex.Message);
            }
        }
    }
}
