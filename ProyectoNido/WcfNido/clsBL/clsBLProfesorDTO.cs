using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLProfesorDTO
    {
        public List<clsEntidades.clsProfesorDTO> listar_profesores()
        {
            clsDAC.clsDacProfesorDTO db = new clsDAC.clsDacProfesorDTO();
            return db.listarProfesores();
        }
    }
}
