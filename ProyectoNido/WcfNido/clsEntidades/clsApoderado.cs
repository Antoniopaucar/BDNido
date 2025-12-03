using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsEntidades
{
    public class clsApoderado:clsUsuario
    {
        public string CopiaDni {  get; set; }
        public clsApoderado() { }
        public clsApoderado(clsUsuario usuario, string copiaDni)
        {
            // Copiamos los atributos del usuario a la clase base
            this.Id = usuario.Id;
            this.NombreUsuario = usuario.NombreUsuario;
            this.Nombres = usuario.Nombres;
            this.ApellidoPaterno = usuario.ApellidoPaterno;
            this.ApellidoMaterno = usuario.ApellidoMaterno;
            this.Dni = usuario.Dni;
            // ... y todos los demás que tengas
            this.CopiaDni = copiaDni;
        }
    }
}
