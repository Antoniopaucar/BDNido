using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLAlumno_A
    {
        public List<clsEntidades.clsAlumno_A> listar_alumnos()
        {
            clsDAC.clsDacAlumno_A xalumno = new clsDAC.clsDacAlumno_A();
            List<clsEntidades.clsAlumno_A> lista = xalumno.listarAlumnos();
            return lista;
        }
    }
}
