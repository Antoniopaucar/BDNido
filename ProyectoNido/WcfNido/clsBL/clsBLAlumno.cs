using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLAlumno
    {
        public List<clsEntidades.clsAlumno> listar_alumnos()
        {
            clsDAC.clsDacAlumno xalumno = new clsDAC.clsDacAlumno();
            List<clsEntidades.clsAlumno> lista = xalumno.listarAlumnos();
            return lista;
        }
    }
}
