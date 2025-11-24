using clsUtilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsBL
{
    public class clsBLUsuario
    {
        public List<clsEntidades.clsUsuario> listar_usuarios()
        {
            clsDAC.clsDacUsuario xusuarios = new clsDAC.clsDacUsuario();
            List<clsEntidades.clsUsuario> xlistausuarios = xusuarios.listarUsuarios();
            return xlistausuarios;
        }

        public void eliminar_usuario(int xcodigo)
        {
            try
            {
                clsDAC.clsDacUsuario xusuarios = new clsDAC.clsDacUsuario();
                xusuarios.EliminarUsuario(xcodigo);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }
        public void insertar_usuario(clsEntidades.clsUsuario xusuario)
        {

            try
            {
                //if (xusuario.Clave == "12345")
                //{
                //    throw new ArgumentException("Error en la clave, es muy facil ");
                //}

                clsDAC.clsDacUsuario db = new clsDAC.clsDacUsuario();
                db.InsertarUsuario(xusuario);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }

        public void modificar_usuario(clsEntidades.clsUsuario xUser)
        {
            try
            {
                clsDAC.clsDacUsuario db = new clsDAC.clsDacUsuario();
                db.ModificarUsuario(xUser);
            }
            catch (ArgumentException ex)
            {
                //throw new ApplicationException(ex.Message);
                throw;
            }
        }

        public (bool Exito, string Mensaje) validar_contrasenia_segura(string usuario, string contrasenia)
        {
            try
            {
                clsDAC.clsDacUsuario db = new clsDAC.clsDacUsuario();
                return db.ValidarContraseniaSegura(usuario, contrasenia);
            }
            catch (Exception ex)
            {
                return (false, "Error al validar la contraseña: " + ex.Message);
            }
        }

        public void actualizar_datos_docente(int idUsuario, string nombres, string apPaterno, string apMaterno,
            string dni, DateTime? fechaNacimiento, string sexo, string direccion, string email,
            DateTime? fechaIngreso, string tituloProfesional, string cv, string evaluacionPsicologica,
            string fotos, string verificacionDomiciliaria)
        {
            try
            {
                clsDAC.clsDacUsuario db = new clsDAC.clsDacUsuario();
                db.ActualizarDatosDocente(idUsuario, nombres, apPaterno, apMaterno, dni, fechaNacimiento,
                    sexo, direccion, email, fechaIngreso, tituloProfesional, cv, evaluacionPsicologica,
                    fotos, verificacionDomiciliaria);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al actualizar los datos del docente: " + ex.Message);
            }
        }

        public clsEntidades.clsUsuario obtener_datos_docente(int idUsuario)
        {
            try
            {
                clsDAC.clsDacUsuario db = new clsDAC.clsDacUsuario();
                return db.ObtenerDatosDocente(idUsuario);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener los datos del docente: " + ex.Message);
            }
        }

    }
}
