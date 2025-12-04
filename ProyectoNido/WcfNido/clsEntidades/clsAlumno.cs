using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsAlumno
    {
        public int Id_Alumno { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }

        // Propiedad calculada para mostrar en combos y grids:
        public string NombreCompleto
        {
            get { return $"{ApellidoPaterno} {ApellidoMaterno}, {Nombres}"; }
        }

        public clsAlumno() { }

        public clsAlumno(int id, string nombres, string apP, string apM)
        {
            Id_Alumno = id;
            Nombres = nombres;
            ApellidoPaterno = apP;
            ApellidoMaterno = apM;
        }
    }
}
