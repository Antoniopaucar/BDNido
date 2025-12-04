using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLProfesor
    {
        public List<clsEntidades.clsProfesorDTO> listar_profesores()
        {
            clsDAC.clsDacProfesor db = new clsDAC.clsDacProfesor();
            return db.listarProfesores();
        }
    }
}
