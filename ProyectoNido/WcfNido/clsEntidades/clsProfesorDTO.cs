using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsProfesorDTO
    {
        public int Id_Profesor { get; set; }
        public int Id_Usuario { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }

        public string NombreCompleto
        {
            get { return $"{ApellidoPaterno} {ApellidoMaterno}, {Nombres}"; }
        }
    }
}
