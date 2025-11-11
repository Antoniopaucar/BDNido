using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido.Auxiliar
{
    public static class clsValidacion
    {
        public static string ValidarUser(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                return "El Usuario no debe estar vacío.";

            if (Regex.IsMatch(nombreUsuario, @"\s"))
                return "El Usuario No debe contener espacios en blanco.";

            if (nombreUsuario.Length < 8 || nombreUsuario.Length > 15)
                return "El Usuario debe tener un mínimo de 8 caracteres y un máximo de 15 caracteres.";

            if (Regex.IsMatch(nombreUsuario, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "El Usuario NO debe tener el formato de una dirección de correo electrónico.";

            return null;
        }

        public static string ValidarContrasenia(string contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
                return "la contraseña no debe estar vacía.";

            if (contrasena.Length < 8 || contrasena.Length > 15)
                return "La contraseña debe tener un mínimo de 8 caracteres y un máximo de 15 caracteres.";

            if (!Regex.IsMatch(contrasena, @"\d"))
                return "La contraseña debe contener al menos un número.";

            if (!Regex.IsMatch(contrasena, @"[A-Z]"))
                return "La contraseña debe contener al menos una letra mayúscula.";

            if (!Regex.IsMatch(contrasena, @"[!@#$%^&*()_+{}\[\]:;'<>,.?\\|~`-]"))
                return "La contraseña debe contener al menos un carácter especial.";

            if (Regex.IsMatch(contrasena, @"\s"))
                return "La contraseña No debe contener espacios en blanco.";

            return null;
        }

        public static void LimpiarControles(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox tb)
                {
                    tb.Text = string.Empty;
                }
                else if (c is DropDownList ddl)
                {
                    ddl.SelectedIndex = 0; // o -1 si quieres sin selección
                }
                else if (c is CheckBox cb)
                {
                    cb.Checked = false;
                }
                else if (c is RadioButton rb)
                {
                    rb.Checked = false;
                }
                else if (c is RadioButtonList rbl)
                {
                    rbl.ClearSelection();
                }
                else if (c is ListBox lb)
                {
                    lb.ClearSelection();
                }
                else if (c is FileUpload fu)
                {
                    // No hay método directo para limpiar FileUpload, se puede reemplazar por uno nuevo
                    // Nota: esto solo funciona si no hay postback aún
                }

                // Recursivamente limpiar controles hijos
                if (c.HasControls())
                {
                    LimpiarControles(c);
                }
            }
        }
    }
}