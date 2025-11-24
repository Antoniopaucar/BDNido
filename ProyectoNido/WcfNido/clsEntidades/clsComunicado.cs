using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsComunicado
    {
        public int Id { get; set; }
        public clsUsuario Usuario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaFinal { get; set; }
        public bool Visto { get; set; }

        public clsComunicado() {}

        public clsComunicado(int id, clsUsuario usuario, string nombre, string descripcion, DateTime? fechaFinal)
        {
            Id = id;
            Usuario = usuario;
            Nombre = nombre;
            Descripcion = descripcion;
            FechaCreacion = DateTime.Now;
            FechaFinal = fechaFinal;
        }
    }
}
